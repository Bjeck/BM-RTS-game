using UnityEngine;
using System.Collections;

public class TestSpawn : MonoBehaviour {
	
	public GameObject otherCube; 
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space)){		
			print ("SPACE!");
			Instantiate(gameObject, new Vector3(0,0,0), Quaternion.identity);
		}
	}
}
