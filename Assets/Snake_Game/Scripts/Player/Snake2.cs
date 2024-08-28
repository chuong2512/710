using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : SnakeClass
{
    public override void SnakeSelection()
    {
        if (isRayEnabled)
        {
            SnakeSelectionCheck.snake2Selected = true;
            SnakeSelectionCheck.snake1Selected = SnakeSelectionCheck.snake3Selected = SnakeSelectionCheck.snake4Selected
                = SnakeSelectionCheck.snake5Selected = false;
        }
        //Cursor.SetActive(true);
    }
    public override void MovetoNearestGrid(RaycastHit hit)
    {
        if (hit.collider != null && SnakeSelectionCheck.snake2Selected)
        {
            if (hit.collider.gameObject.GetComponent<CanMove>().player2CanMoveToThisTile &&
                         !hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                SnakeBehaviour(hit);
            }

        }
    }
    public override void GridBooleans()
    {
        foreach (GameObject grid in Grids)
        {
            grid.GetComponent<CanMove>().player2CanMoveToThisTile = false;
        }
    }
    public override void HitCheck(RaycastHit hit)
    {
        if (!isSnakeFinished)
            hit.collider.gameObject.GetComponent<CanMove>().player2CanMoveToThisTile = true;
        
    }
    public override void GettingGridProperties(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Grid"))
        {
            if (hit.collider.gameObject.GetComponent<CanMove>().player2CanMoveToThisTile && 
                !hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                SnakeBehaviour(hit);
            }
            if (hit.collider.gameObject.GetComponent<CanMove>().player2CanMoveToThisTile 
                && hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
            {
                AnimationOn();
            }
        }
    }
    //public override void OnTriggerEnter(Collider other)
    //{
    //    CheckPos2.Add(other.gameObject.transform.position);
    //}
}
