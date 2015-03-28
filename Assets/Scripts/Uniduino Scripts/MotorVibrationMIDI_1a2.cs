using UnityEngine;
using System.Collections;
using Uniduino;

public class MotorVibrationMIDI_1a2 : MonoBehaviour {

	public Arduino arduino;

	public int sensePin1 = 0;
	public int motorPin1= 8;

	public int senseVal1;
	public bool checkSense1 = false;
	public bool checkSense2 = false;

	public MidiChannel channel1 = MidiChannel.Ch1;
	public int noteNumber1 = 56;
	public int noteNumber2 = 68;
	public float velocity1 = 1.0f;

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
		GUILayout.HorizontalSlider(senseVal1, 0, 940, GUILayout.Height(21), GUILayout.Width(150));

		if (senseVal1 > 400 && senseVal1 < 600) {
			checkSense1 = true;
		}
		else {
			checkSense1 = false;
		}
		if (senseVal1 > 600) {
			checkSense2 = true;
		}
		else {
			checkSense2 = false;
		}
		
		GUILayout.EndArea();
		
	}

	IEnumerator motorOn1(bool checkSense1) {

		while (true) {

			if (senseVal1 > 400 && senseVal1 < 600) {
				checkSense1 = true;
			}
			else {
				checkSense1 = false;
			}

			if (senseVal1 > 600) {
				checkSense2 = true;
			}
			else {
				checkSense2 = false;
			}

			if (checkSense1 == true) {

				arduino.digitalWrite(motorPin1, Arduino.HIGH);
				MidiOut.SendNoteOn (channel1, noteNumber1, velocity1);
				yield return new WaitForSeconds(1.0f);

				arduino.digitalWrite(motorPin1, Arduino.LOW);
				MidiOut.SendNoteOff (channel1, noteNumber1);
				yield return new WaitForSeconds(0.5f);
			}
			else {
				yield return null;
			}

			if (checkSense2 == true) {
				
				arduino.digitalWrite(motorPin1, Arduino.HIGH);
				MidiOut.SendNoteOn (channel1, noteNumber2, velocity1);
				yield return new WaitForSeconds(0.5f);
				
				arduino.digitalWrite(motorPin1, Arduino.LOW);
				MidiOut.SendNoteOff (channel1, noteNumber2);
				yield return new WaitForSeconds(0.25f);
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
