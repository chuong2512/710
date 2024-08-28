using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HelperSplit : MonoBehaviour
{
    public void DisableHand1()
    {
        HandTut.instance.handHelperDisable();
    }
    public void DisableHand2()
    {
        HandTut.instance.handHelper2Disable();
    }
    public void EnableHand1()
    {
        HandTut.instance.handHelperEnable();
    }
    public void EnableHand2()
    {
        HandTut.instance.handHelper2Enable();
    }
}
