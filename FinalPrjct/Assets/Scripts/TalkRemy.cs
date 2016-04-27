using UnityEngine;
using System.Collections;

public class TalkRemy : MonoBehaviour {

	public Animator anim;
	public iTweenEvent scvar;

	// Use this for initialization
	void Start(){
		anim = GetComponent<Animator> ();
		scvar = GetComponent<iTweenEvent> ();
		StartCoroutine ("text1");
	}

	IEnumerator text1(){
		yield return new WaitForSeconds (5.0f);
		GameObject txtbx = GameObject.FindGameObjectWithTag ("text1");
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		//txtbx.transform.position = cam.transform.position + new Vector3 (9.61f, -4.0f, -9.14f);
		txtbx.transform.position = new Vector3(-13.78f,5.13f,-8.98f);
		//txtbx.transform.rotation = new Quaternion (25.17577f, 180.0f, 1.349529e-12f);

		anim.SetTrigger ("isTalking");

		Destroy (txtbx, 7.0f);
		StartCoroutine ("move1");
	}

	IEnumerator move1(){
		yield return new WaitForSeconds (8.0f);

		anim.SetTrigger ("isWalking");
		scvar.enabled = true;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
