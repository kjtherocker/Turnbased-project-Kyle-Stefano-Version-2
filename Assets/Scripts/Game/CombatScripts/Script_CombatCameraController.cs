using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatCameraController : MonoBehaviour
{
    public enum CameraState
    {
        Nothing,
        Default,
        Spawn,
        AllyHealingSelecting,
        AllyHealing,
        EnemyHealing,
        AllyAttacking,
        AllyAttackSelecting,
        EnemyAttacking,
        EnemyAttackingMelee,
        EnemyZoomIn,
        AllyBuff,
        EnemyBuff


    }

    public CameraState m_cameraState;
    public Script_Creatures m_CharacterReference;
    public Script_Creatures m_OtherCharacterReference;
    public List<Script_Creatures> m_AllCharactersReference;
    public GameObject m_AllyHealingSelectingPosition;
    public GameObject m_SpawnPos;
    public GameObject m_EnemyAttackingPoint1;
    public GameObject m_EnemyAttackingPoint2;
    public GameObject m_AllyHealing1;
    public GameObject m_AllyHealing2;
    public GameObject m_EnemyBuff1;
    public GameObject m_EnemyBuff2;
    public GameObject m_AllyHealinglookatpos;
    public GameObject m_EnemyZoomin;
    private Vector3 m_Camera_Offset;
    private Vector3 m_Camera_Offset_EnemyAttack;


    // Use this for initialization
    void Start()
    {
        Script_GameManager.Instance.m_BattleCamera = this;

        m_AllyHealingSelectingPosition = GameObject.Find("PlayerPosition1");
        m_SpawnPos = GameObject.Find("SpawnPos");
        m_EnemyAttackingPoint1 = GameObject.Find("EnemyAttackPos1");
        m_EnemyAttackingPoint2 = GameObject.Find("EnemyAttackPos2");
        m_AllyHealing1 = GameObject.Find("AllyHealingCameraPosition");
        m_AllyHealing2 = GameObject.Find("AllyHealingCameraPosition2");
        m_EnemyBuff1 = GameObject.Find("EnemyBuffPos1");
        m_EnemyBuff2 = GameObject.Find("EnemyBuffPos2");
        m_AllyHealinglookatpos = GameObject.Find("AllyHealingLookAtPos");
        m_EnemyZoomin = GameObject.Find("EnemyZoomin");



        m_cameraState = CameraState.Default;
        m_Camera_Offset = new Vector3(2, 1.5f, 0);
        m_Camera_Offset_EnemyAttack = new Vector3(2, 1.5f, 0);
        m_SpawnPos.transform.position = new Vector3(18, 6, -1);

        transform.position = m_SpawnPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CharacterReference != null)
        {
            if (m_cameraState == CameraState.EnemyZoomIn)
            {
                m_CharacterReference.ModelInGame.gameObject.transform.rotation = Quaternion.Euler(0.0f,90.0f,0.0f);
                transform.position = m_EnemyZoomin.transform.position;
                m_EnemyZoomin.transform.position = Vector3.Lerp(transform.position, m_CharacterReference.ModelInGame.transform.position +  new Vector3(0, 20, 0), Time.deltaTime * 0.1f);
                transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
                if (Vector3.Distance(transform.position, m_CharacterReference.ModelInGame.transform.position + new Vector3(0, 20, 0)) < 80.0f)
                {
                    m_cameraState = CameraState.Nothing;
                }

            }

            if (m_cameraState == CameraState.Default)
            {
                if (m_CharacterReference.GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {
                    transform.position = Vector3.Slerp(transform.position, m_CharacterReference.ModelInGame.transform.position + m_Camera_Offset, Time.deltaTime * 4.0f);
                    transform.rotation = Quaternion.Euler(21.0f, -90, 0.0f);
                }
            }
            if (m_cameraState == CameraState.AllyHealing)
            {
                if (m_AllCharactersReference[0].GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {

                    transform.position = m_AllyHealing1.transform.position;
                    m_AllyHealing1.transform.position = Vector3.Slerp(m_AllyHealing1.transform.position, m_AllyHealing2.transform.position, Time.deltaTime * 0.5f);

                    transform.LookAt(m_AllyHealinglookatpos.transform.position);

                    if (Vector3.Distance(transform.position, m_AllyHealing2.transform.position) < 80)
                    {
                        m_cameraState = CameraState.Nothing;
                        m_AllyHealing1.transform.localPosition = new Vector3(1350.0f, 0, 587.0f);
                    }
                }
                else
                {

                    //m_AllyHealing1.transform.localPosition = new Vector3(1305.7f, 0, 274.7f);
                }
            }

            if (m_cameraState == CameraState.AllyHealingSelecting)
            {
                if (m_CharacterReference.GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {
                    transform.position = m_AllyHealingSelectingPosition.transform.position;
                    transform.rotation = Quaternion.Euler(0.0f, 111.7f, 0.0f);

                    //  transform.position = Vector3.Slerp(transform.position, m_CharacterReference.ModelInGame.transform.position + m_Camera_Offset, Time.deltaTime * 4.0f);
                }
            }
            if (m_cameraState == CameraState.Spawn)
            {
                transform.position = m_SpawnPos.transform.position;
                //m_EnemyAttackingPoint1.transform.position = Vector3.MoveTowards(transform.position, m_EnemyAttackingPoint2.transform.position, 4);
                m_SpawnPos.transform.position = Vector3.Slerp(m_SpawnPos.transform.position, m_EnemyAttackingPoint2.transform.position, Time.deltaTime * 1);
                transform.rotation = Quaternion.Euler(23.0f, -90, 0.0f);

                if (Vector3.Distance(transform.position, m_SpawnPos.transform.position) < 0.900f)
                {
                    m_cameraState = CameraState.Default;
                }
            }
            else
            {
                m_SpawnPos.transform.localPosition = new Vector3(1931, 271, 382.8235f);
            }
            if (m_cameraState == CameraState.AllyAttackSelecting)
            {
                transform.position = m_EnemyAttackingPoint2.transform.position;
            }
            if (m_cameraState == CameraState.EnemyAttackingMelee)
            {
                transform.position = Vector3.Slerp(transform.position, m_OtherCharacterReference.ModelInGame.transform.position + m_Camera_Offset_EnemyAttack, Time.deltaTime * 4.0f);
                transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
            }
            if (m_cameraState == CameraState.EnemyAttacking)
            {
                transform.position = m_EnemyAttackingPoint1.transform.position;
                m_EnemyAttackingPoint1.transform.position = Vector3.Slerp(m_EnemyAttackingPoint1.transform.position, m_EnemyAttackingPoint2.transform.position, Time.deltaTime * 1);

                transform.rotation = Quaternion.Euler(22.964f, -90, 0.0f);

                if (Vector3.Distance(transform.position, m_EnemyAttackingPoint2.transform.position) < 0.500f)
                {
                    m_cameraState = CameraState.Nothing;
                }
            }
            else
            {

                m_EnemyAttackingPoint1.transform.localPosition = new Vector3(1514, 45, 376.5f);
            }

            if (m_cameraState == CameraState.EnemyBuff)
            {
                transform.position = m_EnemyBuff2.transform.position;
                m_EnemyBuff2.transform.position = Vector3.Slerp(m_EnemyBuff2.transform.position, m_EnemyBuff1.transform.position, Time.deltaTime * 1);

                transform.rotation = Quaternion.Euler(22.964f, 260, 0.0f);

                if (Vector3.Distance(transform.position, m_EnemyAttackingPoint1.transform.position) < 100)
                {
                    m_cameraState = CameraState.Nothing;
                }
            }
            else
            {

                m_EnemyBuff2.transform.localPosition = new Vector3(1745, 267, 406f);
            }
        }

    }

    public CameraState GetCameraState()
    {
        return m_cameraState;
    }

    public void SetCameraState(CameraState a_cameraState)
    {
        m_cameraState = a_cameraState;
    }
    public void SetCharacterReference(Script_Creatures a_Reference)
    {
        m_CharacterReference = a_Reference;
    }
    public void SetOtherCharacterReference(Script_Creatures a_Reference)
    {
        m_OtherCharacterReference = a_Reference;
    }
    public void SetAllCharacterReferences(List<Script_Creatures> a_AllReference)
    {
        m_AllCharactersReference = a_AllReference;
    }


    
}
