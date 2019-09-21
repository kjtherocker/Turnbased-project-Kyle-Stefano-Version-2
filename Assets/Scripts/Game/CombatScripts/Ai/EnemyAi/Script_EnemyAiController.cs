using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyAiController : Script_AiController
{
    // Start is called before the first frame update

    public Script_AiController m_Target;
    public bool m_AiFinished;
    public bool m_EndMovement;
    public int m_EnemyRange;

}
