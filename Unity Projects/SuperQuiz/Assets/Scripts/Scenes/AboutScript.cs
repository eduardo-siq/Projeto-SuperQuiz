using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AboutScript : MonoBehaviour
{

    // UI
    public RectTransform aboutRect;
    public bool endScene;
    private bool quit;

    void Start()
    {
        StartCoroutine(StartScene());
    }

    void Update()
    {
        // if (endScene){
        // aboutRect.anchoredPosition = new Vector2 (aboutRect.anchoredPosition.x, aboutRect.anchoredPosition.y - Time.deltaTime * 1200);
        // return;
        // }
        if (quit)
        {
            SessionScript.songAudio.volume = SessionScript.songAudio.volume - (Time.deltaTime * 2);
        }
    }

    IEnumerator StartScene()
    {
        yield return null;
        aboutRect = GameObject.Find("Canvas/Scroll View/Viewport/About").GetComponent<RectTransform>();
    }

    public void Noise()
    {
        SessionScript.ButtonAudio(SessionScript.negative);
    }

    public void SelectMenu(){
        SessionScript.ButtonAudio(SessionScript.neutral);
        Invoke("EndScene", 1.2f);
        Invoke("NextScene", 1.2f);
        // TransitionScript.PlayAnimation();
        // TransitionScript.StartAnimation();
        TransitionScript.EndAnimation();
    }

    public void NextScene()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

    void EndScene(){	// OBSOLETE?
        endScene = true;
    }
}

