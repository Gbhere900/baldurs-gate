using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCrate : MonoBehaviour
{
    private GridPosition gridPositon;

    [SerializeField]private GameObject destructedCrate;

    public static Action<DestructableCrate> OnAnyDestructableCrateDestructed; 

    private void Awake()
    {
        gridPositon = LevelGrid.Instance().GetGridPosition(transform.position);
    }

    public GridPosition GetGridPosition()
    {

        return gridPositon; 
    }


    public void Destruct()
    {
        GameObject.Instantiate(destructedCrate).transform.position = this.transform.position;

        OnAnyDestructableCrateDestructed.Invoke(this);
        Destroy(gameObject);
    }


    public void AddExplosionForce()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform single = transform.GetChild(i);
            if (single.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                float force = UnityEngine.Random.Range(200f, 400f);
                float radius = UnityEngine.Random.Range(2f, 5f);
                Vector3 position = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)) + transform.position;
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
