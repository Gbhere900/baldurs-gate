using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("walk")]
    private Vector3 targetPosition;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float speed  = 1;

    [Header("animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed = 10f;

    [Header("Ñ¡ÖÐÍ¼±ê")]
    [SerializeField] private GameObject selectedVisual;


    private void Awake()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,targetPosition) > stopDistance)
        {
           Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * speed;
            animator.SetBool("isWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime *rotateSpeed );
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public void DisAbleSelectedVisual()
    {
        selectedVisual.SetActive(false);
    }
    public void EnableSelectedVisual()
    {
        selectedVisual.SetActive(true);
    }
}
