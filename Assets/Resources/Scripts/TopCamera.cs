using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCamera : MonoBehaviour{
    GameObject mainCamera;
    Vector3 topPosition;

    // Start is called before the first frame update
    void Start(){
        mainCamera = GameObject.Find("MainCamera");
        transform.position = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update(){
        topPosition = mainCamera.transform.position;
        topPosition.y += 30;
        transform.position = topPosition;
        
    }
}
