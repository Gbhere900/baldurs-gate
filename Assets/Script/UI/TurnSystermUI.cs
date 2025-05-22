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
    [SerializeField] private GameObject enemytTurnVisual;

    private void OnEnable()
    {
        TurnSysterm.Instance().OnTurnCountChanged += TurnSysterm_OnTurnCountChanged;
        UpdateTurnText();
        UpdateEnemyTurnVisual();
    }
   
    private void UpdateTurnText()
    {
        turnText.text = "Turn "+ TurnSysterm.Instance().GetTurnCount();
    }

    private void TurnSysterm_OnTurnCountChanged()
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
    }

    private void UpdateEnemyTurnVisual()
    {
        if(TurnSysterm.Instance().GetIsEnemyTurn())
        {
            enemytTurnVisual.SetActive(true);
        }
        else
        {
            enemytTurnVisual.SetActive(false);
        }
    }



}
