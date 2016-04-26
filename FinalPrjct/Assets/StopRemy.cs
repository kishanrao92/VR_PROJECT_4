using UnityEngine;
using System.Collections;

public class StopRemy : MonoBehaviour {

	Animator anim;
	public iTweenEvent spvar;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		spvar = GetComponent<iTweenEvent> ();

	}

	void OnTriggerEnter (Collider col){
		spvar.enabled = false;
		anim.SetTrigger ("isDancing");

	}
	// Update is called once per frame
	void Update () {
	}
}
