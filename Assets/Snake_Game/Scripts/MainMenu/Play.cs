using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void GoToLevels()
    {
        AudioMainMenu.instance.Click();
        StartCoroutine(GoTo(.5f));
    }
    IEnumerator GoTo(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
