using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    [Header("Pause")]
    public bool isPaused = false;
    public GameObject pauseText;

    [Header("Restart Game")]
    public int sceneIndex;

    [Header("Victory Window")]
    public GameObject LevelVictoryWindow;
    public GameObject EmojisHolder;
    public GameObject GoodWordsHolder;
    GameObject[] Emojis;
    GameObject[] GoodWords;

    [Header("Level Index")]
    public TextMeshProUGUI levelText;
    int TotalLevels = 275;
    int sceneIndexafterMaxLevelReached;
    int isMaxLevelReached;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerPrefs.SetInt("lastplayedlevel", SceneManager.GetActiveScene().buildIndex);
        pauseText.SetActive(false);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LevelVictoryWindow.SetActive(false);
        isMaxLevelReached = PlayerPrefs.GetInt("ismaxreached");
        sceneIndexafterMaxLevelReached = PlayerPrefs.GetInt("aftermaxlevel");
        Emojis = new GameObject[EmojisHolder.transform.childCount];
        for (int i = 0; i < EmojisHolder.transform.childCount; i++)
        {
            Emojis[i] = EmojisHolder.transform.GetChild(i).gameObject;
        }
        GoodWords = new GameObject[GoodWordsHolder.transform.childCount];
        for (int i = 0; i < GoodWordsHolder.transform.childCount; i++)
        {
            GoodWords[i] = GoodWordsHolder.transform.GetChild(i).gameObject;
        }
        foreach (GameObject emoji in Emojis)
        {
            emoji.SetActive(false);
        }
        foreach (GameObject goodWord in GoodWords)
        {
            goodWord.SetActive(false);
        }
        if (isMaxLevelReached != 1)
        {
            levelText.text = "Level - " + sceneIndex;
            Debug.Log("Haaha");
        }
        else
            levelText.text = "Level - " + sceneIndexafterMaxLevelReached;

    }
    public void OnPause()
    {
        if (!isPaused)
        {
            AudioManager.instance.Click();
            pauseText.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            AudioManager.instance.Click();
            pauseText.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
    public void OnRestart()
    {
        if (!isPaused)
        {
            restarted = true;
            AudioManager.instance.Click();
           // StartCoroutine(ShowInterstitialAds());
            SceneManager.LoadScene(sceneIndex);
            if (AdManager.instance)
                AdManager.instance.ShowAd();
        }
    }
    public void LevelVictory()
    {
        //StartCoroutine(Victory(2f));
        Debug.Log("onlyonce");
        StartCoroutine(CompleteLevelPP(2f));
    }
    IEnumerator CompleteLevelPP(float time)
    {
        yield return new WaitForSeconds(time);
     
        StartCoroutine(Victory(2f));
    }
    public void Home()
    {
        Time.timeScale = 1;
        AudioManager.instance.Click();
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        AudioManager.instance.Click();
        if (isMaxLevelReached != 1)
        {
            if (sceneIndex == TotalLevels)
            {
                PlayerPrefs.SetInt("ismaxreached", 1);
                sceneIndexafterMaxLevelReached = TotalLevels;
                StartCoroutine(MaxLevelReached(20, 275, .5f));

                return;
            }
            PlayerPrefs.SetInt("Level", sceneIndex + 1);
            StartCoroutine(GoToNextLevel(.5f));
        }
        else
        {
            StartCoroutine(MaxLevelReached(20, 275, .5f));

        }
        if (AdManager.instance)
            AdManager.instance.ShowAd();
    }
    IEnumerator Victory(float time)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.VictoryPanel();
        LevelVictoryWindow.SetActive(true);
        int indexE = Random.Range(0, Emojis.Length);
        Emojis[indexE].SetActive(true);
        int indexG = Random.Range(0, GoodWords.Length);
        GoodWords[indexG].SetActive(true);

    }
    IEnumerator GoToNextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneIndex + 1);
    }
    IEnumerator MaxLevelReached(int min, int max, float time)
    {
        sceneIndexafterMaxLevelReached += 1;
        yield return new WaitForSeconds(time);
        PlayerPrefs.SetInt("aftermaxlevel", sceneIndexafterMaxLevelReached);
        PlayerPrefs.SetInt("Level", sceneIndexafterMaxLevelReached);
        SceneManager.LoadScene(Random.Range(min, max));

    }
    bool restarted;
   // IEnumerator ShowInterstitialAds()
   // {
        //MyPlugin.instance.ShowADSText.gameObject.SetActive(true);
        //for (int i = 3; i >= 0; i--)
        //{
        //    MyPlugin.instance.ShowADSCount.text = i.ToString();
        //    yield return new WaitForSeconds(0.5f);
        //    if (i <= 0)
        //    {
        //        MyPlugin.instance.ShowInterstitialAds("LevelFail");
        //        MyPlugin.instance.ShowADSCount.text = "";
        //        if (restarted)
        //        {
        //            restarted = false;
        //            SceneManager.LoadScene(sceneIndex);
        //            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //            MyPlugin.instance.ShowADSText.gameObject.SetActive(false);
        //        }
        //    }
        //}
  //  }
}
