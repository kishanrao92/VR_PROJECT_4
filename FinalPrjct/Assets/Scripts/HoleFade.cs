using UnityEngine;
using System.Collections;

public class HoleFade : MonoBehaviour 
{
	public float fadeTime;				//in seconds
	public float delay;

	private MeshRenderer renderer;
	private Color col;
	private float decrease;
	private float sTime;

	// Use this for initialization
	void Start () 
	{
		renderer = GetComponent <MeshRenderer>();
		col = renderer.material.color;
		decrease = col.a / fadeTime*1;
		sTime = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time >= sTime) 
		{
			col.a -= decrease * Time.deltaTime;
			renderer.material.color = col;
		}
	}
}
