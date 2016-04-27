using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
	public AudioSource aud;
	public Animation anim1;
	public Animation anim2;
	public Animation anim3;
	public Animation anim4;
	public Animation anim5;
	public Light light;
   



	// Use this for initialization
	void Start () {
		//aud = gameObject.GetComponent<AudioSource> ();
		//aud=gameObject.GetComponent<AudioSource>();
		gameObject.GetComponent<Animation>();
		gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {

		flicker_lights_off ();
	}

	 void OnTriggerEnter(Collider other){
		Debug.Log ("inside trigger");
		flicker_lights_on ();
		if (other.gameObject.tag == "Player") {
			aud.Play ();
			anim1.CrossFadeQueued ("celebration");
			anim2.CrossFadeQueued ("celebration2");
			anim3.CrossFadeQueued ("celebration3");
			anim4.CrossFadeQueued ("applause");
			anim5.CrossFadeQueued ("applause2");

		}
	}

				IEnumerator flicker_lights_on(){
		Debug.Log("inside flicker on");
		for(int i=1;i>0;i++){
			light.intensity += 1;
			yield return new WaitForSeconds (1);
	}
	}
		IEnumerator flicker_lights_off(){
		for (int j = 1; j > 0; j++) {
			light.intensity = 0;
			yield return new WaitForSeconds (1);
		}
	}
				
				
}
