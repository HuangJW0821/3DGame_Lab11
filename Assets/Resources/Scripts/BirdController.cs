using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip song1;
	public AudioClip song2;

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        // 检查是否正在播放 sing 动画，并随机播放音效
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("sing")){
            if (!audioSource.isPlaying){
                if (Random.Range(0, 2) == 0) audioSource.clip = song1;
                else audioSource.clip = song2;

                audioSource.Play();
            }
        }
    }
}