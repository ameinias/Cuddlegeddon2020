using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Learn : MonoBehaviour
{

    public GameObject currentPanel;
    public GameObject firstPanel;

    public void Start()
    {
        firstPanel.SetActive(true);
        currentPanel = firstPanel;

    }

    private void Update()
    {
        if(Input.GetButtonDown("P1ProjUp") || Input.GetButtonDown("P2ProjUp"))
        { Debug.Log("moving");

        }
    }

    public void nextPanel(GameObject panel)
    {
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
    }

    public void LoadB(string sceneANumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneANumber);
        SceneManager.LoadScene(sceneANumber);
    }
}
