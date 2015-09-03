using UnityEngine;
using System.Collections;

public class AdventureModeUpdate : MonoBehaviour {

	public delegate void UpdateDelegate();
	public UpdateDelegate updateDelegate;	
	
	protected virtual void OnFreeze(){
		updateDelegate = null;
	}	
	protected virtual void OnUnFreeze(){
		updateDelegate = MainUpdate;
	}
	
	protected virtual void MainUpdate(){}
	
	
	protected bool initialized = false;
	public bool IsInitialized{ get{ return initialized; } }
	
	private bool mIsFreeze = false;
	public bool isFreeze{
		get{ return mIsFreeze; }
		set{
			mIsFreeze = value;
			if(!gameObject.activeInHierarchy){ return; }			
			if(mIsFreeze){
				OnFreeze();
			}else{
				OnUnFreeze();
			}
		}
	}
	
	void Update () {
		if(updateDelegate != null){
			updateDelegate();
		}
	}	
	 
}
