using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{

    //public Slider loadingBar;
    public Image bar;


    public float fillBar;
    public float maxProgress;

    void Start()
    {
        maxProgress = 0.25f;
        fillBar = 0;

        // loadingBar = GameObject.Find("Canvas/LoadingBar").GetComponent<Slider>();
        bar = GameObject.Find("Canvas/Scroll View/Viewport/Load/LoadingBar2/Fill").GetComponent<Image>();
        Invoke("StartLoading", 0.5f);
    }

    void StartLoading()
    {
        StartCoroutine(LoadAsynchronously());
    }

    void Update()
    {
        fillBar = Mathf.Clamp01(fillBar + Time.deltaTime);
        // loadingBar.value = fillBar;
        bar.fillAmount = fillBar;
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("login");
        while (!operation.isDone)
        {
            if (operation.progress > 0.5f && fillBar < 0.5f)
            {
                fillBar = 0.5f;
            }
            if (operation.progress > 0.75f && fillBar < 0.75f)
            {
                fillBar = 0.75f;
            }
            if (operation.progress > 0.95f && fillBar < 0.95f)
            {
                fillBar = 0.995f;
            }
            // float progress = Mathf.Clamp01(operation.progress / 0.9f);  // otherwise, 'progress' would never be 1
            // maxProgress = Mathf.Clamp(maxProgress + Time.deltaTime * 5, 0f, progress);
            yield return null;
        }
    }


}
