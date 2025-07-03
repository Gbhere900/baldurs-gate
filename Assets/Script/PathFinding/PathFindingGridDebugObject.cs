using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathFindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro gridPositionText;
    [SerializeField] private TextMeshPro fText;
    [SerializeField] private TextMeshPro hText;
    [SerializeField] private TextMeshPro gText;
    [SerializeField] private GameObject isWalkableVisual;
    [SerializeField] private bool enablewalkableVisual;


    private PathFindingGridObject pathFindingGridObject;
    protected override void UpdateText()
    {
        gridPositionText.text = pathFindingGridObject.ToString();
        fText.text = pathFindingGridObject.GetF().ToString();
        hText.text = pathFindingGridObject.GetH().ToString();
        gText.text = pathFindingGridObject.GetG().ToString();

    }

    private void Update()
    {
        UpdateText();
        if (enablewalkableVisual)
        {
            if (pathFindingGridObject.GetIsWalkable())
            {
                isWalkableVisual.SetActive(true);

            }
            else
                isWalkableVisual.SetActive(false);
        }
        else
            isWalkableVisual.SetActive(false);

        
    }
    public override void SetGridObject(object pathFindingGridObject)
    {
        this.pathFindingGridObject = (PathFindingGridObject)pathFindingGridObject;

    }



}
