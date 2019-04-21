using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private float jump = 10;
    private const int maxjump = 2;
    private int currentjump = 0;
    private bool grounded = true;
    private Rigidbody rb;
    private int count;

    public Button restartButton;
    public Button returnButton;
    public string SceneName;

    public Text counterText;
    public Text clearText;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCounterText();
        clearText.text = "";
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
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
            count = count + 1;
            SetCounterText();
        }
    }

    void SetCounterText()
    {
        counterText.text = "Materials: " + count.ToString();
        if(count >= 4)
        {
            clearText.text = "Stage Cleared!";
            restartButton.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(true);
        }
    }

    public void RetryGame()
    {
        SetCounterText();
        if (count >= 4)
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void ReturnGame()
    {
        SetCounterText();
        if (count >= 4)
        {
            SceneManager.LoadScene("Start Menu");
        }
           
    }
}
