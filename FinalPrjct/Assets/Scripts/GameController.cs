using UnityEngine;
using System.Collections;

    public class GameController : MonoBehaviour
    {

    public AudioSource final;
    public AudioClip sFinal;
    

    
    private GameObject[] guns;
        
   

        
        // Use this for initialization
        void Start()
        {
            guns = GameObject.FindGameObjectsWithTag("Gun");
        final = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    

void OnTriggerEnter(Collider other)
    {
        final.PlayOneShot(sFinal, 1);
        for (int i = 0; i < guns.Length; i++)
        {

            GunController gun = guns[i].GetComponent<GunController>();
            gun.startShooting();
        }
        
       

    }
    }
