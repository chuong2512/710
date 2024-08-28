using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode()]
public class LineRendererPoints : MonoBehaviour
{
    LineRenderer Lr;
    public Transform[] Points;
    
    private void Start()
    {
        Lr = GetComponent<LineRenderer>();
        Lr.positionCount = Points.Length;
    }
    private void Update()
    {
        for(int i = 0; i< Points.Length; ++i)
        {
            Lr.SetPosition(i, Points[i].position);
        }
    }
}
