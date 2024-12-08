using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAnimatorControllor : MonoBehaviour{
    private Animator animator;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float baseArrowForce = 0f;
    public float maxArrowForce = 10f;
    private float currentArrowForce;
    private bool isCharging = false;

    public UIView uiView;
    public int arrowNum = 5;

    private AudioSource audioSource;
    public AudioClip shootSound1;
    public AudioClip shootSound2;
    public AudioClip loadSound;

    void Awake(){
        uiView = FindObjectOfType<UIView>();
        if(uiView == null) Debug.LogError("UIView not found");
    }

    // Start is called before the first frame update
    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if(uiView != null) uiView.UpdateArrowNum(arrowNum);
    }

    // Update is called once per frame
    void Update(){
        if(arrowNum <= 0) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(Input.GetMouseButtonDown(0)){
            isCharging = true;
            animator.SetTrigger("Fill");
            if(!audioSource.isPlaying){
                audioSource.clip = loadSound;
                audioSource.Play();
            }
        }

        if(Input.GetMouseButtonUp(0) && isCharging){
            isCharging = false;
            animator.SetTrigger("Shoot");
            if(!audioSource.isPlaying){
                if (Random.Range(0, 2) == 0) audioSource.clip = shootSound1;
                else audioSource.clip = shootSound2;

                audioSource.Play();
            }
        }

        if(isCharging){
            float normalizedTime = stateInfo.normalizedTime % 1f;
            currentArrowForce = Mathf.Lerp(baseArrowForce, maxArrowForce, normalizedTime);
        }
    }

    void OnShootAnimationEnd(){SpawnArrow();}

    void SpawnArrow(){
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if(rb!=null) rb.AddForce(arrowSpawnPoint.forward * currentArrowForce, ForceMode.Impulse);
    
        UpdateArrowNum(-1);
    }

    public void UpdateArrowNum(int deltArrowNum){
        Debug.Log("In UpdateArrowNum");
        arrowNum += deltArrowNum;
        if(uiView != null) uiView.UpdateArrowNum(arrowNum);
    }
}
