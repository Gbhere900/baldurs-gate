using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSysterm : MonoBehaviour
{
    private int turnCount =1;
    public Action OnTurnCountChanged;

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
       OnTurnCountChanged.Invoke();
    }
}
