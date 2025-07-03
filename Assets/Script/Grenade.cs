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
    [SerializeField] private AnimationCurve animationCurve;

    private Vector3 startPosition;
    private Vector3 aimPosition;
    private float sumDistance;
    private Vector3 aimDirection;

    [SerializeField] private GameObject explosionVerticleEffect;

    private Action OnActionCompleted;


   public void SetUp( GridPosition gridPosition,float damage,Action OnActionCompleted, float speed = 5)
    {
        startPosition = transform.position;
        aimPosition = LevelGrid.Instance().GetWorldPosition(gridPosition);
        sumDistance = Vector3.Distance(startPosition, aimPosition);

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

        float distanceReachedPercent = (Vector3.Distance(startPosition, transform.position) / sumDistance);
        Vector3 positon = new Vector3(transform.position.x ,animationCurve.Evaluate(distanceReachedPercent), transform.position.z);
        transform.position = positon;

        if (Vector3.Distance(aimPosition,transform.position) < 0.2)
        {
            Collider[] collider =  Physics.OverlapSphere(transform.position, radius);
            foreach(Collider c in collider)
            {
                if(c.TryGetComponent<Health>( out Health health))
                {
                    health.TakeDamage((int)damage);
                }

                if(c.TryGetComponent<DestructableCrate>(out DestructableCrate destructableCrate))
                {
                    destructableCrate.Destruct();
                }
            }
            GameObject tempExplosionVerticleEffect =  GameObject.Instantiate(explosionVerticleEffect);
            tempExplosionVerticleEffect.transform.position = transform.position;
            CameraShake.Instance().Shake();
            OnActionCompleted();
            Destroy(gameObject);
        }
    }


}
