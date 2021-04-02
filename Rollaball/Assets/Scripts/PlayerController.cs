using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;


    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;


    public float jumpforce = 0;

  
    public int maxjumps = 0;
    int jumpcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnJump(InputValue movementValue) 
    {
        if ((jumpcount < maxjumps))
        {
            jumpcount = jumpcount + 1;
            movementZ = jumpforce;
        }
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
       
        if (count >= 12)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);

        rb.AddForce(movement * speed);
        movementZ = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        jumpcount = 0;
    }

}