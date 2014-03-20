
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	static AudioManager _instance=null;
	
	public static AudioManager Instance()
	{
		return _instance;
	}
	public AudioClip music = null;//배경음악
	//외부 음향파일을 관리하는 객체
	
	
	void Start () 
	{
		if(_instance==null)
			_instance=this;
		
		if(music!=null)//music 이 있으면.
		{
			audio.clip=music;//audio는 transform과 같은 멤버함수
			audio.loop=true;//반복
			audio.Play();
		}
	}
	//오디오 소스와 오디오 리스너
	//소스는 소리를 내는 용도고
	//리스너는 어떤 객체에서 소리가나는데  캐릭터와 거이에의해서 소리크리를 조절하는 용도?
	
	public void PlaySfx(AudioClip clip)//효과음을 내주는 함수를 만든다.
	{
		audio.PlayOneShot(clip);//한번 출력하는 것을 해주는 함수.
	}
	void Update () 
	{
		
	}
}