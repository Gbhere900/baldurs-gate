using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private UnitMove unitMove;
    private UnitShoot unitShoot;
    private UnitSlash unitSlash;
    [SerializeField] private GameObject BulletEffect;
    [SerializeField] private Transform BulletSpawnPoint;
                        

    private void OnEnable()
    {
        if(TryGetComponent<UnitMove>(out unitMove))
        {
            unitMove.OnStartWalkAnimation += UnitMove_OnStartWalkAnimation;
            unitMove.OnStopWalkAnimation += UnitMove_OnStopWalkAnimation;
        }
        if (TryGetComponent<UnitShoot>(out unitShoot))
        {
            unitShoot.OnStartShootAnimation += UnitShoot_OnStartShootAnimation;
        }

        if(TryGetComponent<UnitSlash>(out unitSlash))
        {
            unitSlash.OnStartSlashAnimation += UnitSlash_OnStartSlashAction;
        }

    }

    private void UnitSlash_OnStartSlashAction()
    {
        animator.SetTrigger("slash");
    }

    private void OnDisable()
    {
        if (TryGetComponent<UnitMove>(out unitMove))
        {
            unitMove.OnStopWalkAnimation -= UnitMove_OnStopWalkAnimation;
            unitMove.OnStopWalkAnimation -= UnitMove_OnStopWalkAnimation;
        }
        if (TryGetComponent<UnitShoot>(out unitShoot))
        {
            unitShoot.OnStartShootAnimation -= UnitShoot_OnStartShootAnimation;

        }

    }

    private void UnitMove_OnStartWalkAnimation()
    {
        animator.SetBool("isWalking",true);
    }
    private void UnitMove_OnStopWalkAnimation()
    {
        animator.SetBool("isWalking",false);
    }

    private void UnitShoot_OnStartShootAnimation(Unit targetUnit)
    {
        animator.SetTrigger("shoot");
        BulletEffect bulletEffect =  GameObject.Instantiate(BulletEffect,BulletSpawnPoint).GetComponent<BulletEffect>();
        bulletEffect.SetUp(targetUnit.GetHitPoint().position);

    }


}
