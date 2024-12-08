using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isInCollision = false;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isInCollision){
            isInCollision = true;

            if (!audioSource.isPlaying) audioSource.Play();

            // 让箭矢在碰撞后停止物理模拟
            rb.isKinematic = true;

            // 获取碰撞点的位置和法线
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point;

            // 在碰撞点稍微插入箭矢
            Vector3 insertionOffset = contact.normal * 0.4f;  // 插入深度
            transform.position += insertionOffset;

            // 销毁箭矢
            Destroy(gameObject, 10f);
        }
    }
}

