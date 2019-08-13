using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{

    public enum ChatBoxType
    {
        White,

    }



    public enum FontTypes
    {
        Arial,

        NumberOfFonts
    }

    [SerializeField] Texture[] m_TypesPortraits;
    [SerializeField] Font[] m_TypesOfFonts;
    public List<Dialogue> m_DialogueList;
    public List<GameObject> m_DialogueObjects;

    public DialogueTrigger m_DialogueTrigger;


    
    public TextMeshProUGUI m_DisplayText;
    public Text m_DisplayName;
    public Canvas m_DialogueCanvas;

    public RawImage m_ChatBox;
    public RawImage m_Portrait;

    public Script_OverworldCamera m_OverworldCamera;

    public int AnimatedTextiterator;
    public string CurrentText;

    int ObjectToDestroy;
    public bool RemoveGameObject;
    public bool TextScroll;
    // Use this for initialization
    void Start()
    {
        m_DialogueList = new List<Dialogue>();
        //m_DialogueCanvas.gameObject.SetActive(false);
        TextScroll = false;
    }

    public void StartDialogue(List<Dialogue> aDialogue)
    {

        if (Constants.Constants.TurnDialogueOff == false)
        {

            //m_Sentances.Clear();

            m_OverworldCamera.gameObject.SetActive(false); 
            m_DialogueList = aDialogue;
            m_DialogueCanvas.gameObject.SetActive(true);


            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (RemoveGameObject == true)
        {

            Destroy(m_DialogueObjects[0]);
            m_DialogueObjects.RemoveAt(0);

        }
        if (m_DialogueList.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (m_DialogueList[0].m_ChatBoxType == ChatBoxType.White)
        {
          //  m_DisplayText.color = Color.white;
        }


       // SetPortrait(m_DialogueList[0].m_PortraitType);
        SetFont(m_DialogueList[0].m_FontTypes);


        if (m_DialogueList[0].m_GameObjectToAppearInCutscene != null)
        {
            GameObject temp;
            temp = Instantiate<GameObject>(m_DialogueList[0].m_GameObjectToAppearInCutscene, m_DialogueList[0].m_GameobjectSpawnPoint.gameObject.transform);


            m_DialogueObjects.Add(temp);
        }
        CurrentText = m_DialogueList[0].m_Sentances;
     
        AnimateText(CurrentText);
        m_DisplayName.text = m_DialogueList[0].m_Name;

        RemoveGameObject = m_DialogueList[0].DestroyGameObjectOnEndOfDialogue;
        ObjectToDestroy = m_DialogueList.Count;

        m_DialogueList.RemoveAt(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (TextScroll == true && m_DialogueList.Count > 0)
            {
                StopAllCoroutines();
                //StopCoroutine(ParseText(CurrentText, true));
                ShowTextWithParse(CurrentText);


            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void AnimateText(string strComplete)
    {

        AnimatedTextiterator = 0;
        m_DisplayText.text = "";
        TextScroll = true;

       
        if (TextScroll == true)
        {
            StartCoroutine(ParseText(strComplete,true));
            
        }
        else
        {

          //  StartCoroutine(ParseText(strComplete, false));

            //m_DisplayText.text = strComplete;
           // m_DisplayText.text += " ";
           
        }
        
        //m_DisplayText.text += " ";
        
        
    }

    public IEnumerator ParseText(string strComplete, bool a_IsScrolling)
    {
        int Line = 1;
        int CharactersBetweenLines = 0;
        int OverflowAmount = 40;
        int OverflowOffset;
        int WordOffset;

        while (AnimatedTextiterator <= strComplete.Length)
        {
            bool m_WorkSpace;


            if (TextScroll == true)
            {
                if (strComplete[AnimatedTextiterator].ToString() == " ")
                {
                    m_DisplayText.text += strComplete[AnimatedTextiterator++];

                    OverflowOffset = OverflowAmount * Line;
                    CharactersBetweenLines = AnimatedTextiterator;
                    WordOffset = AnimatedTextiterator;
                    m_WorkSpace = true;

                    while (m_WorkSpace == true)
                    {
                        if (TextScroll == true)
                        {
                            if (WordOffset < strComplete.Length)
                            {
                                if (strComplete[WordOffset].ToString() == " ")
                                {
                                    m_WorkSpace = false;

                                }
                                else
                                {
                                    CharactersBetweenLines++;
                                    WordOffset++;
                                }
                            }
                            else
                            {
                                m_WorkSpace = false;
                                TextScroll = false;
                            }
                        }
                    }

                    if (TextScroll == true)
                    {
                        if (CharactersBetweenLines >= OverflowOffset)
                        {


                            CharactersBetweenLines = 0;
                            Line++;

                            m_DisplayText.text += "                                                                   ";




                            m_DisplayText.text += strComplete[AnimatedTextiterator++];

                            if (a_IsScrolling == true)
                            {
                                yield return new WaitForSeconds(0.05F);
                            }
                            else
                            {
                                yield return null;
                            }
                        }
                        else
                        {
                            m_DisplayText.text += strComplete[AnimatedTextiterator++];

                            if (a_IsScrolling == true)
                            {
                                yield return new WaitForSeconds(0.05F);
                            }
                            else
                            {
                                yield return null;
                            }

                        }
                    }
                }
                else
                {
                    m_DisplayText.text += strComplete[AnimatedTextiterator++];
                    if (a_IsScrolling == true)
                    {
                        yield return new WaitForSeconds(0.05F);
                    }
                    else
                    {
                        yield return null;
                    }

                }
            }
            else
            {
                break;
            }
        }
    }

    public void ShowTextWithParse(string strComplete)
    {
        int Line = 1;
        int CharactersBetweenLines = 0;
        int OverflowAmount = 37;
        int OverflowOffset;
        int WordOffset;
        m_DisplayText.text = "";
        AnimatedTextiterator = 0;
        while (AnimatedTextiterator < strComplete.Length)
        {
            bool m_WorkSpace;
            if (TextScroll == true)
            {
                if (strComplete[AnimatedTextiterator].ToString() == " ")
                {
                    m_DisplayText.text += strComplete[AnimatedTextiterator++];

                    OverflowOffset = OverflowAmount * Line;
                    CharactersBetweenLines = AnimatedTextiterator;
                    WordOffset = AnimatedTextiterator;
                    m_WorkSpace = true;

                    while (m_WorkSpace == true)
                    {
                        if (WordOffset < strComplete.Length)
                        {
                            if (strComplete[WordOffset].ToString() == " ")
                            {
                                m_WorkSpace = false;

                            }
                            else
                            {
                                CharactersBetweenLines++;
                                WordOffset++;
                            }
                        }
                        else
                        {
                            m_WorkSpace = false;
                            TextScroll = false;
                        }
                    }

                    if (TextScroll == true)
                    {
                        if (CharactersBetweenLines >= OverflowOffset)
                        {


                            CharactersBetweenLines = 0;
                            Line++;

                            m_DisplayText.text += "                                                                   ";




                            m_DisplayText.text += strComplete[AnimatedTextiterator++];


                        }
                        else
                        {
                            m_DisplayText.text += strComplete[AnimatedTextiterator++];



                        }
                    }
                }
                else
                {
                    m_DisplayText.text += strComplete[AnimatedTextiterator++];

                }
            }
            else
            {
                m_WorkSpace = false;
                TextScroll = false;
                break;
            }
        }
        
    }

    public void SetPortrait(Constants.Portrait aPortrait, string sourceName = "Global")
    {
        m_Portrait.texture = m_TypesPortraits[(int)aPortrait];
    }

    public void SetFont(FontTypes aFont, string sourceName = "Global")
    {
       // m_DisplayText.fontStyle = m_TypesOfFonts[(int)aFont];

    }

    public void StunPlayers()
    {
        
    }

    public void EndDialogue()
    {

    
        if (m_DialogueObjects != null)
        {
            for (int i = 0; i < m_DialogueObjects.Count; i++)
            {
                Destroy(m_DialogueObjects[i]);
                m_DialogueObjects.RemoveAt(i);
            }
        }
        m_DialogueCanvas.gameObject.SetActive(false);
        if (m_DialogueTrigger != null)
        {
            m_DialogueTrigger.DialogueIsDone = true;
        }
        Destroy(m_DialogueTrigger.gameObject);
        m_OverworldCamera.gameObject.SetActive(true);
    }


}
