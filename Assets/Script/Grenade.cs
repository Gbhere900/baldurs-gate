using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float radius = 4;
    [SerializeField] private float damage = 10;
    private Vector3 aimPosition;
    private Vector3 aimDirection;

    private Action OnActionCompleted;


   public void SetUp( GridPosition gridPosition,float damage,Action OnActionCompleted, float speed = 5)
    {
        aimPosition = LevelGrid.Instance().GetWorldPosition(gridPosition);
        Vector3 direction = LevelGrid.Instance().GetWorldPosition(gridPosition) - transform.position;

        this.damage = damage;
        aimDirection = direction;

        this.OnActionCompleted = OnActionCompleted;
    }
    private void Awake()
    {
        aimDirection = transform.position;
    }

    private void Update()
    {
        transform.position += aimDirection * speed * Time.deltaTime;
        if(Vector3.Distance(aimPosition,transform.position) < 0.2)
        {
            Collider[] collider =  Physics.OverlapSphere(transform.position, radius);
            foreach(Collider c in collider)
            {
                if(c.TryGetComponent<Health>( out Health health))
                {
                    health.TakeDamage((int)damage);
                }
            }
            OnActionCompleted();
            Destroy(gameObject);
        }
    }


}
