using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystermUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI turnText;

    private void OnEnable()
    {
        TurnSysterm.Instance().OnTurnCountChanged += TurnSysterm_OnTurnCountChanged;
        UpdateTurnText();
    }
   
    private void UpdateTurnText()
    {
        turnText.text = "Turn "+ TurnSysterm.Instance().GetTurnCount();
    }

    private void TurnSysterm_OnTurnCountChanged()
    {
        UpdateTurnText();
    }



}
