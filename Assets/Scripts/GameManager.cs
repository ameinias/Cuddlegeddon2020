using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool noTimer;
    private static GameManager singleton;


    [Space(10)] // 10 pixels of spacing here.

    public Text scoreP1;
    public Text scoreP2;
    public Text relDescText;
    public Text relNumText;
    public int relNum;
    [Space(10)] // 10 pixels of spacing here.
    public int player1Score = 0;
    public int player2Score = 0;


    public bool finishedVanilla = false;

    public bool paused;

    public string level;



    [Space(10)] // 10 pixels of spacing here.
    public HeadController P1Head;
    public HeadController P2Head;

    [Space(10)] // End Timer
    public float timer;
    public float toxicTimeWarn = 6;
    public float toxicTime = 5;
    public float happyTime = 10;


    [Space(10)] // Start Timer
    public float timerStart;
    public float timerStartLength = 5;



    public string relationshipResult;
    public string relationshipResultTxt;

    public SpriteRenderer background;
    [Header("Audio")]
    AudioSource audio;
    public AudioClip beepSFX;
    public AudioClip winSFX;
    public bool muted;
    public bool muteMusic;

    public Healthbar healthBar;

    [Space(10)] // 10 pixels of spacing here.

    public Text countDownTimer;
    public AudioSource bgMusic;

    [Header("Menus")]
    public GameObject pausedBG;
    public GameObject coDepScreen;
    public GameObject heckScreen;
    public GameObject blissScreen;
    public string nextLevel;

    public Healthbar P1HealthBar;
    public Healthbar P2HealthBar;

    [Header("Domestic Heck Mode")]
    public GameObject P1WinScreen;
    public GameObject P2WinScreen;

    void Start()
    {
        singleton = this;
        //RelationShipScore();
        
        timer = happyTime * 60;
        audio = GetComponent<AudioSource>();
        Pause(false);

        muteMusic = true;
        muted = false;
        if (!muteMusic) { bgMusic.Play(); }

        coDepScreen.SetActive(false);
        heckScreen.SetActive(false);
        blissScreen.SetActive(false);

   timerStart = timerStartLength;
        level = GetComponent<LevelTracker>().level;


        Debug.Log(player1Score + " score " + player2Score);
        if (level == "vanilla")
        {
            countDownTimer.gameObject.SetActive(false);
        } else if (level == "hell")
        {
            singleton.P1WinScreen.SetActive(false);
            singleton.P1WinScreen.SetActive(false);
        }

        else { //countDownTimer.gameObject.SetActive(true);
         
        }
    }

    void Update()
    {

        LevelTimerStart();


        if (!paused)
        {
            if (!noTimer && level == "vanilla")
                LevelTimerEnd();
        }

        if (finishedVanilla == true)
        {
            if (Input.anyKeyDown)
            { LoadB(nextLevel); }
        }


        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {

                Pause(true);
            }
            else
            {
                Pause(false);

            }

        }



    }


    public void Mute(bool value)
    {
        muted = !muted;


    }

    public void MuteMusic(bool value)
    {
        muteMusic = !muteMusic;


    }


    public static bool IsPaused()
    {
        bool value = singleton.paused;
        return value;
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


    public void Paused2(bool value)
    {
        Pause(value);

    }


    public static void Pause(bool value)
    {
        singleton.paused = value;
        singleton.pausedBG.SetActive(value);

        if (value)
        {
            Time.timeScale = 0;
            singleton.bgMusic.Pause();


        }
        else
        {
            Time.timeScale = 1;
            if (singleton.muteMusic == false)
            {
                singleton.bgMusic.Play();
            }
            else
            {
                singleton.bgMusic.Pause();
            }
        }
    }




    // Happiness Calculator

    public static void Express(string face, string player)
    {




        if (player == "P1")
        {
            singleton.P1Head.ChangeFace(face);

        }
        else if (player == "P2")
        { singleton.P2Head.ChangeFace(face); }
        else { Debug.Log("You are trying to elicite a reaction from a non-existant person."); }

    }

    public static void UpdateScore(string player, int damageValue)
    {

      Debug.Log("HIT " + player + " for " + damageValue + " now " + singleton.player1Score);
    singleton.NewRelScore(damageValue);

        if (player == "P1")
        {
            if (singleton.P1HealthBar.health > 0)
            {
                singleton.player1Score += damageValue;
                singleton.scoreP1.text = singleton.player1Score.ToString();
                singleton.P1HealthBar.GainHealth(damageValue);
                Debug.Log("Hit P1");
            } else
            {

                singleton.Defeated("P1");
            }

}
        else if (player == "P2")
        {
            if (singleton.P2HealthBar.health > 0)
            {
                singleton.player2Score += damageValue;
            singleton.scoreP2.text = singleton.player2Score.ToString();
            singleton.P2HealthBar.GainHealth(damageValue);

               Debug.Log("hitP2  THIS ONE is over updating");
            }
            else
            {
                singleton.Defeated("P2");


            }
        }
        else { Debug.Log(player + "does not exist, cannot damage"); }
    }


    public void Defeated(string player)
    {
        Time.timeScale = 0;
        singleton.Mute(true);
        audio.PlayOneShot(winSFX);


        if (player == "P1")

        { singleton.P1WinScreen.SetActive(true); }
        else
        { singleton.P1WinScreen.SetActive(true); }
    }


    public  void NewRelScore(int damage) {

        healthBar.GainHealth(damage);
       
        relDescText.text = PlayerSatisfaction(healthBar.health); 

    }

    string PlayerSatisfaction(float score)


    {

        if (score > healthBar.lowHealth && score < healthBar.highHealth)
        { return "fine"; }

        else if (score > healthBar.highHealth)
        { return "obsessed"; }

        else if (score < healthBar.lowHealth)
        {

            if (score < healthBar.minimumHealth)
            {
                return "tortured";
            }
            else if (score < healthBar.lowHealth)
            {
                return "miserable";
            }
            else
            { return "rocks"; }

        }
        else { return "rocks"; }





    }



    public void LevelTimerEnd()
    {
        if (timer > 0)
        {
            timer--;
            if (timer < toxicTimeWarn * 60)
            { background.color = Color.yellow;
                countDownTimer.gameObject.SetActive(true);

                float minutes = Mathf.FloorToInt(timer / 60);
                float seconds = Mathf.FloorToInt(timer % 60);
                countDownTimer.text = "00:0" + minutes.ToString(); //string.Format("{0:00}:{1:00}", minutes, seconds); 


                if (Mathf.Approximately(timer/60, Mathf.RoundToInt(timer/60)))
                {
                    audio.PlayOneShot(beepSFX); 
                   // Debug.Log(timer);
                }

            }



            if (timer < toxicTime * 60)
            {



                if (relationshipResult == "hell") { EndLevel(); } else if (relationshipResult == "fine") { background.color = Color.blue; }
            }
        }
        else
        {

            EndLevel();


        }


    }

    void LevelTimerStart()
    {



          if (timerStart > 0)
        {
            timerStart = timerStart - Time.deltaTime;
         

                float minutes = Mathf.FloorToInt(timer / 60);
    float seconds = Mathf.FloorToInt(timer % 60);
    countDownTimer.text = "00:0" + minutes.ToString(); //string.Format("{0:00}:{1:00}", minutes, seconds); 


                if (Mathf.Approximately(timerStart / 60, Mathf.RoundToInt(timerStart / 60)))
                {
                    audio.PlayOneShot(beepSFX); 
                   // Debug.Log(timer);
                }

            } else
        {
            countDownTimer.gameObject.SetActive(false);

        }

    }


public void LoadB(string sceneANumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneANumber);
        SceneManager.LoadScene(sceneANumber);
    }

    public void EndLevel()
    {

        


        //Pause(true);
         relDescText.text = relationshipResult;
        Time.timeScale = 0;
        if (PlayerSatisfaction(healthBar.health) == "fine")
        { background.color = Color.green;
            blissScreen.SetActive(true);
            nextLevel = "Bliss";


        }
        else if (PlayerSatisfaction(healthBar.health) == "obsessed")
        { background.color = Color.yellow;
            coDepScreen.SetActive(true);

            nextLevel = "Codep";

        }
    
        else { background.color = Color.red;
            heckScreen.SetActive(true);
            nextLevel = "DomesticHeck";

        }

        finishedVanilla = true;




    }



}