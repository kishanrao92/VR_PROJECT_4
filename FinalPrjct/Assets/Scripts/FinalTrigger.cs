using UnityEngine;
using System.Collections;

public class FinalTrigger : MonoBehaviour
{
    public AudioSource final;
    public AudioClip sFinal;

	// Use this for initialization
	void Start () 
    {
        final = this.GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Car_Move") ;
        final.PlayOneShot(sFinal, 1);
    }
}
