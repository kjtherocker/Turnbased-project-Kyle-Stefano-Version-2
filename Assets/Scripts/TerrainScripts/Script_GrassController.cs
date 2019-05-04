using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class Script_GrassController : MonoBehaviour
{

    public float radius = 0.5f;
    public float softness = 0.5f;
    public Vector3 CirclePosition;
    public GameObject m_ChantReference;
    public List<ParticleSystem> m_DarkRingParticles;
    public ParticleSystem m_DarkRingReference;
    public ParticleSystem m_DarkAuraReference;
    public ParticleSystem m_DarkParticlesReference;
    public GameObject ForestEnviorment;
    public GameObject DomainEnviorment;
    public Script_CombatCameraController m_CameraReference;
    public Script_Creatures m_RedeyesReference;

    bool DomainHasSpawned;
    bool ChantHasSpawwed;
    bool m_IsEnroaching;
    bool m_EnroachingIsFinished;

    float m_EnroachDistance;
    void Start()
    {
        DomainHasSpawned = false;
        ChantHasSpawwed = false;
        m_IsEnroaching = false;
        m_EnroachingIsFinished = false;
        m_EnroachDistance = 80;
        softness = 0;
    }

    void Update()
    {
        Shader.SetGlobalVector("_GLOBALMaskPosition", CirclePosition);
        Shader.SetGlobalFloat("_GLOBALMaskRadius", radius);
        Shader.SetGlobalFloat("_GLOBALMaskSoftness", softness);

        if (m_RedeyesReference != null)
        {
            SetUpDomainCircle();
        }

        if (m_IsEnroaching == true)
        {
            if (m_EnroachingIsFinished == false)
            {
                EncrochDomain();
            }
        }

        if (softness >= m_EnroachDistance)
        {
            m_IsEnroaching = false;
            m_EnroachDistance += 40;
        }
        
    }

    public void SetRedEyesReference(Script_Creatures a_Redeyesref)
    {
        m_RedeyesReference = a_Redeyesref;
    }

    void SetUpDomainCircle()
    {
        if (DomainHasSpawned == false)
        {
            //CirclePosition = new Vector3( m_RedeyesReference.transform.localPosition.x,0, m_RedeyesReference.transform.localPosition.y);

           //m_DarkRingParticles.Add(Instantiate(m_DarkRingReference, CirclePosition, Quaternion.identity));
           //m_DarkRingParticles.Add(Instantiate(m_DarkAuraReference, CirclePosition, Quaternion.identity));
           //m_DarkRingParticles[1].gameObject.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
           //m_DarkRingParticles.Add(Instantiate(m_DarkParticlesReference, CirclePosition, Quaternion.identity));
           //m_DarkRingParticles[2].gameObject.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            DomainHasSpawned = true;
            softness = 36;
        }
       

    }


    public void IsEncoraching()
    {
        m_IsEnroaching = true;
    }
    public void EncrochDomain()
    {
        
       //m_DarkRingParticles[0].transform.localScale += new Vector3(13.257f, 13.240f, 10.423f) * Time.deltaTime * 0.185f;
       //m_DarkRingParticles[1].transform.localScale += new Vector3(7.476724f, 8.400129f, 6.613211f) * Time.deltaTime * 0.185f;
       //m_DarkRingParticles[2].transform.localScale += new Vector3(7.476724f, 8.400129f, 6.613211f) * Time.deltaTime * 0.185f;

        softness += 60.0f * 0.002f;
    }
}