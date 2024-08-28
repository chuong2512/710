using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMove : MonoBehaviour
{
    public bool player1CanMoveToThisTile = false;
    public bool player2CanMoveToThisTile = false;
    public bool player3CanMoveToThisTile = false;
    public bool player4CanMoveToThisTile = false;
    public bool player5CanMoveToThisTile = false;
    public bool isOccupied = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isOccupied = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOccupied = false;
        }
    }
}
