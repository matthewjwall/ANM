using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class visualize_1a2 : MonoBehaviour
{

	public Transform node;
	float space = 1.5f;
	public GameObject[,] grid = new GameObject[4,4]; 
	GameObject[] nodeGrid; 
	private MusicPlayer getMyPitch;
	Color32 cOn = Color.white; 
	Color32 cOff = Color.red; 


	void Start() {
		//nodeArr.AddRange(GameObject.FindGameObjectsWithTag("nodePoint"));
		griddr ();
		getMyPitch = GameObject.Find("Main Camera").GetComponent<MusicPlayer>();
		nodeGrid = GameObject.FindGameObjectsWithTag("nodePoint"); 
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		StartCoroutine("nodeColor"); 
	}

	void griddr ()
	{
		for (int y = 0; y < 4; y++) {
			for (int x = 0; x < 4; x++) {
				grid[x,y] = Instantiate(node, new Vector3(x*space, y*space, 0), Quaternion.identity) as GameObject;
			}
		}
	}

	private IEnumerator nodeColor()
	{
		nodeGrid [(int)getMyPitch.myPitch].gameObject.GetComponent<Renderer>().material.color = cOn;
		yield return new WaitForSeconds(getMyPitch.durational);
		StartCoroutine ("clearGrid"); 
	}

	private IEnumerator clearGrid()
	{
		StopCoroutine ("nodeColor"); 
		for (int i = 0; i < nodeGrid.Length; i++) {
			nodeGrid [i].gameObject.GetComponent<Renderer> ().material.color = cOff;
		}
		yield return new WaitForSeconds(0f);
	}
}

