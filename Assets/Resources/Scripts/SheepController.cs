using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour{
    private Animator animator;
    public float moveSpeed = 1f;
    public float runSpeed = 2.5f;
    private bool isDead = false;
    private float deathDelay = 3f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("walk0") || stateInfo.IsName("walk1") || stateInfo.IsName("walk2") || stateInfo.IsName("walk3")){
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (stateInfo.IsName("run0") || stateInfo.IsName("run1")){
            transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        }

        if (isDead && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){
            // 延迟3秒销毁 
            StartCoroutine(DestroyAfterDelay());
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Arrow")){
            animator.SetTrigger("Dead");
            isDead = true;

            if (!audioSource.isPlaying && Random.Range(0, 5) == 0) audioSource.Play();

            if(ScoreManager.Instance != null) ScoreManager.Instance.AddScore(5);
        }

    }

    private IEnumerator DestroyAfterDelay(){
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
