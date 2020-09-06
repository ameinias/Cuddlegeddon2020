using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    static int healthP1;
    static int healthP2;
        public bool isP1;
    public Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            Debug.Log("IceHit");
            if (isP1) {
                Manager.player1Score++;
                textScore.text = healthP1.ToString();
            } else {
                healthP2++;
                textScore.text = healthP2.ToString();
            }
            Destroy(other.gameObject);
        }
    }
}
