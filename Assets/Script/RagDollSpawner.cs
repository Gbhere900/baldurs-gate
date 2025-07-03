using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollSpawner : MonoBehaviour
{
    [SerializeField] private RagDoll ragDollPrefabs;
    [SerializeField] private Transform originRoot;
    public void SpawnRagDoll()
    {
        RagDoll ragdoll =  GameObject.Instantiate(ragDollPrefabs,transform.position,Quaternion.identity,null);
        ragdoll.SetUpClone(ragdoll.GetRoot(),originRoot);
    }

}
