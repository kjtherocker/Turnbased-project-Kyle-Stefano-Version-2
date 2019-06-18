using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Script_InputManager : MonoBehaviour
{
    public enum InputManagerStates
    {
        True,
        False,
        Nothing

    }

    public bool m_DPadY;
    public bool m_DPadX;

    public bool m_AButton;

    public delegate void Method();
    public Method m_Method;

    bool m_AxisBool = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetXboxAxis(Method aMethod, string aNameOfAxis, bool aPositiveOrNegative, ref bool aReferenceToInput)
    {
        m_Method = aMethod;

        if (Input.GetAxis(aNameOfAxis) == 0)
        {
            aReferenceToInput = false;
        }

        if (aPositiveOrNegative == true)
        {
            if (Input.GetAxis(aNameOfAxis) > 0.9f)
            {
                if (aReferenceToInput == false)
                {
                    m_Method();
                    aReferenceToInput = true;
                }
            }
        }

        if (aPositiveOrNegative == false)
        {
            if (Input.GetAxis(aNameOfAxis) < -0.9f)
            {
                if (aReferenceToInput == false)
                {
                    m_Method();
                    aReferenceToInput = true;
                }
            }
        }


    }


    public InputManagerStates SetPlaystationAxis(string aNameOfAxis, bool aPositiveOrNegative, ref bool aReferenceToInput)
    {
        //  m_Method = aMethod;

        if (Input.GetAxis(aNameOfAxis) == 0)
        {
            aReferenceToInput = false;
        }

        if (aPositiveOrNegative == true)
        {
            if (Input.GetAxis(aNameOfAxis) > 0.9f)
            {
                if (aReferenceToInput == false)
                {
                    aReferenceToInput = true;
                    return InputManagerStates.True;
                }
            }
        }

        if (aPositiveOrNegative == false)
        {
            if (Input.GetAxis(aNameOfAxis) < -0.9f)
            {
                if (aReferenceToInput == false)
                {

                    aReferenceToInput = true;
                    return InputManagerStates.False;
                }
            }
        }

        return InputManagerStates.Nothing;
    }

    public void SetPlaystationButton(Method aMethod, string aNameOfButton, ref bool aReferenceToInput)
    {
        m_Method = aMethod;

        if (Input.GetButtonDown(aNameOfButton))
        {
            if (aReferenceToInput == false)
            {
                m_Method();
                aReferenceToInput = true;
            }
        }

        if (Input.GetButtonUp(aNameOfButton))
        {
            aReferenceToInput = false;
        }
    }

    public void SetXboxButton(Method aMethod, string aNameOfButton, ref bool aReferenceToInput)
    {
        m_Method = aMethod;

        if (Input.GetButtonDown(aNameOfButton))
        {
            if (aReferenceToInput == false)
            {
                m_Method();
                aReferenceToInput = true;
            }
        }

        if (Input.GetButtonUp(aNameOfButton))
        {
            aReferenceToInput = false;
        }
    }

    public void SetPlaystationAxis(Method aMethod, string aNameOfAxis, bool aPositiveOrNegative, ref bool aReferenceToInput)
    {
        m_Method = aMethod;

        if (Input.GetAxis(aNameOfAxis) == 0)
        {
            aReferenceToInput = false;
        }

        if (aPositiveOrNegative == true)
        {
            if (Input.GetAxis(aNameOfAxis) > 0.9f)
            {
                if (aReferenceToInput == false)
                {
                    m_Method();
                    aReferenceToInput = true;
                }
            }
        }

        if (aPositiveOrNegative == false)
        {
            if (Input.GetAxis(aNameOfAxis) < -0.9f)
            {
                if (aReferenceToInput == false)
                {
                    m_Method();
                    aReferenceToInput = true;
                }
            }
        }


    }
}
