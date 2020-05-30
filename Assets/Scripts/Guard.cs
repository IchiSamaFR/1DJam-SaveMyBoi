using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
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

        if(!Dialog.instance.showing && GameMaster.instance.won)
        {
            Dialog.instance.Show("YOU WON ! Thank's for playing at this game.");
        }


        if (Input.GetKeyDown("e") && CanTalk)
        {
            


            if(step == 0)
            {
                Dialog.instance.Show("If you want to save him, give us 10 Gold.");
                step++;
            } else if (step == 1 && gameMaster.golds < 10)
            {
                Dialog.instance.Show("Need " + (10 - gameMaster.golds) + " more.");
            } else if (gameMaster.golds >= 10 && !GameMaster.instance.won)
            {
                Dialog.instance.Show("It's okay we let him safe.");
                GameMaster.instance.won = true;
                gameMaster.golds -= 10;
                step++;
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
