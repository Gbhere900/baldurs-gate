using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
     private List<Unit> unitList = new List<Unit>();
     private List<Unit> friendlyUnitList = new List<Unit>();
     private List<Unit> enemyUnitList = new List<Unit>();

    private static UnitManager instance;

    public static UnitManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
       instance = this;

       unitList = new List<Unit>();
       friendlyUnitList = new List<Unit>();
       enemyUnitList = new List<Unit>();
    }

    private void OnEnable()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAntUnitDead;
    }
    private void OnDisable()
    {
        Unit.OnAnyUnitSpawned -= Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead -= Unit_OnAntUnitDead;
    }
    private void Unit_OnAntUnitDead(Unit unit)
    {
        if(unit.GetIsEnemy())
            enemyUnitList.Remove(unit);
        else
            friendlyUnitList.Remove(unit);
        unitList.Remove(unit);
    }

    private void Unit_OnAnyUnitSpawned(Unit unit)
    {
        if (unit.GetIsEnemy())
            enemyUnitList.Add(unit);
        else
            friendlyUnitList.Add(unit);
        unitList.Add(unit);
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

}
