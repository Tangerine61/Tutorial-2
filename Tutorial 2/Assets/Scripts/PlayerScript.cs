using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jump;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    private int scoreValue;
    private int livesValue;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        WinTextObject.SetActive(false);
        SetCountText();
        LoseTextObject.SetActive(false);
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 9)
        {
            WinTextObject.SetActive(true);
            Destroy(gameObject);

            SoundManagerScript.PlaySound("Win sound");
        }
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            livesValue = 3;
            transform.position = new Vector2(0f, 27.98f);
        }
        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            }
        }
    }
}