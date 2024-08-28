using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    void Awake()
    {
        SwipeDetection.onSwipe += SwipeDetection_OnSwipe;
    }

    private void SwipeDetection_OnSwipe(SwipeData data)
    {
        
        if (data.Direction == SwipeDirection.Up)
        {
            UnitMovement.instance.isUp = true;
            Invoke("CancelSwipePosY", .5f);
        }
        if (data.Direction == SwipeDirection.Right)
        {
            UnitMovement.instance.isRight = true;
            Invoke("CancelSwipePosX", .5f);
        }
        if (data.Direction == SwipeDirection.Left)
        {
            UnitMovement.instance.isLeft = true;
            Invoke("CancelSwipePosX", .5f);
        }
        if (data.Direction == SwipeDirection.Down )
        {
            UnitMovement.instance.isDown = true;
            Invoke("CancelSwipePosY", .5f);
        }

        Debug.Log("Swipe in Direction" + data.Direction);
    }
    public void CancelSwipePosY()
    {
        UnitMovement.instance.isUp = false;
        UnitMovement.instance.isDown = false;
    }
    public void CancelSwipePosX()
    {
        UnitMovement.instance.isRight = false;
        UnitMovement.instance.isLeft = false;
    }

}
