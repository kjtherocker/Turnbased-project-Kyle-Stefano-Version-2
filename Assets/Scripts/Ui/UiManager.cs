using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public enum Screen
    {
        PartyMenu,
        TurnIndicator,
        EndCombatMenu,
        Notifcation,
        CommandBoard,
        PartyStats,
        Dialogue,
        SkillBoard,


        _NumberOfScreens
    }

    public UiScreen[] m_UiScreens;

    public List<KeyValuePair<Screen, UiScreen>> m_ScreenStack = new List<KeyValuePair<Screen, UiScreen>>();

    void OnValidate()
    {
        System.Array.Resize(ref m_UiScreens, (int)Screen._NumberOfScreens);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Screen GetTopScreenType()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Key;
    }

    public UiScreen GetTopScreen()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Value;
    }

    public UiScreen GetScreen(Screen aScreen)
    {
        for (int i = 0; i < m_ScreenStack.Count; i++)
        {
            if (m_ScreenStack[i].Key == aScreen)
            {
                return m_ScreenStack[i].Value;
            }
        }

        return null;
    }

    public void PushScreen(Screen aScreen)
    {
        if (m_ScreenStack.Count != 0)
        {
            m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
        }

        UiScreen screenToAdd = m_UiScreens[(int)aScreen];
        screenToAdd.OnPush();

        m_ScreenStack.Add(new KeyValuePair<Screen, UiScreen>(aScreen, screenToAdd));
        m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
    }

    public void PopScreen()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = false;
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
       
    }

    public void PopInvisivble()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
    }

    public void PopAllInvisivble()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            screenPair.Value.OnPop();
        }
    }

    public void PushToTurnOn()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPush();
    }

    public void PopAllScreens()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            UiScreen Screen = screenPair.Value;

            Screen.OnPop();
        }

        m_ScreenStack.Clear();
    }
}
