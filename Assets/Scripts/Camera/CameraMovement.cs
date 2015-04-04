using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	// Characters
	private GameObject[] Players = new GameObject[2];
	
	// Distance
	internal float fCamDistMin = 0.6f;
	internal float fCamDistMax = 11.0f;
	public float distance;
	public Vector3 inbetween;
	public float newDistance;
	Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		// Find Characters
		Players[0] = GameObject.FindWithTag("Player01");
		Players[1] = GameObject.FindWithTag("Player02");

		// Set Distance settings + Where Camera should be position inbetween the Characters
		fCamDistMin = 0.6f;
		fCamDistMax = 11.0f;
	//	distance = Vector3.Distance (Players[0].transform.position, Players[1].transform.position) * 0.5f;
		distance = MyDistance(Players[0].transform.position.x, Players[1].transform.position.x);
		inbetween = (Players[0].transform.position + Players[1].transform.position);

		//
		offset = transform.position - inbetween;
		//
	}

	void LateUpdate()
	{
		
		inbetween = (Players[0].transform.position + Players[1].transform.position) * 0.5f;
	//	distance = Vector3.Distance (Players[0].transform.position, Players[1].transform.position);
		distance = MyDistance(Players[0].transform.position.x, Players[1].transform.position.x);

		Vector3 desiredPosition = inbetween - offset;
		desiredPosition.y = 1;
		desiredPosition.z = -6;

		if(distance > fCamDistMin && distance < fCamDistMax)
			transform.position = desiredPosition;
	}
	
	/*
	 * Gets distance between x Axis
	 */
	static float MyDistance(float First, float Second)
	{
		float NewDistance = First - Second;
		return Mathf.Sqrt (NewDistance * NewDistance);
	}
}
