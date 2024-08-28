using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedDistanceMovement : MonoBehaviour,IDragHandler
{
    
    //Vector3 startPosition;
    //Vector3 endPosition;
    //private float fraction; 
    public float dragSpeed = 0.15f;
    Vector3 lastMousePosition;
    public Transform Player;
    private enum DraggedDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    #region  IDragHandler - IEndDragHandler

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Press position + " + eventData.pressPosition);

        Debug.Log("End position + " + eventData.position);

        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;

        Debug.Log("norm + " + dragVectorDirection);

        GetDragDirection(dragVectorDirection);
    }
    //It must be implemented otherwise IEndDragHandler won't work 

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.Lerp(transform.position, Input.mousePosition, .5f);
        
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);

        float positiveY = Mathf.Abs(dragVector.y);

        DraggedDirection draggedDir;

        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
            
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }

        Debug.Log(draggedDir);

        return draggedDir;

    }

    #endregion

    
}
