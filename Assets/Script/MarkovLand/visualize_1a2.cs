using UnityEngine;
using System.Collections;

public class visualize_1a2 : MonoBehaviour
{

	public Transform node;
	float space = 1.5f;
	public GameObject[,] grid = new GameObject[4,4];



	void Start() {
		for (int y = 0; y < 4; y++) {
			for (int x = 0; x < 4; x++) {
				grid[x,y] = Instantiate(node, new Vector3(x*space, y*space, 0), Quaternion.identity) as GameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
}

