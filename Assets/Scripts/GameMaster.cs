using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject effect;
    public GameObject effectWon;
    public bool won = false;

    [Header("Bools")]
    public bool fruits;
    public bool graveyard;
    public int golds;

    public bool needRewind;

    public bool showInteract;


    public int step = 0;

    [Header("Obj")]
    public static GameMaster instance;
    public Text textCoins;

    public GameObject interactObj;

    public Image timeUI;

    public GameObject Menu;
    public Text keyBoardInfo;
    public bool azerty = true;

    // TIME
    public float gameTime = 120;
    float timeNow;
    bool pause;

    void Awake()
    {
        Menu.SetActive(false);
        timeNow = Time.time;
        instance = this;
        effect.SetActive(false);
        effectWon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(step <= 2 && !needRewind)
        {
            if (Dialog.instance.showing)
            {
                return;
            }
            if(step == 0)
            {
                Dialog.instance.Show("Your friend has been caught by red guards cause he's a thief.");
                step++;
            } else if (step == 1)
            {
                Dialog.instance.Show("Even it's a thief, you need to help your friend !");
                step++;
            }
            else if (step == 2)
            {
                if (azerty)
                {
                    Dialog.instance.Show("- Use AZERTY and SPACE to move (Press Escape to change) -");
                } else
                {
                    Dialog.instance.Show("- Use QWERTY and SPACE to move (Press Escape to change) -");
                }
                step++;
            }
            return;
        }



        textCoins.text = golds.ToString();

        if (!won)
        {
            timeNow += Time.deltaTime;
        } else
        {
            effectWon.SetActive(true);
        }
        if (Input.GetKeyDown("escape"))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;
                Menu.SetActive(true);
            } else
            {
                Continue();
            }
        }

        timeUI.fillAmount = timeNow / gameTime;

        if(timeNow > gameTime && !needRewind && !won)
        {
            effect.SetActive(true);

            if(timeNow > gameTime + 1f)
            {
                step = 0;
                needRewind = true;
                effect.SetActive(false);
            }
        }

        if (needRewind && timeNow > 0)
        {
            golds = 0;
            graveyard = false;
            fruits = false;
            Time.timeScale = 0;
            timeNow -= 0.2f;
            if(timeNow < 0)
            {
                timeNow = 0;
                needRewind = false;
                Time.timeScale = 1;
            }
        }


        if (showInteract)
        {
            interactObj.SetActive(true);
        } else
        {
            interactObj.SetActive(false);
        }
    }

    public bool isPause()
    {
        return pause;
    }

    public void ChangeKeyboard()
    {
        azerty = !azerty;
        if (azerty)
        {
            keyBoardInfo.text = "Keyboard (AZERTY)";
        }
        else
        {
            keyBoardInfo.text = "Keyboard (QWERTY)";
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
