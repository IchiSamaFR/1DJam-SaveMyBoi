using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    public bool done;
    public bool into;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.needRewind)
        {
            done = false;
        }

        if (!done && into)
        {
            GameMaster.instance.showInteract = true;
            Dialog.instance.Show("Take a look at the altar ?");

            if (Input.GetKeyDown("e"))
            {
                done = true;
                GameMaster.instance.graveyard = true;

                GameMaster.instance.showInteract = false;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            into = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            into = false;
            GameMaster.instance.showInteract = false;
        }
    }
}
