using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameMaster gameMaster;

    bool canJump = true;

    [Header("Movement")]
    public float impulseForce = 10;
    public float moveSpeed = 10;

    string forwardKey;
    string leftKey;
    string RightKey;
    string backKey;
    string upKey = "space";

    Vector3 toGo = new Vector3();

    Rewind rewind;



    [Header("Rotation")]
    Camera cam;
    public GameObject body;
    public float mouseSensitivity = 100;
    Vector3 toLook = new Vector3();


    void Start()
    {
        gameMaster = GameMaster.instance;


        rewind = this.GetComponent<Rewind>();
        cam = Camera.main;
        toGo = this.transform.position;
        toLook = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameMaster.step <= 2)
        {
            return;
        }


        CheckKey();

        if(rewind.Rewinding() == false)
        {
            Rotate();
            Movement();
        } else
        {
            toGo = this.transform.position;
            toLook = new Vector3(body.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0);
        }
    }
    void Movement()
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0;

        Vector3 right = cam.transform.right;
        right.y = 0;

        if (Input.GetKey(forwardKey))
        {
            this.transform.position += forward * Time.fixedDeltaTime * moveSpeed;
        }
        if (Input.GetKey(backKey))
        {
            this.transform.position += -forward * Time.fixedDeltaTime * (moveSpeed / 2);
        }
        if (Input.GetKey(RightKey))
        {
            this.transform.position += right * Time.fixedDeltaTime * (moveSpeed / 2);
        }
        if (Input.GetKey(leftKey))
        {
            this.transform.position += -right * Time.fixedDeltaTime * (moveSpeed / 2);
        }
        if (Input.GetKeyDown(upKey) && canJump)
        {
            canJump = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, impulseForce, 0), ForceMode.Impulse);
        }
        /*
        //  Set de la position y pour les mouvements de haut en bas.
        toGo.y = this.transform.position.y;

        //this.transform.position = Vector3.Lerp(this.transform.position, toGo, Time.fixedDeltaTime * 8);
        this.transform.position = toGo;
        */
    }
    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        toLook.y += mouseX;
        toLook.x -= mouseY;

        toLook.x = Mathf.Clamp(toLook.x, -20, 20);

        //  Rotation du personnage de gauche à droite
        Quaternion target = Quaternion.Euler(0, toLook.y, 0);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target, Time.fixedDeltaTime * 8);

        //  S'il regarde au dessus de 10, alors on bloque la rotation du personnage pour ne pas qu'il soit couché
        if (toLook.x < -20)
        {
            target = Quaternion.Euler(10, 0, 0);
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, target, Time.fixedDeltaTime * 8);

            target = Quaternion.Euler(body.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            body.transform.rotation = target;
        }
        //  S'il regarde en dessous de 20, alors on bloque la rotation du personnage pour ne pas qu'il soit couché
        else if (toLook.x > 20)
        {
            target = Quaternion.Euler(-20, 0, 0);
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, target, Time.deltaTime * 8);

            target = Quaternion.Euler(body.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            body.transform.rotation = target;
        }
        //  Sinon le laisser se baisser ou se monter
        else
        {
            target = Quaternion.Euler(-toLook.x, 0, 0);
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, target, Time.deltaTime * 8);

            target = Quaternion.Euler(body.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            body.transform.rotation = target;
        }
    }

    void CheckKey()
    {
        if (gameMaster.azerty)
        {
            forwardKey = "z";
            leftKey = "q";
            RightKey = "d";
            backKey = "s";
        }
        else
        {
            forwardKey = "w";
            leftKey = "a";
            RightKey = "d";
            backKey = "s";
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
}
