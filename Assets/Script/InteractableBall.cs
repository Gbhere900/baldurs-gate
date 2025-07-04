using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBall : MonoBehaviour,Interactable
{
    private GridPosition gridPosition;
    private bool isGreen;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    private Action OnActionCompleted;

    private void Awake()
    {
        gridPosition = LevelGrid.Instance().GetGridPosition(transform.position);
        LevelGrid.Instance().SetInteractableAtGridPosition(this, gridPosition);

        if (isGreen)
        {
            SetGreen();
        }
        else
        {
            SetRed();
        }
    }
    public void Interact(Action OnActionCompeted)
    {
        if(isGreen)
            SetRed();
        else
            SetGreen();
        this.OnActionCompleted = OnActionCompeted;
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        OnActionCompleted.Invoke();
    }
    public void SetRed()
    {
        isGreen = false;
        meshRenderer.material = redMaterial;
    }

    public void SetGreen()
    {
        isGreen = true;
        meshRenderer.material = greenMaterial;
    }
}
