using UnityEngine;
using System.Collections;
using Uniduino;

public class MotorVibrationMIDI_1a7 : MonoBehaviour {

	public Arduino arduino;

	public int sensePin1 = 0;
	public int motorPin1= 8;

	public int senseVal1;
	public bool checkSense1 = false;
	public bool checkSense2 = false;

	float timer1 = 0.0f;

	public MidiChannel channel1 = MidiChannel.Ch1;
	public int noteNumber1;
	public int noteNumber2 = 1;
	public int noteNumber3 = 2;
	public float velocity1 = 1.0f;
	public float velocity2 = 0.5f;

	// Use this for initialization
	void Start () {

		arduino = Arduino.global;
		arduino.Log = (s) => Debug.Log("Arduino: " +s);
		arduino.Setup(ConfigurePins);

		StartCoroutine (motorOn1 (checkSense1));
		StartCoroutine (MIDIOn());

		noteNumber1 = RandomNoteGetter1();
	
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

	IEnumerator MIDIOn () {
		while(true){
			MidiOut.SendNoteOn (channel1, noteNumber1, velocity1);
			yield return new WaitForSeconds (40);
			for(int i = 0; i < 127; i++){
				MidiOut.SendNoteOff (channel1, i);
			}
		}
	}

	int RandomNoteGetter1() {
		int random1 = 1;
		random1 = Random.Range (40,60);
		return random1;
	}

	IEnumerator motorOn1(bool checkSense1) {

		while (true) {

			if (senseVal1 > 200 && senseVal1 < 600) {
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
				MidiOut.SendNoteOn (channel1, noteNumber3, velocity1);
				yield return new WaitForSeconds(0.5f);

				arduino.digitalWrite(motorPin1, Arduino.LOW);
				yield return new WaitForSeconds(1.0f);
			}

			if (checkSense2 == true) {
				
				arduino.digitalWrite(motorPin1, Arduino.HIGH);
				MidiOut.SendNoteOn (channel1, noteNumber3, velocity1);
				yield return new WaitForSeconds(0.25f);

				arduino.digitalWrite(motorPin1, Arduino.LOW);
				yield return new WaitForSeconds(0.5f);
			}

			else {
				MidiOut.SendNoteOff (channel1, noteNumber2);
				MidiOut.SendNoteOff (channel1, noteNumber3);
				yield return null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		timer1 += Time.deltaTime;
		if(timer1 > 40f)
		{
			noteNumber1 = RandomNoteGetter1 ();
			timer1 = 0.0f;
		}
	
	}
}
