using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SingletonExample : MonoBehaviour
{

    private static SingletonExample singleton;
    public Text exampleText;

    public GameObject pausedBG;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ExampleFunction()
    {
        singleton.exampleText.text = "Score: ";
    }

    public static void Pause(bool value)
    {
        singleton.paused = value;
        singleton.pausedBG.SetActive(value);

        if (value) { Time.timeScale = 0; } else { Time.timeScale = 1; }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }


}
