using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    GameMaster gameMaster;


    public List<Vector3> position = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();

    float timeBef;
    Rigidbody rb;

    bool isRewinding;

    void Start()
    {
        gameMaster = GameMaster.instance;
    }


    public bool Rewinding()
    {
        return isRewinding;
    }


    // Update is called once per frame
    void Update()
    {
        if(timeBef < Time.time && !gameMaster.needRewind)
        {
            Record();

            timeBef = Time.time + 0.4f;
        }
        
        if(gameMaster.needRewind)
        {
            if(position.Count > 0)
            {
                Rew();
                isRewinding = true;
            }
        } else
        {
            isRewinding = false;
        }
        timeBef -= Time.deltaTime;
    }

    void Rew()
    {
        this.gameObject.transform.position = position[0];
        this.gameObject.transform.rotation = rotation[0];
        position.RemoveAt(0);
        rotation.RemoveAt(0);
    }

    void Record()
    {
        position.Insert(0, this.transform.position);
        rotation.Insert(0, this.transform.rotation);
    }

}
