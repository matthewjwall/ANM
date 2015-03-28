using UnityEngine;
using System.Collections;
using Uniduino;

public class MotorVibration_1a1 : MonoBehaviour {

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
	
	}

	void ConfigurePins( )
	{
		arduino.pinMode(sensePin1, PinMode.ANALOG);
		arduino.reportAnalog(sensePin1, 1);

	}

	void OnGUI()
	{

		senseVal1 = arduino.analogRead(sensePin1);

		GUILayout.BeginArea(new Rect(100, 100, Screen.width/3, Screen.height-100));
		GUILayout.HorizontalSlider(senseVal1, 40, 940, GUILayout.Height(21), GUILayout.Width(150));

		if (senseVal1 > 400) {
			checkSense1 = true;
		}
		else {
			checkSense1 = false;
		}
		
		GUILayout.EndArea();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
