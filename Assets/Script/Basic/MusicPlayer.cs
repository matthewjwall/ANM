using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class MusicPlayer : MonoBehaviour {
	
	private int channelcount = 1;
	public MarkovPlay markovplay;
	public float plength = 10.0f;
	public float blength = 10.0f;
	public float bprob = 10.0f;

	private List <MelodyGenerator> generators;
	

	public void Start()
	{
		for (int i=0;i<channelcount;i++)
		{
			AudioSource a_s = this.gameObject.AddComponent<AudioSource>();
			a_s.panStereo=0;
			a_s.spatialBlend=0;
			a_s.loop=false;
			a_s.playOnAwake=false;
		}
		
		generators = new List<MelodyGenerator>();
		//new Jazzy();
		//generators.Add(new Jazzy());
		markovplay = new MarkovPlay();
		//phraselength = markovplay.phraselength;
		generators.Add(markovplay);
		//generators.Add(new Species_Generator());
		//generators.Add(new MotivicGenerator());
			
		NewMelody();
	}
	
	public void NewMelody()
	{
		StopCoroutine("GenerateMelody");
		StartCoroutine("GenerateMelody");
	}
	
	private IEnumerator GenerateMelody()
	{
		GenericMelody melody = Utility.RandomElement(generators).GenerateMelody();
		StopCoroutine("PlayMelody");
		StartCoroutine("PlayMelody",melody);
		yield break;
	}
	
	private IEnumerator PlayMelody(GenericMelody melody)
	{
		int pitchindex=0;
		int durationindex=0;
		int velocityindex=0;
		int sampleindex=0;		
		int channelindex=0;
		
		AudioSource[] audiosources = GetComponents<AudioSource>();
		
		while(true)
		{
			float pitch = melody.frequencies[pitchindex];
			float duration = melody.durations[durationindex];
			float velocity = melody.volumes[velocityindex]/2;
			AudioClip sample = melody.sounds[sampleindex];
			
			AudioSource a_s;
			if (melody.specifychannels)
			{
				a_s = audiosources[melody.channels[channelindex]];
			}
			else
			{
				a_s = audiosources[channelindex];
			}
			
			a_s.pitch=pitch;
			a_s.volume=velocity;
			//produces clicks :(
			//if (melody.clearchannelonretrigger)
			//	a_s.Stop();
			a_s.PlayOneShot(sample);
			yield return new WaitForSeconds(duration);
			
			pitchindex = (pitchindex+1)%melody.frequencies.Count;
			durationindex = (durationindex+1)%melody.durations.Count;
			velocityindex = (velocityindex+1)%melody.volumes.Count;
			sampleindex = (sampleindex+1)%melody.sounds.Count;
			if (melody.specifychannels)
			{
				channelindex = (channelindex + 1)%melody.channels.Count;
			}
			else
			{
				channelindex = (channelindex + 1)%audiosources.Length;
			}
		}
	}
	
	public void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			NewMelody();
			//Debug.Log (markovplay.phraselength); 
		}
		markovplay.phraselength = plength;
		markovplay.barlength = blength;
		markovplay.barprob = bprob; 
	}
	

	void OnGUI()
	{
		plength = GUI.VerticalSlider(new Rect(25, 25, 10, 150), plength, 35.0F, 1.0F);
		blength = GUI.VerticalSlider(new Rect(90, 25, 10, 150), blength, 35.0F, 1.0F);
		bprob = GUI.VerticalSlider(new Rect(155, 25, 10, 150), bprob, 35.0F, 1.0F);
	}
}
