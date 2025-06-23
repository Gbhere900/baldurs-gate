using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystermVisualSingle : MonoBehaviour
{
    [SerializeField] MeshRenderer m_MeshRenderer;

    public void Show(Material gridMaterial)
    {
        m_MeshRenderer.enabled = true;
        m_MeshRenderer.material = gridMaterial;
    }
    public void Hide()
    {
        m_MeshRenderer.enabled =  false;
    }
}
