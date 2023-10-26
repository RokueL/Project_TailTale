using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager Instance;

    public Button StartButton;
    public Button EndButton;

    float time;
    float loadingTime = 2f;

    IEnumerator LoadAsynSceneCoroutine(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        switch (sceneName)
        {
            case "Level_1_1":
                while (!operation.isDone)
                {
                    time += Time.deltaTime;

                    if (operation.progress >= 0.9f && time >= loadingTime)
                    {
                        operation.allowSceneActivation = true;
                    }
                    yield return null;
                }
                break;
            case "Level_1_2":

                while (!operation.isDone)
                {
                    time += Time.deltaTime;

                    if (operation.progress >= 0.9f && time >= loadingTime)
                    {
                        operation.allowSceneActivation = true;
                    }
                    yield return null;
                }
                break;
        }
        time = 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    #region BUTTON_SETUP
    public void GameButtonSetup(Button button)
    {
        button.onClick.AddListener(LoadGameScene);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadAsynSceneCoroutine("Level_1_1"));
    }

    public void EndGame()
    {
        Debug.Log("Stop");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

#endregion

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level_Start")
        {
            Debug.Log("Setup");
            //StartButton = GameObject.Find("StartButton").GetComponent<Button>();
            //EndButton = GameObject.Find("EndButton").GetComponent<Button>();
            //StartButton.onClick.AddListener(LoadGameScene);
            //EndButton.onClick.AddListener(EndGame);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}