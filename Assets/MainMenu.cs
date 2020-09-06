using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float timer;
    public float countDownTimer;
    public Text countDownTimerText;
    float timerText;
    public GameObject menuPanel;
    public GameObject creditPanel;


    // Start is called before the first frame update
    void Start()
    {
        timer = countDownTimer;
        menuPanel.SetActive(true);
    }

    // Update is called once per frame


        public void ActivatePanel( string panel)
    {
        if (panel == "menu")
        {
            menuPanel.SetActive(true);
            creditPanel.SetActive(false);
        } else
        {
            creditPanel.SetActive(true);
            menuPanel.SetActive(false);
        }

    }


    void Update()
    {
        //if (timer > 0)
        //{
        //    timer = timer - Time.deltaTime;
        //     timerText = timer;
        //    countDownTimerText.text = Mathf.RoundToInt(timerText).ToString();

        //} else
        //{
        //    LoadB(1);

        //}
    }


    public void LoadB(string sceneANumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneANumber);
        SceneManager.LoadScene(sceneANumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
