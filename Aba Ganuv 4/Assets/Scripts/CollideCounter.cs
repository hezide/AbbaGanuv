using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollideCounter : MonoBehaviour {
    int collideCounter;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Text countText;
    [SerializeField] AudioClip[] collsionSounds;
    GameObject failureCanvas;
    GameObject winCanvas;
    // Use this for initialization
    void Start () {
        collideCounter = 0;
        //find the inactive UI element of sucess or failure
        foreach (GameObject item in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (item.name.Equals("failureCanvas"))
            {
                failureCanvas = item;
            }
            else if (item.name.Equals("winCanvas"))
            {
                winCanvas = item;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.Equals(GameObject.Find("islands")))
        {
            //game won!!!!
            StartCoroutine(GameWon());
        }
        //else, we colided an obstacle
        else
        {
            //play random sound
            int soundIndex = UnityEngine.Random.Range(0, collsionSounds.Length);
            audioSource.clip = collsionSounds[soundIndex];
            audioSource.Play();

            collideCounter++;
            countText.text = "Hits: " + collideCounter.ToString();
            Destroy(collision.gameObject);

            if (collideCounter == 3)
            {
                StartCoroutine(GameOver());
            }
        }

    }

     IEnumerator GameWon()
    {
        yield return new WaitForSeconds(1);
        //pause all game sound
        AudioListener.pause = true;
        winCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        winCanvas.SetActive(false);

        AudioListener.pause = false;
        Application.LoadLevel("MainMenu");
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        //pause all game sound
        AudioListener.pause = true;
        failureCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        failureCanvas.SetActive(false);

        AudioListener.pause = false;
        Application.LoadLevel("MainMenu");
    }
}
