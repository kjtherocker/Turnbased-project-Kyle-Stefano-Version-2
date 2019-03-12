//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class DialogueManager : MonoBehaviour
//{

//    public enum ChatBoxType
//    {
//        White,
//        Black
//    }



//    public enum FontTypes
//    {
//        RifficFree,
//        MegaTron,
//        Metalord,
//        RedRocket,
//        Thunderstrike,
//        LuckiestGuy,


//        NumberOfFonts
//    }

//    public enum Portrait
//    {
//        BlueKnight,
//        YellowKnight,
//        OrangeKnight,
//        GreenKnight,
//        MechaSean,
//        WereCoon,
//        NumberOfPortraits }

//    [SerializeField] Texture[] m_TypesPortraits;
//    [SerializeField] Font[] m_TypesOfFonts;
//    public List<Dialogue> m_DialogueList;
//    public List<GameObject> m_DialogueObjects;
//    public GameObject[] m_PlayerControllers;
//    public Text m_DisplayText;
//    public Canvas m_DialogueCanvas;

//    public RawImage m_ChatBox;
//    public RawImage m_Portrait;

//    public Texture m_WhiteChatBox;
//    public Texture m_BlackChatBox;

//    int ObjectToDestroy;
//    public bool RemoveGameObject;

//    // Use this for initialization
//    void Start()
//    {
//        m_DialogueList = new List<Dialogue>();
//        m_DialogueCanvas.gameObject.SetActive(false);

//    }

//    public void StartDialogue(List<Dialogue> aDialogue)
//    {

//        if (Constants.Constants.TurnDialogueOff == false)
//        {
//            //m_Sentances.Clear();
//            m_DialogueList = aDialogue;
//            m_DialogueCanvas.gameObject.SetActive(true);

//            m_PlayerControllers = GameObject.FindGameObjectsWithTag("Player");

//            foreach (GameObject aPlayercontroller in m_PlayerControllers)
//            {
//                aPlayercontroller.GetComponent<PlayerController>().ClearAnimationTriggers();
//                aPlayercontroller.GetComponent<PlayerController>().isMoving = false;
//                aPlayercontroller.GetComponent<PlayerController>().isStunned = true;
//            }

//            DisplayNextSentence();
//        }
//    }

//    public void DisplayNextSentence()
//    {
//        if (RemoveGameObject == true)
//        {

//            Destroy(m_DialogueObjects[0]);
//            m_DialogueObjects.RemoveAt(0);

//        }
//        if (m_DialogueList.Count == 0)
//        {
//            EndDialogue();
//            return;
//        }

//        if (m_DialogueList[0].m_ChatBoxType == ChatBoxType.White)
//        {
//            m_ChatBox.texture = m_WhiteChatBox;
//            m_DisplayText.color = Color.black;
//        }
//        else if (m_DialogueList[0].m_ChatBoxType == ChatBoxType.Black)
//        {
//            m_ChatBox.texture = m_BlackChatBox;
//            m_DisplayText.color = Color.white;
//        }

//        SetPortrait(m_DialogueList[0].m_PortraitType);
//        SetFont(m_DialogueList[0].m_FontTypes);


//        if (m_DialogueList[0].m_GameObjectToAppearInCutscene != null)
//        {
//            GameObject temp;
//            temp = Instantiate<GameObject>(m_DialogueList[0].m_GameObjectToAppearInCutscene, m_DialogueList[0].m_GameobjectSpawnPoint.gameObject.transform);


//            m_DialogueObjects.Add(temp);
//        }
//        string sentence = m_DialogueList[0].m_Sentances;

//        m_DisplayText.text = sentence;

//        RemoveGameObject = m_DialogueList[0].DestroyGameObjectOnEndOfDialogue;
//        ObjectToDestroy = m_DialogueList.Count;

//        m_DialogueList.RemoveAt(0);
//    }

//    public void Update()
//    {
//        if (Input.GetButtonDown("A_1"))
//        {
//            DisplayNextSentence();
//        }
//    }

//    public void SetPortrait(DialogueManager.Portrait aPortrait, string sourceName = "Global")
//    {
//        m_Portrait.texture = m_TypesPortraits[(int)aPortrait];
//    }

//    public void SetFont(FontTypes aFont, string sourceName = "Global")
//    {
//        m_DisplayText.font = m_TypesOfFonts[(int)aFont];

//    }

//    public void EndDialogue()
//    {

//        foreach (GameObject aPlayercontroller in m_PlayerControllers)
//        {
//            aPlayercontroller.GetComponent<PlayerController>().isStunned = false;
//        }

//        if (m_DialogueObjects != null)
//        {
//            for (int i = 0; i < m_DialogueObjects.Count; i++)
//            {
//                Destroy(m_DialogueObjects[i]);
//                m_DialogueObjects.RemoveAt(i);
//            }
//        }
//        m_DialogueCanvas.gameObject.SetActive(false);

//    }


//}
