using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour {

    public Vector2 moveValue;
    public float speed;
    private int count;
    private float stime;
    private float time;
    private bool gameEnd;
    private bool btnPressed;
    private int numPickups = 4;
    string cs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timeText;

    private void Start() {
        count = 0;
        stime = Time.time;
        winText.text = "";
        SetCountText();
        gameEnd = false;
        btnPressed = false;
        cs = SceneManager.GetActiveScene().name;

    }

    private void Update() {
        if (Input.anyKey && !btnPressed)
        {
            btnPressed = true;
            stime = Time.time;
        }
        if (!gameEnd || Input.anyKeyDown) {
            time = Time.time - stime;
            timeText.text = "Time: " + time.ToString("F2");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(cs);
        }

    }

    void OnMove(InputValue value) {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText() {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups) {
            scoreText.text = "";
            gameEnd = true;
            winText.text = "You win!";
        }
    }
}
