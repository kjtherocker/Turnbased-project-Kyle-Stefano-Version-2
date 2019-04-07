﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public enum TriggerType
    {
        Default,
        WaitForObjectToCome
    }



    public List<Dialogue> m_Dialogue;
    public DialogueManager m_DialogueManager;
    public bool DialogueHasHappend;

    public GameObject SpawnPoint_Start;
    public GameObject SpawnPoint_End;

    public GameObject StartObject;
    public GameObject EndObject;

    public GameObject EndObjectInGame;
    public GameObject StartObjectInGame;

    public bool DeleteStartAfterStart;
    public bool DeleteEndOnEnd;

    public bool UseStartObjectFulltime;


    public TriggerType m_TriggerType;



    public bool DialogueIsDone;


    public void Start()
    {
        DialogueHasHappend = false;
        m_DialogueManager = FindObjectOfType<DialogueManager>();

        

    }

    public void Update()
    {

        if (DialogueIsDone == true && DialogueHasHappend == true)
        {
           
              

                if (UseStartObjectFulltime == false)
                {
                   if (EndObject != null)
                   {
                       EndObjectInGame = Instantiate<GameObject>(EndObject, SpawnPoint_End.gameObject.transform);
                       if (EndObjectInGame.GetComponent<Animator>() != null)
                       {
                           EndObjectInGame.GetComponent<Animator>().SetTrigger("End");
                       }
                   }
                }
                else
                {
                    
                    if (StartObjectInGame.GetComponent<Animator>() != null)
                    {
                        StartObjectInGame.GetComponent<Animator>().SetTrigger("End");
                    }

                }
                DialogueIsDone = false;
            
        }


        if (m_TriggerType == TriggerType.WaitForObjectToCome)
        {
            if (DialogueHasHappend == false)
            {
                if (StartObjectInGame != null)
                {
                    if (Vector3.Distance(StartObjectInGame.gameObject.transform.position, new Vector3(23.9f, 12.6f, 0)) < 1)
                    {

                        m_DialogueManager.StartDialogue(m_Dialogue);
                        DialogueHasHappend = true;
                        DialogueIsDone = false;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DialogueHasHappend == false)
            {
                if (m_TriggerType == TriggerType.Default)
                {
                    m_DialogueManager.m_DialogueTrigger = this;
                    m_DialogueManager.StartDialogue(m_Dialogue);
                    DialogueHasHappend = true;
                    DialogueIsDone = false;
                }
                else if(m_TriggerType == TriggerType.WaitForObjectToCome)
                {
                    m_DialogueManager.m_DialogueTrigger = this;
                    m_DialogueManager.StunPlayers();
                    StartObjectInGame = Instantiate<GameObject>(StartObject, SpawnPoint_Start.gameObject.transform);
                    StartObjectInGame.GetComponent<Animator>().SetTrigger("Start");
                }
            }
        }
    }

    public void TriggerDialogue()
    {
        m_DialogueManager.m_DialogueTrigger = this;
        m_DialogueManager.StartDialogue(m_Dialogue);
        DialogueHasHappend = true;
        DialogueIsDone = false;
    }

}
