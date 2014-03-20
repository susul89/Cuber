using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour 
{
	private float rotationX;
	private float rotationY;
	private float sensitivity=300;
	void Start () 
	{
		//부모에게 접근하는 방법
	}
	
	void Update () //프레임에 따라 매번 갱신되는 함수
	{

		float mouseMoveValueX= Input.GetAxis("Mouse X");
		//마우스가 어디로 움직이는지방향을 반환해줘
		float mouseMoveValueY= Input.GetAxis("Mouse Y");
		
		rotationY += mouseMoveValueX*sensitivity*Time.deltaTime;
		//Time.deltaTime은 이전프레임과 현재 프레임의 시간차이?
		//interval+= Time.deltaTime;시간을 계속 증가시키는 거지 이러면
		//시간을 사용해서 얼마만큼 회전할 것인가를 결정한다.
		//sensitivity가 700이니까 시간에 곱해져서 그마늠만 움직이도록.
		rotationX += mouseMoveValueY*sensitivity*Time.deltaTime;
		
		rotationX %=360;
		rotationY %=360;
		
		if(rotationX>90.0f)
			rotationX=90.0f;
		if(rotationX<-90.0f)
			rotationX=-90.0f;
		//시야가 상하로 뒤아래 90도 가 넘어가거나 아래로 90도가 넘어가면 고정
		transform.eulerAngles=new Vector3(-rotationX, rotationY,0.0f);
		//이것은 무엇이냐 . transform.eulerAngles는 회전을 시키는 기능을 하는.
	}
}
