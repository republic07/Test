using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bon_AdvancePoolManager : AdventureModeUpdate {

	public static Bon_AdvancePoolManager controller;

	public GameObject 	[]dnaPrefabArray;
	public int 			[]preloadAmountArray;
	public int			[]maxAmountArray;
	
	private Transform []groupTrans;
	private List<GameObject> []waitingObjLists;
	private List<GameObject> []workingObjLists;
	
	private GameObject fetchObject = null;
	
	public void RecycleAll(){
		fetchObject = null;
		GameObject tmp = null;
		for(int i=0;i<dnaPrefabArray.Length;++i){
			while(workingObjLists[i].Count > 0){
				tmp = workingObjLists[i][0];
				workingObjLists[i].Remove(tmp);				
				tmp.SetActive(false);
				waitingObjLists[i].Add(tmp);
			}
		}
	}

    public float GetPreloadPercent()
    {
        if (numberOfAllPreloadObjects == 0) { return 0f; }

        return (numberOfPreloadedObjects*100f) / numberOfAllPreloadObjects;
    }
    private int numberOfAllPreloadObjects = 0;
    private int numberOfPreloadedObjects = 0;
	
	private IEnumerator CoPreload(){
		groupTrans = new Transform[dnaPrefabArray.Length];
		waitingObjLists = new List<GameObject>[dnaPrefabArray.Length];
		workingObjLists = new List<GameObject>[dnaPrefabArray.Length];
		
		GameObject sample = null;

        numberOfAllPreloadObjects = 0;
        numberOfPreloadedObjects = 0;
        for (int n = 0; n < preloadAmountArray.Length; ++n)
        {
            numberOfAllPreloadObjects += preloadAmountArray[n];
        }
		
		for(int n=0;n<dnaPrefabArray.Length;++n){
			waitingObjLists[n] = new List<GameObject>();
			workingObjLists[n] = new List<GameObject>();
			GameObject parentObj = new GameObject(
				string.Format("{0} group",
				dnaPrefabArray[n].name));
			groupTrans[n] = parentObj.transform;
			groupTrans[n].parent = transform;
			sample = dnaPrefabArray[n];
			for(int i=0;i<preloadAmountArray[n] && i<maxAmountArray[n];++i){
				GameObject tmp = Instantiate(sample) as GameObject;
				tmp.transform.parent = groupTrans[n];
				tmp.transform.localPosition = sample.transform.localPosition;
				tmp.transform.localRotation = sample.transform.localRotation;
				tmp.transform.localScale = sample.transform.localScale;
				tmp.SetActive(false);
				tmp.name = string.Format("{0}_{1}_{2}",n.ToString("D4"),i.ToString("D4"),sample.name);
				waitingObjLists[n].Add(tmp);
                numberOfPreloadedObjects++;				
			}	
			yield return null;	
		}
		
		initialized = true;
	}
	
	public GameObject Create(int index){
		if(index >= dnaPrefabArray.Length){ return null; }
		if(waitingObjLists[index].Count + workingObjLists[index].Count
		 >= maxAmountArray[index]){ return null; }
		GameObject sample = dnaPrefabArray[index];
		GameObject tmp = Instantiate(sample) as GameObject;
		tmp.transform.parent = groupTrans[index];
		tmp.transform.localPosition = sample.transform.localPosition;
		tmp.transform.localRotation = sample.transform.localRotation;
		tmp.transform.localScale = sample.transform.localScale;
		tmp.SetActive(false);
		tmp.name = string.Format("{0}_{1}_{2}",index.ToString("D4"),waitingObjLists[index].Count.ToString("D4"),sample.name);
		waitingObjLists[index].Add(tmp);
		return tmp;
	}
	
	public GameObject FetchObjectFromPool(int index){		
		if(waitingObjLists[index].Count == 0){
			if(Create(index) == null){
               //BonDebug.Log("Exceed limit Pool index "+index);
                return null; }		
		}
		fetchObject = waitingObjLists[index][0];
		fetchObject.SetActive(true);
		workingObjLists[index].Add(fetchObject);
		waitingObjLists[index].RemoveAt(0);
		return fetchObject;
	}
	public void RecycleObjectToPool(GameObject obj){
		int index = int.Parse( obj.name.Substring(0,4) );
		obj.SetActive(false);
		workingObjLists[index].Remove(obj);
		waitingObjLists[index].Add(obj);
	}
	
	void Awake(){
		controller = this;
	}
	
	void Start(){
		StartCoroutine( CoPreload() );
	}
}
