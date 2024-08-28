using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GridPlacement : MonoBehaviour
{
    Transform Snake;
    private void Start()
    {
        Snake = GameObject.FindWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Grid"))
        {
            Debug.Log("Place");
            Snake.transform.position = other.transform.position;
        }
    }
}
