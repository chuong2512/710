using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Video;

public class VideoXML : MonoBehaviour
{
    public RawImage rawImage;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    public string link,linkTo;
    public int i;
    // Start is called before the first frame update
    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        if (AdManager.instance && AdManager.instance.IconLinkList.Count>0)
        {
            i = Random.Range(0, AdManager.instance.Iconlist.Count - 1);
            link = AdManager.instance.IconLinkList[i];
            linkTo = AdManager.instance.IconToList[i];
            StartCoroutine(playVideo());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        Application.OpenURL(linkTo);
    }


    IEnumerator playVideo()
    {

        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
       
        // Vide clip from Url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = link;
        //Application.persistentDataPath + fileName;


        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);


        //Set video To Play then prepare Audio to prevent Buffering
    
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        rawImage.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();


        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

}
