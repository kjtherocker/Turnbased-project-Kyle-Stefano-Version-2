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
    public GameObject m_AllyHealinglookatpos;
    private Vector3 m_Camera_Offset;
    private Vector3 m_Camera_Offset_EnemyAttack;


    // Use this for initialization
    void Start()
    {
        m_cameraState = CameraState.Default;
        m_Camera_Offset = new Vector3(80, 45, 0);
        m_Camera_Offset_EnemyAttack = new Vector3(40, 20, 0);
        m_SpawnPos.transform.position = new Vector3(500.4f, 68.9f, 376.5f);

        transform.position = m_SpawnPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CharacterReference != null)
        {
            if (m_cameraState == CameraState.Default)
            {
                if (m_CharacterReference.GetCharactertype() == Script_Creatures.Charactertype.Ally)
                {
                    transform.position = Vector3.Slerp(transform.position, m_CharacterReference.ModelInGame.transform.position + m_Camera_Offset, Time.deltaTime * 4.0f);
                    transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
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
                    }
                }
                else
                {

                    m_AllyHealing1.transform.localPosition = new Vector3(1305.7f, 0, 274.7f);
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
                m_SpawnPos.transform.position = new Vector3(1271.4f, 68.9f, 376.5f);
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
