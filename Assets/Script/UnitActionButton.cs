using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public void ChangeText(string tsxt)
    {
        textMeshProUGUI.text = tsxt.ToUpper();
    }
}
