using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoListener : MonoBehaviour {
    [SerializeField]
    VideoPlayer videoPlayer;
    public static bool introShowed = false;
    // Use this for initialization
    void Start()
    {
        if (!introShowed)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
        }
        else
        {
            Application.LoadLevel("GameScene");
        }
    }
    private void EndReached(VideoPlayer source)
    {
        introShowed = true;
        Application.LoadLevel("GameScene");
    }

}
