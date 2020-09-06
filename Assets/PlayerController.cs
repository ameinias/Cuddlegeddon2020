using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject face;
    public ChooseProjectile chamber;
    [Space(10)] // 10 pixels of spacing here.
    public float handSpeed = 10;
    public float bulletSpeed = 40f;
    public float offset = 0.9f;
    [Space(10)] // 10 pixels of spacing here.
    public string upKey;
    public string downKey;
    public string fireKey;
    public string projUpKey;
    public string projDownKey;
    Rigidbody rb;
    public GameObject projectile;
    [Space(10)] // 10 pixels of spacing here.
    public float coolDown;
    public bool canFire;
    public float coolDownTimer = 10f;
    [Space(10)] // 10 pixels of spacing here.
    public bool isP2;
    float projDir = -1;
    [Space(10)] // 10 pixels of spacing here.
    AudioSource audio;
    public AudioClip hitHugSFX;
    public AudioClip hitAskSFX;
    public AudioClip hitColdSFX;
    public AudioClip shootSFX;
    public Sprite handBlast;
    Sprite origSprite;
    SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        coolDownTimer = coolDown;
        origSprite = sprite.sprite;

        if (isP2) {

            projDir = -1;
        } else { projDir = 1; }
    }


    void CanFire()
    {

        coolDownTimer -= Time.deltaTime;
      
        
        if (coolDownTimer <= 0)
        {
            coolDownTimer = coolDown;
            canFire = true;
            sprite.sprite = origSprite;
        }
    }
    // Update is called once per frame
    void Update()
    {


        if (!GameManager.IsPaused())
        {
            //Move Hand
            if (Input.GetButton(upKey)) { rb.velocity = new Vector3(0, handSpeed, 0); }
            else if (Input.GetButton(downKey)) { rb.velocity = new Vector3(0, -handSpeed, 0); }
            else { rb.velocity = new Vector3(0, 0, 0); }

            //Fire
            CanFire();


            if (Input.GetButtonDown(fireKey) && canFire)
            {
                FireProjectile();
            }

            // Switch Projectile
            else if (Input.GetButtonDown(projUpKey))
            { chamber.LastProjectile(); }
            else if (Input.GetButtonDown(projDownKey))
            { chamber.NextProjectile(); }
        }
    }

    public void FireProjectile() {
        sprite.sprite = handBlast;
        Debug.Log("Fire");
        canFire = false;
   
        GameObject bullet = Instantiate(chamber.preCurrent, new Vector3(this.transform.position.x + offset * projDir
            , this.transform.position.y+0.5f, this.transform.position.z),Quaternion.identity);

        float speed = bullet.GetComponent<Projectile>().speed;
        if (isP2)
        {
            bullet.GetComponent<SpriteRenderer>().flipX = true; 
        }

        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(speed * projDir, 0, 0));

       // audio.PlayOneShot(shootSFX);

        
}

    void GetHit(string projectile)
    {
        if (projectile == "cold") {
            audio.PlayOneShot(hitColdSFX);
        }
        else if (projectile == "hug")
        {
            audio.PlayOneShot(hitHugSFX);
        }
        else if (projectile == "ask")
        {
            audio.PlayOneShot(hitAskSFX);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      //  GetHit(collision.gameObject.name);
    }
}
