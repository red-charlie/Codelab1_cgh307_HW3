using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public KeyCode forwardInput;
    public KeyCode backwardInput;
    public KeyCode rightInput;
    public KeyCode leftInput;

    public float speed;
    static public bool canMove = true;
    public Text currentScore;

    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
   

    // Update is called once per frame
    void Update()
    {

        Vector3 ForwardDirection = transform.forward;  //the player's relative forward 
        Vector3 RightDirection = transform.right; //the players relative right 
        Vector3 CurrentVelocity = rb.velocity; //the current velocity

        if (canMove == true)
        {

            if (Input.GetKey(forwardInput))
            {
                //rb.velocity = new Vector3 (0,0,MovementSpeed); //altering these because they don't account for rotation
                rb.velocity = ForwardDirection * speed; //Move forward at the movement speed float
            }

            if (Input.GetKey(backwardInput))
            {
                // rb.velocity = new Vector3 (0,0,-MovementSpeed);
                rb.velocity = ForwardDirection * -speed;
            }

            if (Input.GetKey(leftInput))
            {
                // rb.velocity = new Vector3 (MovementSpeed,0,0);
                rb.velocity = RightDirection * -speed;

            }

            if (Input.GetKey(rightInput))
            {
                // rb.velocity = new Vector3 (-MovementSpeed,0,0);
                rb.velocity = RightDirection * speed;
            }

            else if (Input.GetKeyUp(leftInput) ||
                     Input.GetKeyUp(forwardInput) ||
                     Input.GetKeyUp(rightInput) ||
                     Input.GetKeyUp(backwardInput))
            {

                rb.velocity = new Vector3(0, 0, 0); // if no input from controls please stop


            }

        }
        else
        {
            rb.velocity = Vector3.zero; // if canmove = false don't move dummy
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("I have collided with something");

        if(other.gameObject.tag == "item")
        {
            GameManager.instance.Score++;

            Debug.Log("I have walked into an " + other.gameObject.name);
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            //update the text for the costs
            currentScore.text = "Grocery Costs: " + GameManager.instance.Score;
        }
    }
}



