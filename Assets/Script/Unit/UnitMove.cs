using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : BaseUnitAction
{
    [Header("walk")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float speed = 1;

    [Header("animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed = 10f;


    protected override void Awake()
    {
        base.Awake();
        maxActionDistance = 3;
        targetPosition = transform.position;

    }
    private void Update()
    {
        if (!isActive)
            return;

        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * speed;
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
        else
        {
            StopMove();
        }
    }

    public override void TakeAcion(Vector3 targetPosition, Action OnActionCompeleted)
    {
        this.OnActionCompeleted = OnActionCompeleted;
        this.targetPosition = targetPosition;
        isActive = true;
    }

    private  void StopMove()
    {
        animator.SetBool("isWalking", false);
        OnActionCompeleted();
        isActive = false;
    }



    public override string GetUnitAcionName()
    {
        return "Move";
    }

}
