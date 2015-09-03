using UnityEngine;
using System.Collections;

public class Player01 : MonoBehaviour {


	public int maxJump = 2;
	public int jumpCount = 0;
	public float gforce = 5;
	public bool runGravity;
	public bool OnGround;
	public bool OnHold;
	public Vector3 startPosition;
	public Vector3 HoldPositioning;

	// Use this for initialization
	void Start () {
		transform.position = startPosition;
		HoldPositioning = new Vector3(transform.position.x,-5,transform.position.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		simpleGravity();
		CheckHold();
		if(Input.GetKeyDown("w")){

			jump();
		}
	}


	public void simpleGravity(){
		int midair = 1;
		if(jumpCount >= maxJump){
			transform.position = Vector3.Lerp (transform.position,startPosition,Time.deltaTime);
			Debug.Log("Gravity");
			if(OnGround == true){
				jumpCount = 0;}

		}
		else if(jumpCount == midair){
			transform.position = Vector3.Lerp (transform.position,startPosition,Time.deltaTime);
			
			Debug.Log("midair");
		}


		

		if (OnHold == true)
			{transform.position = Vector3.Lerp (transform.position,HoldPositioning,Time.deltaTime);}
	}
	public void jump(){

			
		if(jumpCount == 0){
			transform.Translate(Vector3.up * gforce *  Time.deltaTime);
			jumpCount++;
			Debug.Log("Jump1");
		}
		else if(jumpCount<maxJump){

			transform.Translate(Vector3.up * gforce * Time.deltaTime);
			Debug.Log("Jump2");
			jumpCount++;
		}

	}

	private void CheckHold(){
		RaycastHit hit;
		Ray Raycasted = new Ray(transform.position, Vector3.down*2);
		Debug.DrawRay (transform.position, new Vector3 (0, -1, 0), Color.blue);
		if (Physics.Raycast (Raycasted,out hit,1.1f)) {
			if(hit.collider.tag=="ground"){
				OnGround = true;

				//Debug.Log("true");
			}
			else if (hit.collider.tag=="hold"){
				OnHold = true;
			}
		}else{
			OnGround = false;
			//Debug.Log("false");
		}
	}
}
