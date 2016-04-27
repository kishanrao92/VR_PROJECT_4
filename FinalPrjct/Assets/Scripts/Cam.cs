using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour 
{
	private const int RIGHT_ANGLE = 90; 

	private Rigidbody rb;

	public int check = 0;

	public GameObject sphere;


	// This variable determinates if the player will move or not 
	public bool isWalking = false;
	public Vector3 direction;

	CardboardHead head = null;

	//This is the variable for the player speed
	[Tooltip("With this speed the player will move.")]
	public float speed;

	[Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
	public bool walkWhenTriggered;

	[Tooltip("Activate this checkbox if the player shall move when he looks below the threshold.")]
	public bool walkWhenLookDown;

	[Tooltip("This has to be an angle from 0° to 90°")]
	public double thresholdAngle;

	[Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
		"For example in the case of you have no collider attached to your CardboardMain-GameObject" +
		"and you want to stay in a fixed level.")]
	public bool freezeYPosition; 

	[Tooltip("This is the fixed y-coordinate.")]
	public float yOffset;

	void Start () 
	{
		head = Camera.main.GetComponent<StereoController>().Head;
		rb = sphere.GetComponent<Rigidbody> ();
	}

	void Update () 
	{
		// Walk when the Cardboard Trigger is used 
		if (walkWhenTriggered && !walkWhenLookDown && !isWalking && Cardboard.SDK.Triggered && check == 0) 
		{
			isWalking = true;
		} 
		else if (walkWhenTriggered && !walkWhenLookDown && isWalking && Cardboard.SDK.Triggered) 
		{
			isWalking = false;
		}

		// Walk when player looks below the threshold angle 
		if (walkWhenLookDown && !walkWhenTriggered && !isWalking &&  
			head.transform.eulerAngles.x >= thresholdAngle && 
			head.transform.eulerAngles.x <= RIGHT_ANGLE) 
		{
			isWalking = true;
		} 
		else if (walkWhenLookDown && !walkWhenTriggered && isWalking && 
			(head.transform.eulerAngles.x <= thresholdAngle ||
				head.transform.eulerAngles.x >= RIGHT_ANGLE)) 
		{
			isWalking = false;
		}

		// Walk when the Cardboard trigger is used and the player looks down below the threshold angle
		if (walkWhenLookDown && walkWhenTriggered && !isWalking &&  
			head.transform.eulerAngles.x >= thresholdAngle && 
			Cardboard.SDK.Triggered &&
			head.transform.eulerAngles.x <= RIGHT_ANGLE) 
		{
			isWalking = true;
		} 
		else if (walkWhenLookDown && walkWhenTriggered && isWalking && 
			head.transform.eulerAngles.x >= thresholdAngle &&
			(Cardboard.SDK.Triggered || 
				head.transform.eulerAngles.x >= RIGHT_ANGLE)) 
		{
			isWalking = false;
		}

		if (isWalking) 
		{
			direction = new Vector3 (head.transform.forward.x, 0, head.transform.forward.z);
			//Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * speed * Time.deltaTime;
			Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
			//transform.Translate(rotation * direction);
		}

		if(freezeYPosition)
		{
			//transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
		}
	}

	void FixedUpdate (){

		if (isWalking) {
			rb.constraints = RigidbodyConstraints.None;
			rb.AddForce (direction * speed);
		} 
		else 
		{
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}

	}
}