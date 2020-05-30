using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
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
            if(gameMaster.fruits == true && step == 1)
            {
                step++;
            }
            if (step == 0)
            {
                Dialog.instance.Show("I don't have products, if you can give me some, i could pay you.");
                step++;
            }
            else if (step == 1)
            {
                Dialog.instance.Show("Give me some products.");
            } else if (step == 2)
            {
                Dialog.instance.Show("Thank's ! Take this golds !");
                GameMaster.instance.golds += 9;
                step++;
            } else
            {
                Dialog.instance.Show("I already paid you and i will not give one more gold.");
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
