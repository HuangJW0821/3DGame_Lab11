using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour{
    public int points;

    void OnCollisionEnter(Collision collision){
        // Debug.Log("Target Collision");
        if (collision.gameObject.CompareTag("Arrow")){
            if(ScoreManager.Instance != null) ScoreManager.Instance.AddScore(points);
        }
    }
}
