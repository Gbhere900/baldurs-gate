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
            SetUpClone(Clone.GetChild(i), originRoot.Find(bone.name));
        }
    }

    public Transform GetRoot()
    {
        return root;
    }

}
