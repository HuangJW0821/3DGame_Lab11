using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class BearController : MonoBehaviour
{
    public Transform player;   // 玩家目标
    public float chaseRange = 15f;  // 追逐范围
    public float attackRange = 5f;  // 攻击范围
    public float moveSpeed = 3f;  // Bear 的移动速度
    public float rotationSpeed = 5f;  // Bear 的旋转速度
    public int heath = 5;
    private bool isDead = false;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip bearRun;
    public AudioClip bearAttack;
    public AudioClip bearDeath;

    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update(){
        if(!isDead){
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (attackRange <= distanceToPlayer && distanceToPlayer <= chaseRange){
                ChasePlayer();
                agent.isStopped = false;
            }
            else{
                animator.SetBool("Run", false);
                agent.isStopped = true;
            }

            if (distanceToPlayer <= attackRange){
                // 如果距离足够近，进行攻击或捕获玩家
                AttackPlayer();
            }
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Arrow")){
            Debug.Log("Bear is hit!");
            animator.SetTrigger("HitFront");
            heath--;

            if(heath<=0){
                isDead=true;
                Death();
            }

            // if (!audioSource.isPlaying && Random.Range(0, 5) == 0) audioSource.Play();
        }
    }

    void Death(){
        agent.isStopped = true;
        isDead = true;
        animator.SetTrigger("Death");
        if(!audioSource.isPlaying){
            audioSource.clip = bearDeath;
            audioSource.Play();
        }
        if(ScoreManager.Instance != null) ScoreManager.Instance.AddScore(30);
        if (isDead && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){
            // 延迟3秒销毁 
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private void ChasePlayer(){
        if(!audioSource.isPlaying){
            audioSource.clip = bearRun;
            audioSource.Play();
        }

        animator.SetBool("Run", true);

        agent.isStopped = false;
        agent.SetDestination(player.position);

        // 使用力场驱动 Bear
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);
        
        // 让 Bear 面向玩家方向
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AttackPlayer(){
        if(!audioSource.isPlaying && Random.Range(0,4)==0){
            audioSource.clip = bearAttack;
            audioSource.Play();
        }
        animator.SetTrigger("Attack1");
        Debug.Log("Bear is attacking the player!");
    }

    private IEnumerator DestroyAfterDelay(){
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
