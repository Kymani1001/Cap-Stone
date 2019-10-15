using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Scale
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    //Movements
    public float speed;
    public float jump = 10;
    private const int maxjump = 2;
    private int currentjump = 0;
    private bool grounded = true;
    bool powerup = true;

    //Timer
    private float startTime;
    private bool Finished = false;
    
    //Physics
    private Rigidbody rb;

    public float gravity;
    public ForceMode forceMode;

    //Sounds
    private int count;
    public AudioSource Jump;
    public AudioSource Item;
    public AudioSource Victory;
    public AudioSource PowerUp;
    public AudioSource PowerDown;

    //Buttons
    public Button restartButton;
    public Button returnButton;
    public Button levelButton;
    public string SceneName;

    //Texts
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
        levelButton.gameObject.SetActive(false);

        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //scale
        //transform.localScale -= new Vector3(x, y, z);

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

        //Speed Up
        if (other.gameObject.CompareTag("PowerUp"))
        {
            speed = speed + 30;
            other.gameObject.SetActive(false);
            StartCoroutine(delay());
            PowerUp.Play();
            
        }
        IEnumerator delay()
        {
            yield return new WaitForSeconds(10);
            speed = speed - 30;
            other.gameObject.SetActive(true);
            PowerDown.Play();
        }
        
        //Jump Up
        if (other.gameObject.CompareTag("JumpUp"))
        {
            jump = jump + 20;
            other.gameObject.SetActive(false);
            StartCoroutine(wait());
            PowerUp.Play();

        }
        IEnumerator wait()
        {
            yield return new WaitForSeconds(10);
            jump = jump - 20;
            other.gameObject.SetActive(true);
            PowerDown.Play();
        }

        //Bigger
        if (other.gameObject.CompareTag("Grow"))
        {
            transform.localScale += new Vector3(x + 3f, y + 3f, z + 3f);
            //x = x + 5f;
            //y = y + 5f;
            //z = z + 5f;
            other.gameObject.SetActive(false);
            StartCoroutine(hold());
            PowerUp.Play();

        }
        IEnumerator hold()
        {
            yield return new WaitForSeconds(10);
            transform.localScale = new Vector3(x - 0f, y - 0f, z - 0f);
            //x = x - 5f;
            //y = y - 5f;
            //z = z - 5f;
            other.gameObject.SetActive(true);
            PowerDown.Play();
        }

        //Smaller
        if (other.gameObject.CompareTag("Shrink"))
        {
            transform.localScale += new Vector3(x + -1.5f, y + -1.5f, z + -1.5f);
            //x = x + 5f;
            //y = y + 5f;
            //z = z + 5f;
            other.gameObject.SetActive(false);
            StartCoroutine(stop());
            PowerUp.Play();

        }
        IEnumerator stop()
        {
            yield return new WaitForSeconds(10);
            transform.localScale = new Vector3(x - 0f, y - 0f, z - 0f);
            //x = x - 5f;
            //y = y - 5f;
            //z = z - 5f;
            other.gameObject.SetActive(true);
            PowerDown.Play();
        }

        //Time Stop
        /*if (other.gameObject.CompareTag("Time"))
        {
            if (other.gameObject.CompareTag("Time"))
                return;

            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;

            other.gameObject.SetActive(false);
            StartCoroutine(pause());
            PowerUp.Play();

        }
        IEnumerator pause()
        {
            if (other.gameObject.CompareTag("Time"))
                yield return new WaitForSeconds(5); 

            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
            other.gameObject.SetActive(true);
            PowerDown.Play();
        }*/
    }
    

    //Buttons
    void SetCounterText()
    {
        counterText.text = "Materials: " + count.ToString();
        if(count >= 10)
        {
            clearText.text = "Stage Cleared!";
            restartButton.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(true);
            levelButton.gameObject.SetActive(true);
            timerText.color = Color.magenta;
            Finished = true;
            Victory.Play();
            
        }
    }

    public void RetryGame()
    {
        SetCounterText();
        if (count >= 10)
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void ReturnGame()
    {
        SetCounterText();
        if (count >= 10)
        {
            SceneManager.LoadScene("Start Menu");
        }
           
    }

    public void LevelSelect()
    {
        SetCounterText();
        if (count >= 10)
        {
            SceneManager.LoadScene("Level Select");
        }

    }
}