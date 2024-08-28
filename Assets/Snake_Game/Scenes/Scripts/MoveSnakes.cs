using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSnakes : MonoBehaviour
{
    public float rayDistance;
    public LayerMask GridLayer;
    public float dragSpeed = 0.15f;
    Vector3 lastMousePosition;
    public bool CanMoveRight,CanMoveLeft,CanMoveUp,CanMoveDown = false;
    private void Update()
    {
        
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        RaycastHit hitRight;

        // Right
        if (Physics.Raycast(rayRight, out hitRight, rayDistance, GridLayer))
        {
            CanMoveRight = true;
            //transform.position = Vector3.Slerp(transform.position, hitRight.collider.transform.position, 1f);
            Debug.DrawLine(rayRight.origin, hitRight.point, Color.red);

        }
        else
        {
            CanMoveRight = false;
            Debug.DrawLine(rayRight.origin, rayRight.origin + rayRight.direction * rayDistance, Color.blue);
        }

        // Left
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        RaycastHit hitLeft;
        if (Physics.Raycast(rayLeft, out hitLeft, rayDistance, GridLayer))
        {
            CanMoveLeft = true;
            Debug.DrawLine(rayLeft.origin, hitLeft.point, Color.red);
            
        }
        else
        {
            CanMoveLeft = false;
            Debug.DrawLine(rayLeft.origin, rayLeft.origin + rayLeft.direction * rayDistance, Color.blue);
        }

        // Up
        Ray rayUp = new Ray(transform.position, transform.TransformDirection(Vector3.up));
        RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, rayDistance, GridLayer))
        {
            CanMoveUp = true;
            Debug.DrawLine(rayUp.origin, hitUp.point, Color.red);
        }
        else
        {
            CanMoveUp = false;
            Debug.DrawLine(rayUp.origin, rayUp.origin + rayUp.direction * rayDistance, Color.blue);
        }

        Ray rayDown = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        RaycastHit hitDown;
        if (Physics.Raycast(rayDown, out hitDown, rayDistance, GridLayer))
        {
            CanMoveDown = true;
            Debug.DrawLine(rayDown.origin, hitDown.point, Color.red);
        }
        else
        {
            CanMoveDown = false;
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayDistance, Color.blue);
        }
    }
    private void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        
        Debug.Log(inputX);
        Vector3 delta = Input.mousePosition - lastMousePosition;
        Vector3 pos = transform.position;
        if (inputX > 0.05 || inputX < 0.05)
        {
            pos.x += delta.x * dragSpeed;
            
        }
        if (inputY > 0.05 || inputY < 0.05)
        {
            pos.y += delta.y * dragSpeed;
            return;
        }
        transform.position = pos;
        lastMousePosition = Input.mousePosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Grid"))
        {
            this.transform.position = other.gameObject.transform.position;
            
        }
    }
}

