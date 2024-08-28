using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int pointsToWin;
    public int currentPoints = 0;
    GameObject AllSnakes;
    GameObject[] targetSnakes;
    public bool isVictory = false;
    private void Awake()
    {
        instance = this;
    }
    public void IntitializeScore()
    {
        AllSnakes = GameObject.FindWithTag("AllSnakes");
        targetSnakes = new GameObject[AllSnakes.transform.childCount];
        for (int i = 0; i < AllSnakes.transform.childCount; i++)
        {
            targetSnakes[i] = AllSnakes.transform.GetChild(i).gameObject;
        }
        pointsToWin = targetSnakes.Length;
    }
    public void AddScorePoints()
    {
        currentPoints += 1;
        Debug.Log(currentPoints + " SnakeEscaped From the Box");
    }
    public void CheckForVictory()
    {
        if (currentPoints == pointsToWin)
        {
            isVictory = true;
        }
    }
}
