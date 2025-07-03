using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen ;
    private GridPosition gridPosition;

    private Animator animator ;

    private Action OnActionCompleted;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gridPosition = LevelGrid.Instance().GetGridPosition(transform.position);
        LevelGrid.Instance().SetDoorAtGridPosition(this,gridPosition);
        if (isOpen)
            Open();
        else
            Close();
    }

    public void Interact(Action OnActionCompleted)
    {
        this.OnActionCompleted = OnActionCompleted;
        if(isOpen)
        {
            Close();
            StartCoroutine(WaitForAnimation());
        }
        else
        {
            Open();
            StartCoroutine(WaitForAnimation());
        }
    }


    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        OnActionCompleted.Invoke();
    }

    public void Open()
    {
        isOpen = true;
        animator.SetBool("isOpen", true);
        PathFinding.Instance().SetIsGridPositionCanWalk(gridPosition, true);
    }

    public void Close()
    {
        isOpen = false;
        animator.SetBool("isOpen", false);
        PathFinding.Instance().SetIsGridPositionCanWalk(gridPosition, false);
    }

}
