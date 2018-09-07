using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadScriptLimited : MonoBehaviour
{

    public Slider loadingBar;
    public float limitYear;
    public float limitMonth;
    public float limitDay;
    public float minYear;
    public float minMonth;
    public float minDay;

    void Start()
    {
        bool lockGame = false;
        string currentDate = DateTime.Now.ToString("yyyy/MM/dd");
        string[] space = new string[] { "/" };
        string[] currentDateArray = currentDate.Split(space, StringSplitOptions.None);
        float currentYear = float.Parse(currentDateArray[0]);
        float currentMonth = float.Parse(currentDateArray[1]);
        float currentDay = float.Parse(currentDateArray[2]);

        if (currentYear > limitYear) lockGame = true;
        if (currentMonth > limitMonth) lockGame = true;
        if (currentDay > limitDay) lockGame = true;
        if (currentYear < minYear) lockGame = true;
        if (currentMonth < minMonth) lockGame = true;
        if (currentDay < minDay) lockGame = true;

        loadingBar = GameObject.Find("Canvas/LoadingBar").GetComponent<Slider>();

        if (lockGame)
        {
            StartCoroutine(LoadAsynchronouslyLocked());
        }
        if (!lockGame)
        {
            StartCoroutine(LoadAsynchronously());
        }
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("login");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);  // otherwise, 'progress' would never be 1
            loadingBar.value = progress;
            yield return null;
        }
    }

    IEnumerator LoadAsynchronouslyLocked()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("loginLocked");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);  // otherwise, 'progress' would never be 1
            loadingBar.value = progress;
            yield return null;
        }
    }
}