using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MasterData_Player masterData;
    [SerializeField] Camera mainCamera;
    [SerializeField] SpringJoint2D springJoint2D;
    [SerializeField] GameObject santa;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 5;
            var moveVec = (targetPos - transform.position).normalized * masterData.moveSpeed;
            rb.MovePosition(transform.position + moveVec);
        }
    }
}
