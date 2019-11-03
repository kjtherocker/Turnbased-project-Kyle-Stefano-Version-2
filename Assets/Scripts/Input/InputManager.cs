using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public PlayerInput m_MovementControls;
    public PlayerInput m_MenuControls;

    // Use this for initialization
    void Awake()
    {
        m_MovementControls = new PlayerInput();
        m_MovementControls.Enable();

        m_MenuControls = new PlayerInput();
        m_MenuControls.Disable();

    }


}
