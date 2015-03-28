using UnityEngine;
using System.Collections;

public class Visualize_1a1 : MonoBehaviour {
	public Arduino_1a1 arduino;

	public AudioListener Audio;

	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	
	public Color outputColora1;
	public Color outputColora2;
	public Color outputColora3;
	public Color outputColora4;
	public Color outputColora5;

	public float duration = 1.0f;
	public float[] vals;
	public float lerpColor1;
	public float lerpColor2;

	public GameObject mainCamera;
	public GameObject[] balls;
	public GameObject ball2;
	public GameObject outputBall1;
	public GameObject outputBall2;
	public GameObject outputBall3;
	public GameObject outputBall4;
	public GameObject outputBall5;
	public GameObject servoHexagon0;
	public GameObject servoHexagon1;

	public int channel = 1;
	public int numSamples = 256;
	public int freq = 1;

	public Light pointLight;

	public Material outputMateriala1;
	public Material outputMateriala2;
	public Material outputMateriala3;
	public Material outputMateriala4;
	public Material outputMateriala5;


	// Use this for initialization
	void Start () {
		arduino = mainCamera.GetComponent<Arduino_1a1>();
	
	}
	
	// Update is called once per frame
	void Update () {
		outputMateriala1.color = outputColora1;
		outputMateriala2.color = outputColora2;
		outputMateriala3.color = outputColora3;
		outputMateriala4.color = outputColora4;
		outputMateriala5.color = outputColora5;

		if (arduino.toggleOutput1 == true) {
			outputColora1 = Color.yellow;
		}
		else {
			outputColora1 = Color.white;
		}
		if (arduino.toggleOutput2 == true) {
			outputColora2 = Color.yellow;
		}
		else {
			outputColora2 = Color.white;
		}
		if (arduino.toggleOutput3 == true) {
			outputColora3 = Color.yellow;
		}
		else {
			outputColora3 = Color.white;
		}
		if (arduino.toggleOutput4 == true) {
			outputColora4 = Color.yellow;
		}
		else {
			outputColora4 = Color.white;
		}
		if (arduino.toggleOutput5 == true) {
			outputColora5 = Color.yellow;
		}
		else {
			outputColora5 = Color.white;
		}

		lerpColor1 = arduino.colorVal4;
		servoHexagon0.GetComponent<Renderer>().material.color = Color.Lerp (colorStart, colorEnd, lerpColor1);
		lerpColor2 = arduino.colorVal5;
		servoHexagon1.GetComponent<Renderer>().material.color = Color.Lerp (colorStart, colorEnd, lerpColor2);

	}
}
