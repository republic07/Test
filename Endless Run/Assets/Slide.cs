using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour {
	public GameObject someGameObject;

	// Use this for initialization
	void Start () {
			}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("s")){
			slide ();
		}
		else
		{Unslide();}

	}

	public void slide(){

		BoxCollider b = someGameObject.GetComponent<Collider>() as BoxCollider;
		b.size = new Vector3(1f,0.5f,1f);
		b.center = new Vector3(0f,-0.5f,0f);
	}
	public void Unslide(){
		BoxCollider b = someGameObject.GetComponent<Collider>() as BoxCollider;
		b.size = new Vector3(1f,1f,1f);
		b.center = new Vector3(0f,0f,0f);
	}
}
