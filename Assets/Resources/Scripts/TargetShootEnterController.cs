using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShootEnterController : MonoBehaviour{
    public int additionalArrows = 5;

    public float cooldownTime = 5f;  // 冷却时间（秒）
    private float lastSpawnTime = -Mathf.Infinity;  // 上次生成的时间

    private void OnControllerColliderHit(ControllerColliderHit collision){
        if (collision.gameObject.CompareTag("TargetShootEnter")){
            CrossbowAnimatorControllor crossbow = GameObject.Find("PlayerCrossbow").GetComponentInChildren<CrossbowAnimatorControllor>();
            
            if (crossbow != null && Time.time >= lastSpawnTime + cooldownTime){
                crossbow.UpdateArrowNum(additionalArrows);
                lastSpawnTime = Time.time;
            }else{
                Debug.Log("Target Shoot Enter is on cooldown.");
            }
        }
    }
}
