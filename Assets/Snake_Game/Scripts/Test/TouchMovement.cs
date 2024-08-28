using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float dragSpeed = 0.15f;
    Vector3 lastMousePosition;
    public float rayDistance;
    public LayerMask GridLayer;
    private void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }
    void Update()
    {
        // Right
        Ray rayRight = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        RaycastHit hitRight;
        if (Physics.Raycast(rayRight, out hitRight, rayDistance, GridLayer))
        {

            transform.position = Vector3.Slerp(transform.position, hitRight.collider.transform.position, 1f);
            Debug.DrawLine(rayRight.origin, hitRight.point, Color.red);

        }
        else
        {
            Debug.DrawLine(rayRight.origin, rayRight.origin + rayRight.direction * rayDistance, Color.blue);
        }
        // Left
        Ray rayLeft = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        RaycastHit hitLeft;
        if (Physics.Raycast(rayLeft, out hitLeft, rayDistance, GridLayer))
        {

            transform.position = hitLeft.collider.transform.position;
            Debug.DrawLine(rayLeft.origin, hitLeft.point, Color.red);

        }
        else
        {
            Debug.DrawLine(rayLeft.origin, rayLeft.origin + rayLeft.direction * rayDistance, Color.blue);
        }
        // Up
        Ray rayUp = new Ray(transform.position, transform.TransformDirection(Vector3.up));
        RaycastHit hitUp;
        if (Physics.Raycast(rayUp, out hitUp, rayDistance, GridLayer))
        {

            transform.position = hitUp.collider.transform.position;
            Debug.DrawLine(rayUp.origin, hitUp.point, Color.red);

        }
        else
        {
            Debug.DrawLine(rayUp.origin, rayUp.origin + rayUp.direction * rayDistance, Color.blue);
        }
        // Down
        Ray rayDown = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        RaycastHit hitDown;
        if (Physics.Raycast(rayDown, out hitDown, rayDistance, GridLayer))
        {

            transform.position = hitDown.collider.transform.position;
            Debug.DrawLine(rayDown.origin, hitDown.point, Color.red);

        }
        else
        {
            Debug.DrawLine(rayDown.origin, rayDown.origin + rayDown.direction * rayDistance, Color.blue);
        }

    }
    private void OnMouseDrag()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        Vector3 pos = transform.position;
        pos.y += delta.y * dragSpeed * Time.deltaTime;
        pos.x += delta.x * dragSpeed * Time.deltaTime;
        transform.position = pos;
        lastMousePosition = Input.mousePosition;
    }
}
