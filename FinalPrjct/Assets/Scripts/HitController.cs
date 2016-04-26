using UnityEngine;
using System.Collections;

public class HitController : MonoBehaviour 
{
	public GameObject damageParticles;
	public GameObject bulletHole;
	public GameObject parent;
	public int objectType;					//what object is this script running on? 0 = player car doors, 1 = everywhere else on the car, 2 = person



	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void getHit(Vector3 hitPoint, bool inst)
	{	
		if (objectType == 0) 
		{	
			GameObject temp1 = null;
			GameObject temp2 = null;
			if(inst)
				temp1 = Instantiate (damageParticles, hitPoint, transform.rotation) as GameObject;
			temp2 = Instantiate (bulletHole, hitPoint, parent.transform.rotation) as GameObject;
			if (temp1 != null) 
				temp1.transform.parent = parent.transform;
			if (temp2 != null) 
				temp2.transform.parent = parent.transform;
		} 
		else if (objectType == 1 || objectType == 2) 
		{
			GameObject temp1 = null;
			if(inst)
				temp1 = Instantiate (damageParticles, hitPoint, transform.rotation) as GameObject;
			if (temp1 != null) 
				temp1.transform.parent = parent.transform;
		}
	}
}
