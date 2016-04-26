using UnityEngine;
using System;
using System.Collections;

public class GunController : MonoBehaviour 
{
	public GameObject gunEnd;
	public float rotationSpeed;
	public float aimOff;				//controls how off target the aim is

	private ShootingController shooting;
	private GameObject player;
	private System.Random random;

	// Use this for initialization
	void Start () 
	{
		shooting = gunEnd.GetComponent <ShootingController>();
		player = GameObject.FindWithTag("Player");
		random = new System.Random ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 playerPos = player.transform.position;
		int aim = (int)(aimOff * 100);
		float x = (random.Next (0, aim * 2 + 1) - aim)/100.0f;
		float y = (random.Next (0, aim * 2 + 1) - aim)/100.0f;
		float z = (random.Next (0, aim * 2 + 1) - aim)/100.0f;
		playerPos.x += x;
		playerPos.y += y;
		playerPos.z += z;

		Vector3 heading = playerPos - transform.position;
		float distance = heading.magnitude;
		Vector3 playerDir = heading / distance;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerDir), Time.deltaTime * rotationSpeed);			
	}

	public void startShooting()
	{
		shooting.startShooting ();
	}

	public void stopShooting()
	{
		shooting.stopShooting ();
	}
}
