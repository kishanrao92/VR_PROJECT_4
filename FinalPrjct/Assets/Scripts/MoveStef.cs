using UnityEngine;
using System.Collections;

public class MoveStef : MonoBehaviour {

	Animator anim;
	public iTweenEvent setscr;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		setscr = GetComponent<iTweenEvent> ();

	}



	// Update is called once per frame
	void Update () {
	
	}
}
