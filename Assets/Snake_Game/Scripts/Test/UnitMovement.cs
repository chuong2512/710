using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public static UnitMovement instance;
    public float rayDistance;
    public LayerMask GridLayer;
    public Transform LeftRayCastPos;
    public Transform RightRayCastPos;
    public Transform UpRayCastPos;
    public Transform DownRayCastPos;
    public float dragSpeed = 0.15f;
    Vector3 lastMousePosition;
    Vector3 currentPos;
    public bool isUp, isDown, isLeft, isRight = false;

    private void Start()
    {
        instance = this;
        currentPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //DirectionDetection();
        // Right
        Ray rayRight = new Ray(RightRayCastPos.position, RightRayCastPos.TransformDirection(Vector3.right));
        RaycastHit hitRight;
        if (Physics.Raycast(rayRight, out hitRight, rayDistance, GridLayer))
        {
            //transform.position = hitRight.collider.transform.position;
            Debug.DrawLine(rayRight.origin, hitRight.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayRight.origin, rayRight.origin + rayRight.direction * rayDistance, Color.blue);
        }
        // Left
        Ray rayLeft = new Ray(LeftRayCastPos.position, LeftRayCastPos.TransformDirection(Vector3.left));
        RaycastHit hitLeft;
        if (Physics.Raycast(rayLeft, out hitLeft, rayDistance, GridLayer))
        {
            //transform.position = hitLeft.collider.transform.position;
            Debug.DrawLine(rayLeft.origin, hitLeft.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayLeft.origin, rayLeft.origin + rayLeft.direction * rayDistance, Color.blue);
        }
        // Up
        Ray rayUp = new Ray(UpRayCastPos.position, UpRayCastPos.TransformDirection(Vector3.up));
        RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, rayDistance, GridLayer))
        {
            //transform.position = hitUp.collider.transform.position;
            Debug.DrawLine(rayUp.origin, hitUp.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayUp.origin, rayUp.origin + rayUp.direction * rayDistance, Color.blue);
        }
        // Down
        Ray rayDown = new Ray(DownRayCastPos.position, DownRayCastPos.TransformDirection(Vector3.down));
        RaycastHit hitDown;
        if (Physics.Raycast(rayDown, out hitDown, rayDistance, GridLayer))
        {
            //transform.position = hitDown.collider.transform.position;
            Debug.DrawLine(rayDown.origin, hitDown.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayDistance, Color.blue);
        }
    }
    private void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        Vector3 pos = transform.position;
        if (isUp || isDown)
        {
            pos.y += delta.y * dragSpeed * Time.deltaTime;
            
        }
        if(isRight || isLeft)
        {
            pos.x += delta.x * dragSpeed * Time.deltaTime;
            
        }
        //pos.x += delta.x * dragSpeed * Time.deltaTime;
        transform.position = pos;
        lastMousePosition = Input.mousePosition;
    }
    private void OnMouseUp()
    {
        
        currentPos = transform.position;
    }
    public void DirectionDetection()
    {
        
        if(transform.position.x > currentPos.x)
        {
            Debug.Log("SwipeDirection is Right");
        }
        if(transform.position.x < currentPos.x)
        {
            Debug.Log("SwipeDirection is Left");
        }

    }
}
