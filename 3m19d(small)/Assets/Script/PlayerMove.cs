using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]
 public class PlayerMove : MonoBehaviour {
	//public Transform cameraTransform=null;
	public int InState=1;
	public float walkSpeed =3.0f;//이동속도 
	public float runSpeed =5.0f;//이동속도 
	public float jumpSpeed =10.0f;//점프속도
	public float gravity = -20.0f;//점프에 중력값
	//public bool isMove=false;//현재 움직이는지 체크
	public bool isLand=false;//현재 바닥위에 서있는지.
	float yVelocity=0.0f;//점프에 처리할?
	public float rotationSpeed=10.0f;//돌아가는 속도

	protected Animator avatar;
	private int MoveChek=0; //이전움직임 상태 체크 0이면 움직임없음 1이면 걷기 2면 달리기
	private Vector3 lentlyDir;//이전 움직임의 방향 

	CharacterController characterController=null;
	CubeMove cubemove=null;

	enum PLAYERSTATE
	{
		NONE=-1,
		IDLE,//0
		WALK,//1
		RUN,//2
		OTHERWALK,//3
		JUMP,//4
		JUMPEND,//5
		FALL,//6
		PUSHBOTTON,//7
		HOVER,//8
		DEAD,//9
	}
	/* 애니메이션 용 번호.
	enum PLAYERSTATE
	{
		NONE=-1,
		IDLE,//0
		WALK,//1
		RUN,//2
		STEPL,//3
		STEPR,//4
		BACKWALK,//5
		JUMP,//6
		JUMPING,//7
		JUMPEND,//8
		FALL,//9
		PUSHBOTTON,//10
		HOVER,//11
		DEAD,//12
	}*/
	PLAYERSTATE playerState=PLAYERSTATE.IDLE;

	void CheckGround()
	{
		if(yVelocity<0)
		{
			if((characterController.collisionFlags!=CollisionFlags.Below)){
				avatar.SetInteger("PlayerState",9);
				playerState=PLAYERSTATE.FALL;
				isLand=false;
				}
		}
		if((characterController.collisionFlags==CollisionFlags.Below))
		{	//캐릭터에 바닥에 충돌이 잇을 경우 
			yVelocity=0.0f;
			if(isLand==false)
			{
				playerState=PLAYERSTATE.JUMPEND;
				avatar.SetInteger("PlayerState",8);
			}
			isLand=true;
		}
	}
	void PushWalk()
	{
		if(Input.GetKey(KeyCode.W))
		{
			avatar.SetInteger("PlayerState",1);
			playerState=PLAYERSTATE.WALK;
		}
		if(Input.GetKey(KeyCode.A))
		{			
			playerState=PLAYERSTATE.OTHERWALK;
			avatar.SetInteger("PlayerState",3);
			if(Input.GetKey(KeyCode.W))
			{
				avatar.SetInteger("PlayerState",1);
				playerState=PLAYERSTATE.WALK;
			}
			if(Input.GetKey(KeyCode.S))
			{
				avatar.SetInteger("PlayerState",5);
			}
		}

		if(Input.GetKey(KeyCode.S))
		{	
			playerState=PLAYERSTATE.OTHERWALK;
			avatar.SetInteger("PlayerState",5);
		}

		if(Input.GetKey(KeyCode.D))
		{			
			playerState=PLAYERSTATE.OTHERWALK;
			avatar.SetInteger("PlayerState",4);
			if(Input.GetKey(KeyCode.W))
			{
				playerState=PLAYERSTATE.WALK;
				avatar.SetInteger("PlayerState",1);
			}
			if(Input.GetKey(KeyCode.S))
			{
				avatar.SetInteger("PlayerState",5);
			}
		}
	}

	void PushJump()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			avatar.SetInteger("PlayerState",6);
			playerState=PLAYERSTATE.JUMP;
		}
	}
	
	void PushRun()
	{
		if(Input.GetKey(KeyCode.LeftShift)){
			avatar.SetInteger("PlayerState",2);
			playerState=PLAYERSTATE.RUN;
		}
	}
	void PushButton()
	{
		if(Input.GetKeyDown(KeyCode.E)){
			avatar.SetInteger("PlayerState",10);
			playerState=PLAYERSTATE.PUSHBOTTON;
		}
	}
	void EndWalk()
	{
		if(Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.A)||
					   Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.D))
		{
			avatar.SetInteger("PlayerState",0);
			playerState=PLAYERSTATE.IDLE;
		}
	}

	void EndRunToWalk()
	{
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			avatar.SetInteger("PlayerState",1);
			playerState=PLAYERSTATE.WALK;
		}
	}
	void Start () 
	{
		GameObject obj=GameObject.Find("CenterCube"+InState);
		cubemove=obj.GetComponent<CubeMove>();
		characterController=GetComponent<CharacterController>();
		lentlyDir=new Vector3(0,0,0);
		avatar=GetComponent<Animator>();
		//캐릭터 컨트롤러를 받아와서 스크립트에서 다루기위해서 처음에 한번 받아옴.
	}
	
	void Update () 
	{
		float x=Input.GetAxis("Horizontal");//좌우 Horizontal 같은 이름은
		float z=Input.GetAxis("Vertical");//앞뒤
		Vector3 moveDirection = new Vector3(x,0,z);
		//moveDirection =cameraTransform.TransformDirection(moveDirection);
		//transform.rotation=Quaternion.LookRotation(moveDirection);
		if(cubemove.a!=0)
			playerState=PLAYERSTATE.HOVER;
		if(cubemove.a==0)
		{
			switch(playerState)
			{
				case PLAYERSTATE.IDLE:
				{
					CheckGround();
					PushWalk();
					PushJump();
					PushButton();
					MoveChek=0;
					characterController.Move(moveDirection*Time.deltaTime);
					lentlyDir=moveDirection;
				}break;
				case PLAYERSTATE.WALK:
				{
					CheckGround();
					PushWalk();
					PushRun();
					PushJump();
					EndWalk();
					MoveChek=1;
					moveDirection *=walkSpeed;
					characterController.Move(moveDirection*Time.deltaTime);
					
					lentlyDir=moveDirection;
				}break;
				case PLAYERSTATE.OTHERWALK:
				{
					CheckGround();
					moveDirection *=walkSpeed;
					characterController.Move(moveDirection*Time.deltaTime);
					PushWalk();
					PushJump();
					EndWalk();
					MoveChek=1;
					lentlyDir=moveDirection;
				}break;

				case PLAYERSTATE.RUN:
				{
					CheckGround();
					moveDirection *=runSpeed;
					characterController.Move(moveDirection*Time.deltaTime);
					EndWalk();
					EndRunToWalk();					
					PushJump();
					MoveChek=2;
					lentlyDir=moveDirection;
				}break;
				case PLAYERSTATE.JUMP:
				{
					if(yVelocity==0)// 
						yVelocity=jumpSpeed;
					yVelocity+=(gravity*Time.deltaTime);
					moveDirection.y=yVelocity;
					if(MoveChek==1){
					moveDirection.x *=walkSpeed;
					moveDirection.z *=walkSpeed;
					}
					else if(MoveChek==2){
					moveDirection.x *=runSpeed;
					moveDirection.z *=runSpeed;}
					characterController.Move(moveDirection*Time.deltaTime);
					CheckGround();
				}break;
				case PLAYERSTATE.FALL:
				{				
					CheckGround();
					yVelocity+=(gravity*Time.deltaTime);
					moveDirection.y=yVelocity;
					characterController.Move(moveDirection*Time.deltaTime);
				}break;
				case PLAYERSTATE.JUMPEND:
				{				
					playerState=PLAYERSTATE.IDLE;
				}break;
				case PLAYERSTATE.PUSHBOTTON:
				{
					playerState=PLAYERSTATE.IDLE;
					avatar.SetInteger("PlayerState",0);
				}break;
				case PLAYERSTATE.DEAD:
				{
				}break;
				case PLAYERSTATE.HOVER:
				{
					if(yVelocity==0)// 
						yVelocity=5;
					yVelocity+=(-5*Time.deltaTime);
					moveDirection.y=yVelocity;
					characterController.Move(moveDirection*Time.deltaTime);
					CheckGround();
				}break;
			}
		}
	}
}

