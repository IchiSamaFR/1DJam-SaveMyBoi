using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.gameObject.transform.position = new Vector3(0, 1, 0);
        }
    }
}
