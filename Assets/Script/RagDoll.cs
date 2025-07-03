using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    [SerializeField] private Transform root;

    public void SetUpClone(Transform Clone, Transform originRoot)
    {
        for(int i=0;i< Clone.childCount;i++)
        {
            Transform bone = Clone.GetChild(i);
            if(originRoot.Find(bone.name) == null)
                continue;
            bone.transform.position = originRoot.Find(bone.name).position;
            bone.transform.rotation = originRoot.Find(bone.name).rotation;


            if(bone.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                float force = Random.Range(500f, 900f);
                float radius = Random.Range(2f, 5f);
                Vector3 position = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) + transform.position;
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
            

            SetUpClone(Clone.GetChild(i), originRoot.Find(bone.name));
        }
    }

    public Transform GetRoot()
    {
        return root;
    }

}
