using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake4 : SnakeClass
{
    
    
    public override void SnakeSelection()
    {
        if (isRayEnabled)
        {
            SnakeSelectionCheck.snake4Selected = true;
            SnakeSelectionCheck.snake1Selected = SnakeSelectionCheck.snake2Selected = SnakeSelectionCheck.snake3Selected =
                SnakeSelectionCheck.snake5Selected = false;
        }
        //Cursor.SetActive(true);
    }
    public override void MovetoNearestGrid(RaycastHit hit)
    {
        if (hit.collider != null && SnakeSelectionCheck.snake4Selected)
        {
            if (hit.collider.gameObject.GetComponent<CanMove>().player4CanMoveToThisTile &&
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
            grid.GetComponent<CanMove>().player4CanMoveToThisTile = false;
        }
    }
    public override void HitCheck(RaycastHit hit)
    {
        if (!isSnakeFinished)
            hit.collider.gameObject.GetComponent<CanMove>().player4CanMoveToThisTile = true;

    }
    public override void GettingGridProperties(RaycastHit hit)
    {
        if (hit.collider.gameObject.GetComponent<CanMove>().player4CanMoveToThisTile &&
                 !hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
        {
            SnakeBehaviour(hit);
        }
        if (hit.collider.gameObject.GetComponent<CanMove>().player4CanMoveToThisTile
            && hit.collider.gameObject.GetComponent<CanMove>().isOccupied)
        {
            AnimationOn();
        }
    }
}
