using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Navigation class between the different scenes
public class Navigation : MonoBehaviour {

    public void OnMainMenuClicked()
    {
        Application.LoadLevel("MainMenu");

    }
    public void OnPlayWasClicked()
    {
        Application.LoadLevel("IntroScene");

    }
    public void OnIntroWasClicked()
    {
        Application.LoadLevel("IntroScene");
    }
    public void OnHowToPlayWasClicked()
    {
        Application.LoadLevel("HowToPlayScene");
    }
}