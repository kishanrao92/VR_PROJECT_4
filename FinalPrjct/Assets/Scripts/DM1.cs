using UnityEngine;
using System.Collections;

public class DM1 : MonoBehaviour {

	public GameObject player;
	public Autowalk setscr;

	// Use this for initialization
	void Start () {
		setscr = GetComponent<Autowalk> ();
		//anim = GetComponent<Animator> ();
	}

	IEnumerator text2(){
		yield return new WaitForSeconds (2.0f);


		GameObject txt = GameObject.FindGameObjectWithTag ("text2");
		txt.transform.position = new Vector3 (55.0f, 6.0f, 32.0f);
	
		StartCoroutine ("choice");
	}

	IEnumerator choice(){
		yield return new WaitForSeconds (5.0f);

		GameObject wtr = GameObject.FindGameObjectWithTag ("Water");
		GameObject whsky = GameObject.FindGameObjectWithTag ("Whiskey");

		wtr.transform.position = new Vector3 (50.0f, 13.71f, 27.0f);
		whsky.transform.position = new Vector3 (50.0f, 13.71f, 29.0f);
	}


	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		
		//Debug.Log ("Inside collider");

		if (col.tag == "stop") {
			setscr.enabled = false;
			transform.position = new Vector3 (55.0f,16.0f,25.0f);
			transform.Rotate (0, 180, 0);
			StartCoroutine ("text2");
		}
	}

}