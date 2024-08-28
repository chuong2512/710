using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int lastPlayedLevel;
    public int maxLevel = 275;

    void Start()
    {
        if (PlayerPrefs.GetInt("ismaxreached") == 0)
            lastPlayedLevel = PlayerPrefs.GetInt("Level");
        else
            lastPlayedLevel = PlayerPrefs.GetInt("aftermaxlevel");
    }

    public void PlayButton()
    {
        AudioMainMenu.instance.Click();
        if (lastPlayedLevel == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastplayedlevel"));

        
       
        
    }
}
