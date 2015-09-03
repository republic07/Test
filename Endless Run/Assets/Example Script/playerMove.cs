using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
	public string jumpKey = "";
	public string axisKey = "";
	public string posKey = "";
	public string nevKey = "";
	public bool control = true;
	public float moveSpeed;
	public float jumpSpeed;
	public float velocityY;
	public float rad;
	public float gravity;
	public bool ground;
	public bool tim;
	public bool stun;
	//public playerFist fist;
	//public gameController gC;
	public float forcePower;
	public float side;
	public AudioClip jumpSound;
	public float mobileMove;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*if(control&&gC.start){
			// if(this.gameObject.name=="player3"){
			// 	moveJA ();
			// 	moveJD ();
			// }else if(this.gameObject.name=="player1"){
			// 	move0 ();
			// }else{
			// 	move ();
			// }
			move(mobileMove);
			gravi ();
			//jump (); 
			checkground ();
			checkside ();
			forcePowerDown();
		}*/
	}
	
	public void move(float h){
		if(h>0){
			Vector3 theScale = transform.localScale;
			theScale.x = 0.2f;
			transform.localScale = theScale;
			//fist.side = -1f;
			side = 1f;
		}else if(h<0){
			Vector3 theScale = transform.localScale;
			theScale.x = -0.2f;
			transform.localScale = theScale;
			//fist.side = 1f;
			side = -1f;
		}
		transform.Translate(Vector3.up * velocityY * Time.deltaTime);
		transform.Translate(-Vector3.right * h * Time.deltaTime * moveSpeed);
		transform.Translate (-Vector3.right * forcePower * Time.deltaTime);
		transform.Translate(Vector3.up * velocityY * Time.deltaTime);
	}


	// void move0(){
	// 	float h = 0;
	// 	if(Input.GetKey(posKey)){
	// 		h = -1;
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = -0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = 1f;
	// 		side = 1f;
	// 	}else if(Input.GetKey(nevKey)){
	// 		h = 1;
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = 0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = -1f;
	// 		side = -1f;
	// 	}
	// 	transform.Translate(Vector3.up * velocityY * Time.deltaTime);
	// 	transform.Translate(-Vector3.right * h * Time.deltaTime * moveSpeed);
	// 	transform.Translate (-Vector3.right * forcePower * Time.deltaTime);
	// 	transform.Translate(Vector3.up * velocityY * Time.deltaTime);
	// }
	// void moveJA(){
	// 	float h = Input.GetAxis("360_AHorizontal");
	// 	if(h<0.3&&h>-0.3){
	// 		h=0;
	// 	}
	// 	if(h>0){
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = 0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = -1f;
	// 		side = 1f;
	// 	}else if(h<0){
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = -0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = 1f;
	// 		side = -1f;
	// 	}
	// 	transform.Translate(-Vector3.right * h * Time.deltaTime * moveSpeed);
	// 	transform.Translate (-Vector3.right * forcePower * Time.deltaTime);
	// 	transform.Translate(Vector3.up * velocityY * Time.deltaTime);
	// }
	// void moveJD(){
	// 	float h = Input.GetAxis("360_DHorizontal");
	// 	if(h>0){
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = 0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = -1f;
	// 		side = 1f;
	// 	}else if(h<0){
	// 		Vector3 theScale = transform.localScale;
	// 		theScale.x = -0.2f;
	// 		transform.localScale = theScale;
	// 		fist.side = 1f;
	// 		side = -1f;
	// 	}
	// 	transform.Translate(-Vector3.right * h * Time.deltaTime * moveSpeed);
	// 	transform.Translate (-Vector3.right * forcePower * Time.deltaTime);
	// 	transform.Translate(Vector3.up * velocityY * Time.deltaTime);
	// }

	public void jumpMobile(){
		if(ground){
			velocityY += jumpSpeed;
			GetComponent<AudioSource>().clip = jumpSound;
			if(!GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().PlayOneShot(jumpSound,0.7f);
			}
		}
	}

	// void jump(){
	// 	if(ground&&Input.GetButton(jumpKey)){
	// 		velocityY += jumpSpeed;
	// 		audio.clip = jumpSound;
	// 		if(!audio.isPlaying){
	// 			audio.PlayOneShot(jumpSound,0.7f);
	// 		}
	// 	}
	// }


	void gravi(){
		if (ground) {
			velocityY = 0;
		} else {
			velocityY += gravity * Time.deltaTime;
		}
		if(transform.position.y < -3.4f){
			transform.position = new Vector3(transform.position.x,-3.4f,transform.position.z);
		}
	}
	void checkground(){
		RaycastHit hit;
		Debug.DrawRay (transform.position, new Vector3 (0, -1, 0), Color.white);
		if (Physics.Raycast (transform.position, -Vector3.up,out hit,rad)) {
			if(hit.collider.tag=="ground"){
				ground = true;
			}
		}else{
			ground = false;
		}
		if(ground&&tim){
			GetComponent<Rigidbody>().Sleep();
		}
	}
	void checkside(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.right*side,out hit,rad)) {
			if(hit.collider.tag=="wall"){
				tim = false;
			}else{
				tim = true;
			}
		}
		if (Physics.Raycast (transform.position, -Vector3.right*side,out hit,rad)) {
			if(hit.collider.tag=="wall"){
				tim = false;
			}else{
				tim = true;
			}
		}
	}
	void force(float inX){
		//		if(inX < transform.position.x){
		//			rigidbody.AddForce(Vector3.right*500f);
		//		}else if(inX > transform.position.x){
		//			rigidbody.AddForce(-Vector3.right*500f);
		//		}
		//		tim = false;
		//		stun = true;
		Invoke ("setTim",0.2f);
		stun = true;
		if(inX < transform.position.x){
			forcePower = -20.0f;
		}else if(inX > transform.position.x){
			forcePower = +20.0f;
		}
	}
	void setTim(){
		tim = true;
		stun = false;
	}
	void forcePowerDown(){
		if(forcePower > 0){
			forcePower -= 1.0f;
		}else if(forcePower < 0){
			forcePower += 1.0f;
		}else{
			stun = false;
		}
	}
	void setControl(){
		control = false;
	}
}
