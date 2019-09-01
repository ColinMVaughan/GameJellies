using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : MonoBehaviour
{

    [SerializeField] private List<Transform> m_VineTargets;
    [SerializeField] private MeshRenderer m_VineMesh;

    private float m_VineHeight = -5.0f;
    private float m_OldHeight = -5;
    private int m_CurrentTarget = 0;

    private float m_lerpTime;

    private bool m_CurrentlyGrowing = false;

    public float m_Speed = 1.0f;



    //----------------------------------------------------------------
    //METHODS
    //---------------------------------------------------------------

    private void Update()
    {
        if(!m_CurrentlyGrowing)
        {
            TriggerGrowth();
        }
    }

    public void TriggerGrowth()
    {
        if(!m_CurrentlyGrowing && (m_CurrentTarget + 1) < m_VineTargets.Count)
        {
            m_CurrentlyGrowing = true;
            m_OldHeight = m_VineHeight;
            m_CurrentTarget++;
            m_lerpTime = 0.0f;

            StartCoroutine(Grow());
        }
    }

    private void GrowthComplete()
    {
        m_CurrentlyGrowing = false;
    }

    private IEnumerator Grow()
    {

        while(m_lerpTime < 1.0f)
        {
            m_lerpTime += Time.deltaTime * m_Speed;
            m_VineHeight = Mathf.Lerp(m_OldHeight, m_VineTargets[m_CurrentTarget].position.y, m_lerpTime);
            m_VineMesh.material.SetFloat("_ClipPos", m_VineHeight);
            yield return null;
        }

        GrowthComplete();
    }
}
