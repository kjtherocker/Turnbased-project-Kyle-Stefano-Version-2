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


        _NumberOfScreens
    }

    public UiScreen[] m_UiScreens;

    private List<KeyValuePair<Screen, UiScreen>> m_ScreenStack = new List<KeyValuePair<Screen, UiScreen>>();

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

    public void PushScreen(Screen aScreen)
    {
        UiScreen screenToAdd = m_UiScreens[(int)aScreen];
        screenToAdd.OnPush();

        m_ScreenStack.Add(new KeyValuePair<Screen, UiScreen>(aScreen, screenToAdd));
    }

    public void PopScreen()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
    }

    public void PopAllScreens()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            UiScreen Screen = screenPair.Value;

            Screen.GetComponent<UiScreen>().OnPop();
        }

        m_ScreenStack.Clear();
    }
}
