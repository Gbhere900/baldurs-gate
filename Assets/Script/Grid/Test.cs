using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GridSysterm gridSysterm = new GridSysterm(10,10,1);
    private void Update()
    {
        //Debug.Log(MousePositionManager.GetMousePosition());
        Debug.Log(gridSysterm.GetGridPosition(MousePositionManager.GetMousePosition()).ToString());
    }
}
