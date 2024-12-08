using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowFollow : MonoBehaviour{
    private Vector3 initialOffset;
    public GameObject mainCamera;
    public Vector3 rotationOffset = new Vector3(90, 0, 0);
    public float positionSmoothSpeed = 10f;
    public float rotationSmoothSpeed = 5f;
    public float followDistance = 1f;
    public float heightOffset = -0.5f;
    public bool isColliding = false;
    private Vector3 originalPosition;
    private Rigidbody rb;

    void Start(){
        mainCamera = GameObject.Find("MainCamera");
        initialOffset = transform.position - mainCamera.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        // if(isColliding) return;

        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward* followDistance;
        targetPosition.y += heightOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, positionSmoothSpeed * Time.deltaTime);
        
        Quaternion targetRotation = mainCamera.transform.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        isColliding = true;
        originalPosition = transform.position;
    }

    private void OnCollisionExit(Collision collision){
        isColliding = false;
        StartCoroutine(SmoothPositionRecovery());
    }

    // private void OnCollisionStay(Collision collision){
    //     isColliding = false;
    //     StartCoroutine(SmoothPositionRecovery());
    // }

    private IEnumerator SmoothPositionRecovery()
    {
        rb.isKinematic = true;

        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * followDistance;
        targetPosition.y += heightOffset;
        Quaternion targetRotation = mainCamera.transform.rotation * Quaternion.Euler(rotationOffset);

        float elapsedTime = 0f;
        float recoveryTime = 0.5f; // 恢复时间，根据需要调整

        while (elapsedTime < recoveryTime)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / recoveryTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保恢复到目标位置
        transform.position = targetPosition;
        transform.rotation = targetRotation;


        rb.isKinematic = false;
    }
}


