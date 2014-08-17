using UnityEngine;
using System.Collections;

public class particleEffectScript : MonoBehaviour {

	ParticleSystem partSys;

	// Use this for initialization
	void Start () {
		partSys = GetComponent<ParticleSystem> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (partSys.isPlaying) {
				}
		else{
			Destroy (this.gameObject);
		}
	
	}
}
