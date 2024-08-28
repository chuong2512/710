using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTut : MonoBehaviour
{
    public static HandTut instance;
    public GameObject HandHelper;
    public GameObject HandHelper2 = null;
   
    void Start()
    {
        instance = this;
        HandHelper.SetActive(true);
        if(HandHelper2 != null)
            HandHelper2.SetActive(false);
    }
    
    public void handHelperDisable()
    {
        if(HandHelper == null)
        {
            return;
        }
        HandHelper.SetActive(false);
       
    }
    public void handHelperEnable()
    {
        if (HandHelper == null)
        {
            return;
        }
        HandHelper.SetActive(true);
       
    }

    public void handHelper2Disable()
    {
        if (HandHelper2 == null)
        {
            return;
        }
        HandHelper2.SetActive(false);
       
    }
    public void handHelper2Enable()
    {
        if (HandHelper2 == null)
        {
            return;
        }
        HandHelper2.SetActive(true);
       
    }
    

}
