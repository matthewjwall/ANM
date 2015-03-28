using UnityEngine;
using System.Collections;
using Uniduino;

using UnityEngine;
using System.Collections;

public static class ExtensionMethod {
	
	public static int Remap (this int s, int a1, int a2, int b1, int b2) {
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
	public static float fRemap (this float s, float a1, float a2, float b1, float b2) {
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}

public class Arduino_1a1 : MonoBehaviour {

	public Arduino arduino;
	public GUISkin ANMSkin1;

	public int sensePin1 = 0;
	public int sensePin2 = 1;
	public int sensePin3 = 2;
	public int sensePin4 = 3;
	public int sensePin5 = 4;

	public int ledPin1 = 4;
	public int ledPin2 = 7;
	public int ledPin3 = 8;
	public int ledPin4 = 12;
	public int ledPin5 = 13;

	public int servoPin1 = 2;
	public int servoPin2 = 3;

	public int senseVal1;
	public int senseVal2;
	public int senseVal3;
	public int senseVal4;
	public int senseVal5;

	public bool toggleOutput1 = false;
	public bool toggleOutput2 = false;
	public bool toggleOutput3 = false;
	public bool toggleOutput4 = false;
	public bool toggleOutput5 = false;

	public bool checkSense1 = false;
	public bool checkSense2 = false;
	public bool checkSense3 = false;
	public bool checkSense4 = false;
	public bool checkSense5 = false;

	public float fSenseVal4;
	public float colorVal4;
	public float fSenseVal5;
	public float colorVal5;

	public int servoRot1;
	public int servoRot2;

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
		arduino.pinMode(sensePin2, PinMode.ANALOG);
		arduino.reportAnalog(sensePin2, 2);
		arduino.pinMode(sensePin3, PinMode.ANALOG);
		arduino.reportAnalog(sensePin3, 3);
		arduino.pinMode(sensePin3, PinMode.ANALOG);
		arduino.reportAnalog(sensePin4, 4);
		arduino.pinMode(sensePin5, PinMode.ANALOG);
		arduino.reportAnalog(sensePin5, 5);

		arduino.pinMode(ledPin1, PinMode.OUTPUT);
		arduino.pinMode(ledPin2, PinMode.OUTPUT);
		arduino.pinMode(ledPin3, PinMode.OUTPUT);
		arduino.pinMode(ledPin4, PinMode.OUTPUT);
		arduino.pinMode(ledPin5, PinMode.OUTPUT);

		arduino.pinMode(servoPin1, PinMode.SERVO);
		//arduino.pinMode(servoPin2, PinMode.SERVO);

	}

	void OnGUI()
	{

		GUI.skin = ANMSkin1;

		senseVal1 = arduino.analogRead(sensePin1);
		senseVal2 = arduino.analogRead(sensePin2);
		senseVal3 = arduino.analogRead(sensePin3);
		senseVal4 = arduino.analogRead(sensePin4);
		senseVal5 = arduino.analogRead(sensePin5);

		fSenseVal4 = arduino.analogRead (sensePin4);
		colorVal4 = fSenseVal4.fRemap(0, 1000, 0, 1);
		fSenseVal5 = arduino.analogRead (sensePin5);
		colorVal5 = fSenseVal5.fRemap(0, 1000, 0, 1);

		servoRot1 = senseVal4.Remap (0, 1000, 0, 180);
		servoRot2 = senseVal5.Remap (0, 1000, 0, 180);

		arduino.analogWrite (servoPin1, servoRot1);
		//arduino.analogWrite (servoPin2, servoRot2);

		GUILayout.BeginArea(new Rect(100, 50, Screen.width/3, Screen.height-100));

		toggleOutput1 = GUILayout.Toggle (toggleOutput1, "Output 1");
		toggleOutput2 = GUILayout.Toggle (toggleOutput2, "Output 2");
		toggleOutput3 = GUILayout.Toggle (toggleOutput3, "Output 3");
		toggleOutput4 = GUILayout.Toggle (toggleOutput4, "Output 4");
		toggleOutput5 = GUILayout.Toggle (toggleOutput5, "Output 5");

		GUILayout.HorizontalSlider(senseVal1, 0, 1000, GUILayout.Height(15), GUILayout.Width(120));
		GUILayout.Label ("Input 1");
		GUILayout.HorizontalSlider(senseVal2, 0, 1000, GUILayout.Height(15), GUILayout.Width(120));
		GUILayout.Label ("Input 2");
		GUILayout.HorizontalSlider(senseVal3, 0, 1000, GUILayout.Height(15), GUILayout.Width(120));
		GUILayout.Label ("Input 3");
		GUILayout.HorizontalSlider(senseVal4, 0, 1000, GUILayout.Height(15), GUILayout.Width(120));
		GUILayout.Label ("Input 4");
		GUILayout.HorizontalSlider(senseVal5, 0, 1000, GUILayout.Height(15), GUILayout.Width(120));
		GUILayout.Label ("Input 5");

		if (senseVal1 > 400) {
			checkSense1 = true;
			toggleOutput1 = true;
			arduino.digitalWrite (ledPin1, Arduino.HIGH);
		}
		else {
			checkSense1 = false;
			toggleOutput1 = false;
			arduino.digitalWrite (ledPin1, Arduino.LOW);
		}

		if (senseVal2 > 400) {
			checkSense2 = true;
			toggleOutput2 = true;
			arduino.digitalWrite (ledPin2, Arduino.HIGH);
		}
		else {
			checkSense2 = false;
			toggleOutput2 = false;
			arduino.digitalWrite (ledPin2, Arduino.LOW);
		}
		if (senseVal3 > 400) {
			checkSense3 = true;
			toggleOutput3 = true;
			arduino.digitalWrite (ledPin3, Arduino.HIGH);
		}
		else {
			checkSense3 = false;
			toggleOutput3 = false;
			arduino.digitalWrite (ledPin3, Arduino.LOW);
		}
		
		if (senseVal4 > 400) {
			checkSense4 = true;
			toggleOutput4 = true;
			arduino.digitalWrite (ledPin4, Arduino.HIGH);
		}
		else {
			checkSense4 = false;
			toggleOutput4 = false;
			arduino.digitalWrite (ledPin4, Arduino.LOW);
		}
		if (senseVal5 > 400) {
			checkSense5 = true;
			toggleOutput5 = true;
			arduino.digitalWrite (ledPin5, Arduino.HIGH);
		}
		else {
			checkSense5 = false;
			toggleOutput5 = false;
			arduino.digitalWrite (ledPin5, Arduino.LOW);
		}
		
		GUILayout.EndArea();
		
	}

	// Update is called once per frame
	void Update () {
	}
}