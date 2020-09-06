using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;
public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    Rigidbody rb;
    public float bounce = 100;
    public int count;
    public string reaction = "shot";
    AudioSource audio;
    public AudioClip hitSFX;
    public AudioClip bounceSFX;
    Animator anim;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)  // You hit a hand
    {
        count++;
        if (count > 3) {
            KillProj();
        }
        if (collision.gameObject.tag == "Projectile")
        {
            // Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Projectile>().KillProj();
            KillProj();
        }


        else if (collision.gameObject.name == "hand_1")
        {
             rb.velocity = Vector3.zero;
            audio.PlayOneShot(bounceSFX);
            Vector3 v = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, v);
            GetComponent<SpriteRenderer>().flipX = false;
            rb.AddForce(collision.contacts[0].normal * bounce, ForceMode.Impulse);



        }
        else if (collision.gameObject.name == "hand_2")
        {
            Vector3 v = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            rb.velocity = Vector3.zero;
            audio.PlayOneShot(bounceSFX);
            GetComponent<SpriteRenderer>().flipX = true;
            rb.AddForce(collision.contacts[0].normal * bounce, ForceMode.Impulse);
        }
        else  
        {
           
            Vector3 v = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            rb.velocity = Vector3.zero;
            audio.PlayOneShot(bounceSFX);
            GetComponent<SpriteRenderer>().flipX = true;
            rb.AddForce(collision.contacts[0].normal * bounce, ForceMode.Impulse);
        }
    }


    private void OnTriggerEnter(Collider other) // You hit a body
    {
      
        // Shaking the camera.
        CameraShaker.Presets.ShortShake2D();
        

        audio.PlayOneShot(hitSFX);
        if (other.gameObject.name == "Body2")
        {
            Debug.Log("hit something else");
            GameManager.UpdateScore("P2", damage);
            GameManager.Express(reaction, "P2");
            KillProj();

        }
        else if (other.gameObject.name == "Body1")
        {
            Debug.Log("hit something else");
            GameManager.UpdateScore("P1", damage);
            KillProj();
            GameManager.Express(reaction, "P1");
           
        } else
        {

            Debug.Log("HIT SOMETHING MYSTERIOUS");


        }
    }

    void KillProj()
    {
        rb.velocity = Vector3.zero;
        audio.PlayOneShot(hitSFX);
        anim.SetTrigger("Die");
        
        // explode animation
    }
    public void Explode() {

        Destroy(this.gameObject);
    }
}
