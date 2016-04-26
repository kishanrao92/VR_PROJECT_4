using UnityEngine;
using System.Collections;

public class MainSounds : MonoBehaviour 
{
     public  AudioSource[] mainSounds; // 0 and 1 are ambience
	// Use this for initialization
	void Start () 
    {
        mainSounds = this.GetComponents<AudioSource>();
        mainSounds[0].loop = true;
        mainSounds[0].Play();

        mainSounds[1].loop = true;
        mainSounds[1].Play();

	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
