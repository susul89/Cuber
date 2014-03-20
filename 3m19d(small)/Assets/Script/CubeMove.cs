using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {
	int CountX = 0;
	int CountY = 0;
	int CountZ = 0;
	Vector3 LookDir;
	public int a=0;//1이 오른쪽 2가 왼쪽 3이 위 4가 아래 
	bool enable=true;//ture 면 사용가능
	bool ing=false;
	int endcheck=0;
	AudioSource RotationAudio=null;
	// Use this for initialization
	void Start () {
		RotationAudio=GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update () {
		LookDir = new Vector3(CountX,CountY,CountZ);
		if(enable==true){
			RotationAudio.Stop();
			RotationAudio.time=0;
			if (Input.GetKey (KeyCode.RightArrow)) {
				a=1;enable=false;RotationAudio.Play();
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				a=2;enable=false;RotationAudio.Play();
			}	
			if (Input.GetKey (KeyCode.UpArrow)) {
				a=3;enable=false;RotationAudio.Play();
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				a=4;enable=false;RotationAudio.Play();
			}
		}
		if(a==1){
			ing=true;
			endcheck+=1;
			CountZ+=1;
			}
		if(a==2){
			ing=true;
			endcheck+=1;
			CountZ-=1;
			}
		if(a==3){
			ing=true;
			endcheck+=1;
			CountX+=1;
			}
		if(a==4){
			ing=true;
			endcheck+=1;
			CountX-=1;
			}
		if(endcheck==90){
			enable=true;
			a=0;
			endcheck=0;
			}
		if(enable==false){
			if(ing==true)
		transform.rotation = Quaternion.Euler(LookDir-Vector3.zero);
		}

	}

}	
