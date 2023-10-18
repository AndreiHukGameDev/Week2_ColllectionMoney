using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;
    [SerializeField] private float speed;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.isTrigger = true;
    }

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 1 * speed * Time.deltaTime);
    }
}
