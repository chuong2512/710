using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 gridSize;
    bool isMoving = false;
    Vector3 endPosition;
    float speed = 5f;
    public float dragSpeed = 0.15f;
    //public bool isRight, isLeft, isUp, isDown = false;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            endPosition = transform.position + new Vector3(0, gridSize.y, 0);
            StartCoroutine(Move());
        }
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            endPosition = transform.position + new Vector3(-gridSize.x, 0, 0);
            StartCoroutine(Move());
        }
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            endPosition = transform.position + new Vector3(0, -gridSize.y, 0);
            StartCoroutine(Move());
        }
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            endPosition = transform.position + new Vector3(gridSize.x, 0, 0);
            StartCoroutine(Move());
        }
        //if(isUp)
        //{
        //    endPosition = transform.position + new Vector3(0, gridSize.y, 0);
        //    StartCoroutine(Move());
        //    isUp = false;
        //}
        //if(isDown)
        //{
        //    endPosition = transform.position + new Vector3(0, -gridSize.y, 0);
        //    StartCoroutine(Move());
        //    isDown = false;
        //}
        //if(isRight)
        //{
        //    endPosition = transform.position + new Vector3(gridSize.x, 0, 0);
        //    StartCoroutine(Move());
        //    isRight = false;
        //}
        //if(isLeft)
        //{
        //    endPosition = transform.position + new Vector3(-gridSize.x, 0, 0);
        //    StartCoroutine(Move());
        //    isLeft = false;
        //}

    }


    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        Debug.Log("is Moving");

        isMoving = true;
        Vector3 startPosition = transform.position;
        while(MoveToPoint(startPosition))
        {
           
            yield return null;
        }

        Debug.Log("isMoving = " + isMoving);
        isMoving = false;
    }
    bool MoveToPoint(Vector3 startPosition)
    {
        startPosition = transform.position;
        return endPosition !=(transform.position = Vector3.MoveTowards(startPosition, endPosition, speed * Time.deltaTime));
    }
}
