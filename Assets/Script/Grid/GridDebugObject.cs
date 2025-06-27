using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private GridObject gridObject;
    public virtual void SetGridObject(object gridObject)
    {
        this.gridObject = (GridObject)gridObject;
        
    }
    private void Update()
    {
        UpdateText();
    }
    public override string ToString()
    {
        return gridObject.ToString();
    }

    protected virtual void UpdateText()
    {
        textMeshPro.text = gridObject.ToString();
    }
}
