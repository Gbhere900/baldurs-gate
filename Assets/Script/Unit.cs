using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("walk")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private float speed  = 1;

    [Header("animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed = 10f;

    [Header("Ñ¡ÖÐÍ¼±ê")]
    [SerializeField] private GameObject selectedVisual;

    private GridPosition lastGridpostion;
    private GridPosition currentGridpostion;


    private void Awake()
    {
        targetPosition = transform.position;
        LevelGrid.Instance().SetUnitAtGridPosition(this,LevelGrid.Instance().GetGridPosition(transform.position));
    }

    private void OnEnable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged += UpdateSelectedVisual;
        UpdateSelectedVisual(this,EventArgs.Empty);
    }

    private void OnDisable()
    {
        UnitActionManager.Instance().OnSelectedUnitChanged -= UpdateSelectedVisual;
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

        currentGridpostion = LevelGrid.Instance().GetGridPosition(transform.position);
        Debug.Log(currentGridpostion.ToString());
        if(currentGridpostion != lastGridpostion)
        {

            LevelGrid.Instance().SwitchUnitFromGridPositionToGridPosition(this,lastGridpostion,currentGridpostion);
            lastGridpostion = currentGridpostion;
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

    private void UpdateSelectedVisual(object sender,EventArgs empty)
    {
        Debug.Log("changed");
        if(UnitActionManager.Instance().GetUnit() == this)
            EnableSelectedVisual();
        else
        {
            DisAbleSelectedVisual();
        }
    }
}
