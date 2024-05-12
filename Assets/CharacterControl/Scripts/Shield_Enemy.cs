using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Enemy : MonoBehaviour
{
    public Animator character;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Block()
    {
        character.SetTrigger("block");
    }
}
