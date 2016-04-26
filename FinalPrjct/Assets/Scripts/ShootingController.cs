using UnityEngine;
using System;
using System.Collections;

public class ShootingController : MonoBehaviour 
{
	public ParticleSystem gunParticles;                   
	public Light shotLight;
	public double timeBShots;
	public int range;
	public double effectDuration;			//must be less than timeBShots
	public int gunType;						//0 = tommy gun, 1 = shotgun, 2 = revolver
    
    public bool __________Sound_Variables________;
    public AudioSource[] guns;
    public AudioSource[] hits;                      
    public AudioClip[] sGunBank;                  // 0 - 4 revolver, 5-9 shotgun, 10-16 tommy gun
    public AudioClip[] sHitBank;                  // 0 - 4 fabric, 5 - 10 glass, 11 - 20 Metal, 21 - 25 ricochets
    public float gunPitchRange = 1.55f;          // varies the pictch of gunshots, 1.5 - 3 is decent or it gets all weird
    public float gunVolMin = .9f;
    public float gunVolMax = 1;                 //varies the volume of gunshots, anything less than .6 and it sounds fluttery
    public float spreadMin = 10;                //sets the spread of shots across an area, making it sound thicker
    public float spreadMax = 100;
    public float hitPitchRange = 3;             //varies hit ounds 
    public float hitVolMin = .75f;
    public float hitVolMax = 1;
    public bool isRandomized = true;
    public bool __________________________________;
	
    private double accTime;		//used to enforce fire rate
	private double accTime2;	//for effect duration
	private bool shooting;
	private bool shotEffects;	//true when shot effects are turned on
	private int playerMask;
	private Ray shootRay;
	private ParticleSystem.EmissionModule shotParticles;

	// Use this for initialization
	void Start () 
	{
        //sound functions 
        sGunBank = GunsBank.S.sGunBank;
        sHitBank = HitsBank.S.sHitBank;
        guns = GameObject.FindGameObjectWithTag("GunMgr").GetComponents<AudioSource>();  //for more polyphony, add AudioSource componentes to the Gun or Hit bank prefabs
        hits = GameObject.FindGameObjectWithTag("HitMgr").GetComponents<AudioSource>();
		accTime = 0;
		accTime2 = effectDuration;
		shooting = false;
		shotEffects = false;
		shotParticles = gunParticles.emission;
		shotParticles.enabled = false;
		gunParticles.Play ();
		playerMask = LayerMask.GetMask ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shotEffects) 											//turn off shot effects if they were turned on
		{
			accTime2 -= Time.deltaTime;

			if (accTime2 <= 0) 
			{										
				shotLight.enabled = false;
				shotParticles.enabled = false;
				shotEffects = false;
				accTime2 = effectDuration;
			}
		}
		if (shooting)
		{
			accTime -= Time.deltaTime;

			if (accTime <= 0)										//time to shoot a "bullet"	
			{
				shotEffects = true;
				shotLight.enabled = true;
                MakeSounds();                                       //TRIGGER GUNSHOT SOUND FROM HERE
				shotParticles.enabled = false;
				shotParticles.enabled = true;

				shootRay.origin = transform.position;
				shootRay.direction = transform.forward;

				RaycastHit[] hits;
				hits = Physics.RaycastAll (shootRay, range, playerMask);
				for (int i = 0; i < hits.Length; i++) 
				{
					HitController playerHit = hits[i].collider.GetComponent <HitController>();
					if (gunType == 0) 
					{
						System.Random random = new System.Random ();
						int num = random.Next (0, 5);
						if (num == 0) 
							playerHit.getHit (hits [i].point, true);
						else
							playerHit.getHit (hits [i].point, false);
					}
					else
						playerHit.getHit (hits [i].point, true);
				}
				if (gunType == 0)
					accTime = timeBShots;
				else 
				{
					System.Random random = new System.Random ();
					int min = (int) (timeBShots * 100 * 0.75);
					int max = (int) (timeBShots * 100 * 1.25);
					accTime = random.Next (min, max)/100.0;
					print ("\nWait Time: " + accTime);
				}
			}
		}		
	}

    int gunIndexCounter = 0;    //makes sure the oldest sound is replaced
    int hitIndexCounter = 0;
    public void MakeSounds()
    {
        int sGunMin;
        int sGunMax;
        int sHitRange;
        switch (gunType)
        {
            case 0: //tommy gun
                sGunMin = 10;
                sGunMax = 16;
                sHitRange = 24; 
                break;
            case 1: //shotgun
                sGunMin = 5;
                sGunMax = 9;
                sHitRange = 18;
                break;
            case 2: //revolver and default if nothing is assigned
            
            default:
                sGunMin = 0;
                sGunMax = 4;
                sHitRange = 14; // add the ricochets if desired
            break;
        }

        if (isRandomized)
        {
            RandomizeWeapons(sGunMin, sGunMax, sHitRange, gunIndexCounter, hitIndexCounter);
        }
        else 
        {
            int gunSoundIndex = UnityEngine.Random.Range(sGunMin, sGunMax);//shoots random sounds without any modifications
            int hitSoundIndex = UnityEngine.Random.Range(0, sHitRange);

            guns[gunIndexCounter].PlayOneShot(sGunBank[gunSoundIndex]);
            hits[hitIndexCounter].PlayOneShot(sHitBank[hitSoundIndex]);
        }

        gunIndexCounter++;
        gunIndexCounter++;

        if (gunIndexCounter == (guns.Length - 1))           //resets the counter to replace the oldest sound on the next call
            gunIndexCounter = 0;
        if (hitIndexCounter == (hits.Length - 1))
            hitIndexCounter = 0;
    }

    public void RandomizeWeapons(int sGunMin, int sGunMax, int sHitRange, int gunIndex, int hitIndex)
    {
        float gunPitch = UnityEngine.Random.Range(-gunPitchRange, gunPitchRange);
        float gunVol = UnityEngine.Random.Range(gunVolMin, gunVolMax);
        float spread = UnityEngine.Random.Range(spreadMin, spreadMax); //spread is = to both due to location source
        int gunSoundIndex = UnityEngine.Random.Range(sGunMin, sGunMax);
        
        guns[gunIndex].pitch = gunPitch;
        guns[gunIndex].volume = gunVol;
        guns[gunIndex].spread = spread;
        guns[gunIndex].PlayOneShot(sGunBank[gunSoundIndex]);

        float hitPitch = UnityEngine.Random.Range(-hitPitchRange, hitPitchRange);
        float hitVol = UnityEngine.Random.Range(hitVolMin, hitVolMax);
        int hitSoundIndex = UnityEngine.Random.Range(0, sHitRange);
       
        hits[hitIndex].pitch = hitPitch;
        hits[hitIndex].volume = hitVol;
        hits[hitIndex].spread = spread;
        hits[hitIndex].PlayOneShot(sHitBank[hitSoundIndex]);

    }

	public void startShooting()
	{
		shooting = true;
		accTime = 0;
	}

	public void stopShooting()
	{
		shooting = false;
		accTime = 0;
	}
}
