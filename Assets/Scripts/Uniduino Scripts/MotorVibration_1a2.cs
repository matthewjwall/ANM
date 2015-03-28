using UnityEngine;
using System.Collections;
using Uniduino;

public class MotorVibration_1a2 : MonoBehaviour {

	public Arduino arduino;

	public int sensePin1 = 0;
	public int motorPin1= 8;

	public int senseVal1;
	public bool checkSense1 = false;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Log = (s) => Debug.Log("Arduino: " +s);
		arduino.Setup(ConfigurePins);

		StartCoroutine (motorOn1 (checkSense1));
	
	}

	void ConfigurePins( )
	{
		arduino.pinMode(sensePin1, PinMode.ANALOG);
		arduino.reportAnalog(sensePin1, 1);

		arduino.pinMode(motorPin1, PinMode.OUTPUT);

	}

	void OnGUI()
	{

		senseVal1 = arduino.analogRead(sensePin1);

		GUILayout.BeginArea(new Rect(100, 100, Screen.width/3, Screen.height-100));
		GUILayout.HorizontalSlider(senseVal1, 40, 940, GUILayout.Height(21), GUILayout.Width(150));
		
		GUILayout.EndArea();
		
	}

	IEnumerator motorOn1(bool checkSense1) {

		while (true) {
			if (senseVal1 > 400) {
				checkSense1 = true;
			}
			else {
				checkSense1 = false;
			}
			if (checkSense1 == true) {
				print("Motor On");
				yield return new WaitForSeconds(1);
			}
			else {
				yield return null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
