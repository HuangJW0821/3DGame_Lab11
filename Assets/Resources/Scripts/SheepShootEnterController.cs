using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepShootEnterController : MonoBehaviour{
    public int additionalArrows = 10;
    public GameObject[] sheepPrefabs;
    public Transform sheepSpawnPoint;
    public float sheepSpawnDistance = 20f;
    public float sheepSpawnRange = 5f;

    public float cooldownTime = 5f;  // 冷却时间（秒）
    private float lastSpawnTime = -Mathf.Infinity;  // 上次生成的时间

    private void OnControllerColliderHit(ControllerColliderHit collision){
        if (collision.gameObject.CompareTag("SheepShootEnter")){
            CrossbowAnimatorControllor crossbow = GameObject.Find("PlayerCrossbow").GetComponentInChildren<CrossbowAnimatorControllor>();
            
            if (crossbow != null && Time.time >= lastSpawnTime + cooldownTime){
                crossbow.UpdateArrowNum(additionalArrows);
                SpawnSheepNearPlayer(); // 生成随机数量的 Sheep (3-5 个)
                lastSpawnTime = Time.time;
            }else{
                Debug.Log("Sheep spawn is on cooldown.");
            }
        }
    }

    // 在玩家前方生成 3-5 个随机的 Sheep
    private void SpawnSheepNearPlayer(){
        int sheepCount = Random.Range(3, 6);

        for (int i = 0; i < sheepCount; i++){
            // 计算随机的偏移量
            Vector3 randomOffset = new Vector3(
                Random.Range(sheepSpawnDistance, sheepSpawnDistance+5f),
                0f,
                Random.Range(-sheepSpawnRange, sheepSpawnRange)
            );

            Vector3 spawnPosition = sheepSpawnPoint.position + -sheepSpawnPoint.right * sheepSpawnDistance + randomOffset;

            GameObject selectedSheepPrefab = sheepPrefabs[Random.Range(0, sheepPrefabs.Length)];

            Instantiate(selectedSheepPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
