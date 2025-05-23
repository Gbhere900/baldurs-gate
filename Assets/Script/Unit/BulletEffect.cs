using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    private Vector3 targetPosition;
    private Vector3 direction;
    private float distance;
    [SerializeField] private GameObject BulletHitEffect;
    [SerializeField] private float speed = 30; 

    public void SetUp(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        direction = (targetPosition - transform.position).normalized;
        distance = (targetPosition - transform.position).magnitude;
    }
    private void Update()
    {
        float currentDistance1 = (targetPosition - transform.position).magnitude;
        transform.position += direction *speed * Time.deltaTime;
        float currentDistance2 = (targetPosition - transform.position).magnitude;
        if (currentDistance1 < currentDistance2)
        {
            Debug.LogWarning(targetPosition);
            trailRenderer.transform.parent = null;
            GameObject bulletHitEffect =  GameObject.Instantiate(BulletHitEffect,targetPosition,Quaternion.identity);

            Destroy(gameObject);
        }


    }

    
}
