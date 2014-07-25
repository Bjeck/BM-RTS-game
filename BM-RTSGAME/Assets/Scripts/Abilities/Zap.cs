using UnityEngine;
using System.Collections;

public class Zap : DirectTarget {

	public ParticleSystem zapEffect;


	// Use this for initialization
	public void Start () {
		base.Start ();

		//Debug.Log ("START FROM ZAP");
		keyToUse = KeyCode.Z;
		damage = 100;
		range = 10f;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("ZAP EFFECT: "+zapEffect);


		base.Update ();

		if (isTargeting) {
			if (Input.GetMouseButtonDown (0)) {

				if(dist > range){
					//Debug.Log("OUT OF RANGE!");
					return;
				}
				if (GUIUtility.hotControl == 0) { //Check if there is a GUI Element under the mouse. If not, continue with the Raycasting.
					Vector3 mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					RaycastHit hit;
					LayerMask layermaskU = (1 << 12);
					if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermaskU)) {
						zap(hit.transform.gameObject);
					}
				}
			}
		}
	}

	public override void Do(){
		base.Do ();
		//Debug.Log ("I'M READY TO ZAP");
		if (isTargeting) {
			return;		
		}
		isTargeting = true;
	}

	public void zap(GameObject t){
		//Debug.Log ("YOU GOT ZAPPED!");
		target = t;
		t.GetComponent<Unit>().health -= damage;
		zapEffect = (ParticleSystem)Instantiate (zapEffect);
		zapEffect.transform.position = targetCursor.transform.position;
		zapEffect.Play ();
		//if (zapEffect.isPlaying)
		//Debug.Log ("IS PLAYING PARTICLES");

		StopTargeting ();
	}
}
