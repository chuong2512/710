using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;

public class VideoDownloadXML : MonoBehaviour
{
    public static VideoDownloadXML instance;
    public string link, linkTo;
    public bool downloaded, playOnce, startNow;
    public int randomNum;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        playOnce = true;
    }

    private void Start()
    {
        StartCoroutine(GeneratingRandomNumber());
    }

    IEnumerator GeneratingRandomNumber()
    {
        yield return new WaitForSeconds(1f);
        if (AdManager.instance)
        {
            if (AdManager.instance.IconLinkList.Count > 0)
            {
                randomNum = Random.Range(0, AdManager.instance.IconLinkList.Count);
                startNow = true;
            }
        }
    }

    void Update() {
        if (playOnce && !downloaded) {
             StartCoroutine(testVideoDownload());
        }
    }

    IEnumerator testVideoDownload()
    {
        if (startNow) {
            if (AdManager.instance)
            {
                if (link == "" && AdManager.instance.IconLinkList.Count > 0)
                {
                    link = AdManager.instance.IconLinkList[randomNum];
                    linkTo = AdManager.instance.IconToList[randomNum];
                }
            }
            
            if (!File.Exists(Application.persistentDataPath + "/videoFile.mp4") && link != "")
            {
                var www = new WWW(link);
                Debug.Log("Downloading!");
                yield return www;
                Debug.Log(www.error + ":" + www.bytes + ":" + www.bytesDownloaded);
                if (www.error == null && www.bytesDownloaded > 243)
                {
                    File.WriteAllBytes(Application.persistentDataPath + "/videoFile.mp4", www.bytes);
                    Debug.Log("File Saved!");
                }
            }
        }
        
    }
}
