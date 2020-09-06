using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    public Sprite neutral;
    public Sprite kissy;
    public Sprite love;
    public Sprite rage;
    public Sprite rage2;
    public Sprite shoot;
    public Sprite shot;
    public Sprite victory;

    SpriteRenderer sprite;
    float timer;
    public float countdownSec = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = neutral;
        countdownSec = countdownSec * 60;

    }

    private void Update()
    {
    if (timer > 0)
        {
            timer -= 1;
        } else { sprite.sprite = neutral; }

    // temp
    if (Input.anyKeyDown) {
         //   ChangeFace("rage");
        }
    }


    public Sprite PickSprite(string input) {

        switch (input)
        {
            case "kissy":
                return kissy;
            case "love":
                return love;
            case "rage":
                return rage;
            case "rage2":
                return rage2;
            case "shoot":
                return shoot;
            case "shot":
                return shot;
            case "victory":
                return victory;
            case "neutral":
                return neutral;
            case null:
                return neutral;
            default: Debug.Log("no face found"); return neutral;

        }


    }

    // Update is called once per frame
    public void ChangeFace(string input)
    {


        // input = PickSprite(input);



        sprite.sprite = PickSprite(input);
        timer = countdownSec;
    }
}
