using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSurf : MonoBehaviour
{

    public float rotateSpeed;
    public float jumpSpeed;
    public Text status;
    enum STATUS
    {
        BALANCE,
        JUMP,
        DEAD,
        FALL
    };
    public float jumpDuration;

    float jumpPeak;

    STATUS condition = STATUS.BALANCE;

    float baseHeight;


    // Random rnd = new Random();

    void Start()
    {
      status.text = "alive";
      baseHeight = transform.position.y;
        jumpPeak = jumpDuration;

    }

    // Update is called once per frame
    void Update()
    {

        switch (condition)
        {
            case STATUS.JUMP:
                Ascend();
                break;
            case STATUS.FALL:
                Descend();
                break;
            case STATUS.DEAD:
                Die();
                break;
            default:
                Balance();
                break;

        }
       


        

    }


    void Die()
    {
        if (jumpDuration > jumpPeak / 2)
        {

            Vector3 yeet = new Vector3(-1, 1, 0);
            transform.Translate(yeet * Time.deltaTime * jumpSpeed, Camera.main.transform);

        }
        else{
            Vector3 yote = new Vector3(-1,-1,0);
            transform.Translate(yote * Time.deltaTime * jumpSpeed, Camera.main.transform);
        }

        transform.Rotate(0, 0, 10 * jumpDuration, Space.World);

        jumpDuration--;

    }



    void Ascend()
    {
        if (jumpDuration > 0)
        {


            transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed, Camera.main.transform);

            if (jumpDuration < jumpPeak )
            {
                transform.Rotate(0, 0, 180 / jumpPeak, Space.World);
            }

            jumpDuration--;
        }
        else
        {
            condition = STATUS.FALL;
        }
    }

    void Descend()
    {

        if (jumpDuration < jumpPeak)
        {
            transform.Translate(Vector3.up * Time.deltaTime * -jumpSpeed, Camera.main.transform);
            jumpDuration++;

            if (jumpDuration > 0)
            {
                transform.Rotate(0, 0, 180 / jumpPeak, Space.World);
            }

        }

        else
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, baseHeight, transform.position.z), transform.rotation);
            condition = STATUS.BALANCE;
        }

    }

    void Balance()
    {
        transform.Rotate(0, 0, (-Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime) + (rotateSpeed / 2 * Time.deltaTime), Space.World);

        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            condition = STATUS.DEAD;
            status.text = "dead";
        }

        if (Input.GetKeyDown("space"))
        {
            condition = STATUS.JUMP;
        }

    }
}


