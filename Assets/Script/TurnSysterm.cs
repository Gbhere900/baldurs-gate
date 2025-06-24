using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSysterm : MonoBehaviour
{
    private int turnCount =1;
    public Action OnTurnCountChanged;
    [SerializeField] private bool IsEnemyTurn = false;

    private static TurnSysterm instance;

    private void Awake()
    {
        instance = this;
    }
    public static TurnSysterm Instance()
    {
        return instance;
    }

    public int GetTurnCount()
    {
        return turnCount;
    }

    public void NextTurn()
    {
       turnCount++;
        IsEnemyTurn = !IsEnemyTurn ;
       OnTurnCountChanged.Invoke();
    }
    public bool GetIsEnemyTurn()
    {
        return IsEnemyTurn;
    }

    public void NextTurnButton_OnClick()
    {
        if (IsEnemyTurn)
            return;
        else
            NextTurn();
    }
}
