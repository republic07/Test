using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public CharacterController myCharacterController;
	public float velocityY = 0f;
	public float jumpSpeed = 10f;
	public float gravity = 10f;
	public Transform moveVector;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown("a"))
		//transform.Translate(Vector3.up * Time.deltaTime);
		GravityAndJump();
	}
	public int maxJump = 2;
	public int jumpCount = 0;
	
	private void GravityAndJump()
	{

			if(Input.GetKeyDown("up") || Input.GetKeyDown("w"))
			{
				if( jumpCount < maxJump){
					velocityY = jumpSpeed;
					jumpCount++;
				}
			}
		
		if(myCharacterController.isGrounded)
		{
			velocityY = 0f;
			jumpCount = 0;
		}
		else
		{
			velocityY += gravity * Time.deltaTime;
		}
		
		//moveVector.y += velocityY*Time.deltaTime;
	}
}
