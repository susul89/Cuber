using UnityEngine;
using System.Collections;

public class PlayerRotationMove : MonoBehaviour {
	PlayerMove PM=null;
	Transform InStageCenter=null;
	// Use this for initialization
	void Start () {
		PM=GetComponent<PlayerMove>();
		GameObject obj=GameObject.Find("CenterCube"+PM.InState);	
		InStageCenter=obj.transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

}
