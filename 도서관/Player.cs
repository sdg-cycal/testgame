using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpPower;
    public int coinCount;
    public GameManagerLogic manager;
    public float ForceSpeed;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")  && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0 ), ForceMode.Impulse);
        }
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ruller")
            rigid.AddForce(new Vector3(0, 0, ForceSpeed),ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            coinCount++;
            audio.Play();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Finish")
        {
            if(coinCount == manager.TotalCoinCount)
            {
                //Game Clear!
                SceneManager.LoadScene(manager.Stage + 1);
            }
            else
            {
                /*Restart.. 
                SceneManager.LoadScene("New Scene"+manager.stage).ToString());
                */
                SceneManager.LoadScene(manager.Stage);
            }
        }
        else if(other.tag == "Deathzoon")
        {
            SceneManager.LoadScene(manager.Stage);
        }
    }
}
