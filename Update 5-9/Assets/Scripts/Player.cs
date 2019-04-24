using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump = 10;
    private const int maxjump = 2;
    private int currentjump = 0;
    private bool grounded = true;

    private float startTime;
    private bool Finished = false;
    
    private Rigidbody rb;

    public float gravity = 9.81f;
    public ForceMode forceMode;

    private int count;
    public AudioSource Jump;
    public AudioSource Item;
    public AudioSource Victory;

    public Button restartButton;
    public Button returnButton;
    public string SceneName;

    public Text counterText;
    public Text clearText;
    public Text timerText;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCounterText();
        clearText.text = "";
        restartButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);

        startTime = Time.time;
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
        rb.AddForce(Vector3.down * gravity, forceMode);
        if (Input.GetKeyDown("space") && (grounded || maxjump > currentjump))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            grounded = false;
            currentjump++;
            Jump.Play();
        }

        //timer
        if (Finished)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }


    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        currentjump = 0;
    }

    //Materials
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCounterText();
            Item.Play();
        }
    }

    void SetCounterText()
    {
        counterText.text = "Materials: " + count.ToString();
        if(count >= 10)
        {
            clearText.text = "Stage Cleared!";
            restartButton.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(true);
            timerText.color = Color.magenta;
            Finished = true;
            Victory.Play();
            
        }
    }

    public void RetryGame()
    {
        SetCounterText();
        if (count >= 5)
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void ReturnGame()
    {
        SetCounterText();
        if (count >= 5)
        {
            SceneManager.LoadScene("Start Menu");
        }
           
    }
}
