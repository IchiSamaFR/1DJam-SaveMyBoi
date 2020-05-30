using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public bool done;
    public bool into;

    public List<GameObject> obj = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.needRewind)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].SetActive(true);
            }
            done = false;
        }

        if (!done && into)
        {
            GameMaster.instance.showInteract = true;
            Dialog.instance.Show("Take bananas ?");

            if (Input.GetKeyDown("e"))
            {
                done = true;
                GameMaster.instance.fruits = true;

                for (int i = 0; i < obj.Count; i++)
                {
                    obj[i].SetActive(false);
                }
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
