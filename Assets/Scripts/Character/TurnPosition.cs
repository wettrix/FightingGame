using UnityEngine;
using System.Collections;

public class TurnPosition : MonoBehaviour
{
	internal GameObject[] Players = new GameObject[2];		// Characters On the stage
	
	private Movement cPlayer01Move;		// Characters Movement Class
	private Movement cPlayer02Move;		// Characters Movement Class
	
	private Vector3 LeftRotation;		// If left  side turn like this
	private Vector3 RightRotation;		// If Right side turn like this

	// Use this for initialization
	void Start ()
	{
		// Getting Players
		for(int i = 0; i < Players.Length; i++)
		{
			// Get's Players tag
			if(GameObject.FindWithTag("Player01") )
				Players[0] = GameObject.FindWithTag("Player01");
			if(GameObject.FindWithTag("Player01") )
				Players[1] = GameObject.FindWithTag("Player02");
		}

		// Set Player Move class
		cPlayer01Move = Players[0].GetComponent<Movement>();
		cPlayer02Move = Players[1].GetComponent<Movement>();

		// Set Rotation information
		LeftRotation = new Vector3(0,0,0);
		RightRotation = new Vector3(0,180,0);

	}

	void OnGUI()
	{
		for(int i = 0; i < Players.Length ; i++)
			GUI.Label(new Rect(0,50 * i,400,50), "Char 0" + (i+1) + " Pos: " + Players[i].transform.position);
	}

	// Update is called once per frame
	void Update ()
	{
		// Tracing Character Positoins, if we pass them flip the character
		if(!cPlayer01Move.jumping && !cPlayer02Move.jumping)
		{
			// If goes past flip
			if(Players[0].transform.position.x < Players[1].transform.position.x)
				Players[0].transform.eulerAngles = LeftRotation;
			else
				Players[0].transform.eulerAngles = RightRotation;
		}

		if(!cPlayer02Move.jumping && !cPlayer01Move.jumping)
		{
			// If goes past flip
			if(Players[1].transform.position.x < Players[0].transform.position.x)
				Players[1].transform.eulerAngles = LeftRotation;
			else
				Players[1].transform.eulerAngles = RightRotation;
		}

	}
}
