using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Transform target;
    public float speed = 1;
    bool isMoving = false;
    public Text pointsText;
    public Text seccondsText;
    private int points;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        rb = GetComponent<Rigidbody>();
        points = 0;
        StartCoroutine(waith());
    }
	
	void Update () {
        //Move the player
        HandleMovement();
	}

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Delay, 3 secconds to star the game
    IEnumerator waith()
    {
        seccondsText.text = "3";

        yield return new WaitForSeconds(1);
        seccondsText.text ="2";

        yield return new WaitForSeconds(1);
        seccondsText.text = "1";

        yield return new WaitForSeconds(1);
        seccondsText.text = " ";

        isMoving = true;
    }

    //Method to move the player
    void HandleMovement()
    {
        // Only if isMoving is true, the player can moves
        if (!isMoving)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        //If player arrive at target
        if(transform.position == target.position)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene("SceneGame");
        }

        if (distance > 0)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            float xMov = Input.GetAxisRaw("Horizontal");

            Vector3 movHor = transform.right * xMov; //(1, 0, 0)

            velocity = (movHor).normalized * speed;
        }
    }

    //Recolecta basura y colisiona con enemigos
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            points += 1;
            SetPointsText();
        }
        else if (other.gameObject.CompareTag("Enemie"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene("SceneGame");
        }
    }

    //Muestra el puntaje en UI
    void SetPointsText()
    {
        pointsText.text = "Basura " + points.ToString() + "/20";
        
    }
}
