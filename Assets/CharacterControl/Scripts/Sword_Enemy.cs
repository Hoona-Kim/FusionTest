using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Enemy : MonoBehaviour
{
    public int weaponDamage = 20;
    public Animator character;

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
        if(other.tag == "PlayerShield")
        {
            other.GetComponent<PlayerStatus>().Block();
        }
        else if(other.tag == "Player")
        {
            other.GetComponent<PlayerStatus>().TakeDamage(weaponDamage);
            if (other.GetComponent<PlayerStatus>().isDead)
            {
                character.SetTrigger("win");
            }
        }
    }
}
