using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {
	public int DoorSwitch = 0;
	float openspeed = 2.0f;
	Vector3 firstPosition;
	Vector3 movePosition;
	// Use this for initialization
	void Start () {
		firstPosition=transform.position;
		movePosition=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(DoorSwitch==0)
				DoorSwitch=1;
			else if(DoorSwitch==2)
				DoorSwitch=3;
		}
		if(DoorSwitch==1)//열기
		{
			if(movePosition.x-firstPosition.x<6.8&&movePosition.x-firstPosition.x>-6.8)
			{
				if(movePosition.z-firstPosition.z<1&&movePosition.z-firstPosition.z>-1)
				{
					//movePosition.z-=(Time.deltaTime*openspeed);
				}
				movePosition.x+=(Time.deltaTime*openspeed);
				transform.position=movePosition;
			}
			else 
			{
				DoorSwitch=2;//열린상태
			}
		}//transform.posit
		if(DoorSwitch==3)//닫힘
		{
			movePosition=firstPosition;
			transform.position=firstPosition;
			DoorSwitch=0;//닫힌상태
		}//transform.posit
	}
}
