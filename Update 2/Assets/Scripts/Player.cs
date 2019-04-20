using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float jump = 10;
    private const int maxjump = 2;
    private int currentjump = 0;
    private bool grounded = true;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


        rb.AddForce(movement * speed);

        //jumping
        if (Input.GetKeyDown("space") && (grounded || maxjump > currentjump))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            grounded = false;
            currentjump++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        currentjump = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
