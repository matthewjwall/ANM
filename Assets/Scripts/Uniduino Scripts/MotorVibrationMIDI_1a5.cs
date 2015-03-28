using UnityEngine;
using System.Collections;
using Uniduino;

public class MotorVibrationMIDI_1a5 : MonoBehaviour {

	public Arduino arduino;

	public int sensePin1 = 0;
	public int motorPin1= 8;

	public int senseVal1;
	public bool checkSense1 = false;
	public bool checkSense2 = false;

	float timer1 = 0.0f;
	float timer2 = 0.0f;

	public MidiChannel channel1 = MidiChannel.Ch1;

	public int noteNumber1;
	public int noteNumber2;

	public float velocity1 = 1.0f;
	public float velocity2 = 0.5f;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Log = (s) => Debug.Log("Arduino: " +s);
		arduino.Setup(ConfigurePins);

		StartCoroutine (motorOn1 (checkSense1));

		noteNumber1 = RandomNoteGetter1();
		noteNumber1 = RandomNoteGetter2();

	}

	void ConfigurePins()
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
				yield return new WaitForSeconds(0.5f);
			}
			else {
				for(int i = 0; i < 128; i++){
					MidiOut.SendNoteOff (channel1, i);
				}
				yield return null;
			}

			if (checkSense2 == true) {
				
				arduino.digitalWrite(motorPin1, Arduino.HIGH);
				MidiOut.SendNoteOn (channel1, noteNumber2, velocity1);
				yield return new WaitForSeconds(0.5f);

				arduino.digitalWrite(motorPin1, Arduino.LOW);
				yield return new WaitForSeconds(0.25f);
			}
			else {
				for(int i = 0; i < 128; i++){
					MidiOut.SendNoteOff (channel1, i);
				}
				yield return null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer1 += Time.deltaTime;
		if(timer1 > 20f)
		{
			noteNumber1 = RandomNoteGetter1 ();
			timer1 = 0.0f;
		}
		timer2 += Time.deltaTime;
		if(timer2 > 10f)
		{
			noteNumber2 = RandomNoteGetter2 ();
			timer2 = 0.0f;
		}


	}

	int RandomNoteGetter1() {
		int random1 = 1;
		random1 = Random.Range (40,60);
		return random1;
	}
	int RandomNoteGetter2() {
		int random2 = 1;
		random2 = Random.Range (60,80);
		return random2;
	}
}
