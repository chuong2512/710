using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBehaviour
{
    void Move();
    void SnakeBehaviour(RaycastHit hit);
    void GettingGridProperties(RaycastHit hit);
    void GridBooleans();
    void RaycastCheck();
   
}
