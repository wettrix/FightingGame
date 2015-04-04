using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	private CameraMovement cCamMove;
	private TurnPosition cTP;
	private GameObject MyObject;

	public float WalkSpeed = 4.0f;
	public float JumpSpeed = 200.0f;
	public float JumpLength = 0.4f;

	// Camera Distance
	private float distance;
	private float fCamDistMin;
	private float fCamDistMax;

	internal bool jumping = false;
	
	public KeyCode MoveLeftBtn;
	public KeyCode MoveRightBtn;
	public KeyCode JumpBtn;

	// TEST STUFF
	private GameObject[] InvisibleWall = new GameObject[2];

	// Use this for initialization
	void Start ()
	{
		MyObject = GameObject.Find("Characters");
		cTP = MyObject.GetComponent<TurnPosition>();

		// Set Camera + Camera Min/Max distance
		cCamMove = Camera.main.GetComponent<CameraMovement>();
		fCamDistMin = cCamMove.fCamDistMin;
		fCamDistMax = cCamMove.fCamDistMax;

		int idx = 0;
		foreach(GameObject Wall in GameObject.FindGameObjectsWithTag("WallTouch"))
		{
			InvisibleWall[idx] = Wall;
			idx++;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Update Distance between chars
		distance = cCamMove.distance;
		// Store Velocity Y

		// One press
		if(!jumping)
		{
			// Jump Right 
			if(Input.GetKeyDown(JumpBtn) && Input.GetKey(MoveRightBtn))
			{
				jumping = true;
				rigidbody.AddForce(new Vector3(JumpLength,1,0) * JumpSpeed);
				transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			// Jump Left
			else if(Input.GetKeyDown(JumpBtn) && Input.GetKey(MoveLeftBtn))
			{
				jumping = true;
				rigidbody.AddForce(new Vector3(-JumpLength,1,0) * JumpSpeed);
				transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
			}
			// Jump
			else if(Input.GetKeyDown(JumpBtn) )
			{
				jumping = true;
				rigidbody.AddForce(Vector3.up * JumpSpeed);
				transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
			}
		}


		// If player is in opposite direction go other way
		if(transform.eulerAngles.y == 0)
		{
			// If Char is on Left Side
			if(Input.GetKey(MoveLeftBtn)){
				if(!jumping && distance < fCamDistMax)
				transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
			}

			if(Input.GetKey(MoveRightBtn)){
				if(!jumping)
				transform.Translate(Vector3.right * Time.deltaTime * WalkSpeed);
			}
		}
		else
		{
			// If Char is on Right side
			if(Input.GetKey(MoveLeftBtn)){
				if(!jumping)
					transform.Translate(Vector3.right * Time.deltaTime * WalkSpeed);
			}
			
			if(Input.GetKey(MoveRightBtn)){
				if(!jumping && distance < fCamDistMax)
					transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
			}
		}

		// If we go to 11 stop Transform
		if(distance > fCamDistMax)
		{
			if(jumping)
			{
			//	transform.Translate(0.05f, 0, 0);
				transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
				rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
			}
			transform.Translate(Vector3.zero * Time.deltaTime * WalkSpeed);
		}

		// so he doesn't tip over
		if(!jumping)
			transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

		// TEST VERSION
		for(int i = 0; i < InvisibleWall.Length; i++)
		{
			float WallDistance;
			WallDistance = MyDistance(InvisibleWall[i].transform.position.x,transform.position.x);

			if(WallDistance < 1.0f)
			{
				Debug.Log ("MYCHAR= " +WallDistance);
				if(jumping)
				{
					//	transform.Translate(0.05f, 0, 0);
					transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
					rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
				}
				transform.Translate(Vector3.zero * Time.deltaTime * WalkSpeed);
			}
		}
	}

	/*
	 * Gets distance between x Axis
	 */
	static float MyDistance(float First, float Second)
	{
		float NewDistance = First - Second;
		return Mathf.Sqrt (NewDistance * NewDistance);
	}


	void OnCollisionEnter(Collision collision)
	{
		// When Landing we set Jumping back on
		if(collision.gameObject.tag == "FloorTouch")
		{
			jumping = false;
			// Turns of velocity so he doesn't slide when landing
			rigidbody.velocity = new Vector3(0,0,0);
		}
		// When Landing we set Jumping back on
		if(collision.gameObject.tag == "WallTouch")
		{

		//	Debug.Log("ME Tuch");

		}
	}
}
