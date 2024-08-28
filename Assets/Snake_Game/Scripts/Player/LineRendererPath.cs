using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererPath : MonoBehaviour
{
    LineRenderer Lr;
    public Transform origin, Destination;
   
    private void Start()
    {
      
        Lr = GetComponent<LineRenderer>();
       
    }
    private void Update()
    {
        Lr.SetPosition(0, origin.position);
        Lr.SetPosition(1, Destination.position);
    }
}
