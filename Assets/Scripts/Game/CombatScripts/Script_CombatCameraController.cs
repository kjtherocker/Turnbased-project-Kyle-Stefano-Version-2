using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CombatCameraController : MonoBehaviour
{
    public enum CameraState
    {
        Default,
        Spawn,
        AllyHealingSelecting,
        AllyHealing,
        EnemyHealing,
        AllyAttacking,
        AllyAttackSelecting,
        EnemyAttacking,
        AllyBuff,
        EnemyBuff


    }

    public CameraState m_cameraState;
    public Script_Creatures m_CharacterReference;
    public GameObject m_AllyHealingSelectingPosition;
    public GameObject m_EnemyAttackingPoint1;
    public GameObject m_EnemyAttackingPoint2;
    private Vector3 m_Camera_Offset;

    // Use this for initialization
    void Start ()
    {
        m_cameraState = CameraState.Default;
        m_Camera_Offset = new Vector3(80, 40, 0);
        m_EnemyAttackingPoint1.transform.position = new Vector3(500.4f, 68.9f, 376.5f);
        transform.position = m_EnemyAttackingPoint1.transform.position;
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
                transform.position = m_EnemyAttackingPoint1.transform.position;
                //m_EnemyAttackingPoint1.transform.position = Vector3.MoveTowards(transform.position, m_EnemyAttackingPoint2.transform.position, 4);
                m_EnemyAttackingPoint1.transform.position = Vector3.Slerp(m_EnemyAttackingPoint1.transform.position, m_EnemyAttackingPoint2.transform.position, Time.deltaTime * 1);
                transform.rotation = Quaternion.Euler(23.0f, -90, 0.0f);

                if (Vector3.Distance(transform.position, m_EnemyAttackingPoint1.transform.position) < 0.900f)
                {
                    m_cameraState = CameraState.Default;
                }
            }
            else
            {
                m_EnemyAttackingPoint1.transform.position = new Vector3(1271.4f, 68.9f, 376.5f);
            }
            if (m_cameraState == CameraState.AllyAttackSelecting)
            {
                transform.position = m_EnemyAttackingPoint2.transform.position;
                transform.rotation = Quaternion.Euler(23.0f, -90, 0.0f);
            }
        }

    }

    public void SetCameraState(CameraState a_cameraState)
    {
        m_cameraState = a_cameraState;
    }
    public void SetCharacterReference(Script_Creatures a_Reference)
    {
        m_CharacterReference = a_Reference;
    }
}
