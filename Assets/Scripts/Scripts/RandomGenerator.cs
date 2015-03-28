using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {
	
	public TextMesh textDisplay;
	public int randomNumber;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
		randomNumber = RandomNumberGetter();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if(timer > 1.0f)
		{
		randomNumber = RandomNumberGetter ();
		textDisplay.text = randomNumber.ToString ();
		timer = 0.0f;
		}
	
	}
	
	int RandomNumberGetter() {
		int random = 0;
		random = Random.Range (0,11);
		return random;
	}
}
