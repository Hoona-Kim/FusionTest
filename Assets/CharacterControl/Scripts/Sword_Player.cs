using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Player : MonoBehaviour
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
        if (other.tag == "EnemyShield")
        {
            other.GetComponent<Shield_Enemy>().Block();
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyStatus>().TakeDamage(weaponDamage);
            if (other.GetComponent<EnemyStatus>().isDead)
            {
                character.SetTrigger("win");
            }
        }
    }
}
