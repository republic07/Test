using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool OnGround = false;
	public bool OnSky = false;
	public float jforce = 0;
	public float gforce = 0;
	public float powerJump = 6;
	public Vector3 changePosition;
	public int jumpcount = 0;
	private int maxjump = 2; 

	void Start () {
	
	}
	

	void Update () {
		CheckGround();
		jumpControl();
		//Gravitron();
		if(Input.GetKeyDown("a")){
			jump ();
			//CheckSky();


		}
		else{}


	}

	public void slide(){


	}

	public void Gravitron(){
		//Debug.Log(jumpcount);

		if(OnGround == true ){

			gforce = 0;
			jumpcount =0;
			//transform.Translate(Vector3.down * gforce *  Time.deltaTime);
			transform.position = Vector3.Lerp(transform.position,changePosition,Time.deltaTime*8);
		}
		else if(OnGround == false ){
	
				jumpcount +=1;
				//Debug.Log("jump");
			gforce -= powerJump;
			changePosition.y = 0;
			transform.Translate(Vector3.up * gforce *  Time.deltaTime);

				
			}



		/*if(transform.position.y < 0){
			transform.position = new Vector3(transform.position.x,0,transform.position.z);
		}*/
	}
	private void CheckGround(){
		RaycastHit hit;
		Ray Raycasted = new Ray(transform.position, Vector3.down*2);
		Debug.DrawRay (transform.position, new Vector3 (0, -1, 0), Color.blue);
		if (Physics.Raycast (Raycasted,out hit,2)) {
			if(hit.collider.tag=="ground"){
				OnGround = true;
				//Debug.Log("true");
			}
		}else{
			OnGround = false;
			//Debug.Log("false");
		}
	}

	/*private void CheckSky(){
		RaycastHit hit;
		Ray Raycasted = new Ray(transform.position, Vector3.down*2);
		Debug.DrawRay (transform.position, new Vector3 (0, -1, 0), Color.blue);
		if (Physics.Raycast (Raycasted,out hit,5)) {
			if(hit.collider.tag=="ground"){
				OnSky = false;
				Debug.Log("Skyfalse");
			}
		}else{
			OnSky = true;
			Debug.Log("Skytrue");
		}
	}*/

	public void jumpControl(){
		if(jumpcount<maxjump){Gravitron();}
		else{
			//jumpcount-=0;
		}
	}


	private void jump(){
		changePosition.y += powerJump;
		gforce += powerJump;
		jumpcount++;
		if(OnGround == false && jumpcount < maxjump){gforce += powerJump*200;changePosition.y += powerJump*500;}

	}

}
