using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public float minDist = .25f;
    public float speed = 1;
    public float rotSpeed = 50;
    public GameObject bodyPrefab;
    private float distance;
    private Transform CurrentBodyPart;
    private Transform previousBodyPart;
    public int startSize;
    void Start()
    {
        for(int i = 0; i < startSize -1; i++)
        {
            AddSnakeBodyPart();
        }
    }

    
    void Update()
    {
        Movement();

    }
    
    public void Movement()
    {
        float currentSpeed = speed;
        if(Input.GetKey(KeyCode.W))
        {
            currentSpeed *= 2;
        }
        bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);
        if(Input.GetAxis("Horizontal") != 0)
        {
            bodyParts[0].Rotate(Vector3.up * rotSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }
        for ( int i = 1; i < bodyParts.Count; i++)
        {
            CurrentBodyPart = bodyParts[i];
            previousBodyPart = bodyParts[i - 1];
            distance = Vector3.Distance(previousBodyPart.position, CurrentBodyPart.position);
            Vector3 newPosition = previousBodyPart.position;
            newPosition.y = bodyParts[0].position.y;
            float time = Time.deltaTime * distance / minDist * currentSpeed;
            if(time > 0.5f)
            {
                time = 0.5f;
                CurrentBodyPart.position = Vector3.Slerp(CurrentBodyPart.position, newPosition, time);
                CurrentBodyPart.rotation = Quaternion.Slerp(CurrentBodyPart.rotation, previousBodyPart.rotation, time);
            }
        }
    }
    public void AddSnakeBodyPart()
    {
        Transform newPart = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;
        newPart.SetParent(transform);
        bodyParts.Add(newPart);
    }
}
