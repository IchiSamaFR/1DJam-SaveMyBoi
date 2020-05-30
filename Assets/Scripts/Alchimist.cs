using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchimist : MonoBehaviour
{
    GameMaster gameMaster;
    bool CanTalk;

    int step = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameMaster.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMaster.needRewind)
        {
            step = 0;
        }

        if (Input.GetKeyDown("e") && CanTalk)
        {
            if (gameMaster.graveyard == true && step == 1)
            {
                step++;
            }
            if (step == 0)
            {
                Dialog.instance.Show("I don't rember the graveyard altar.");
                step++;
            }
            else if (step == 1)
            {
                Dialog.instance.Show("Could you help me ?");
            }
            else if (step == 2)
            {
                Dialog.instance.Show("Thank's ! Take this litle coin !");
                GameMaster.instance.golds += 1;
                step++;
            }
            else
            {
                Dialog.instance.Show("I can't pay you more than it.");
            }
        }
    }


    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CanTalk = true;
            gameMaster.showInteract = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CanTalk = false;
            gameMaster.showInteract = false;
            Dialog.instance.StopShow();
        }
    }
}
