using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    //Sprites
    private GameObject ship;
    private GameObject shipRight;
    private GameObject shipLeft;

    private Rigidbody2D Rigidbody;

    private bool isShipBoosted;    

    // Start is called before the first frame update
    void Start()
    {
        isShipBoosted = false;
        this.Rigidbody = GetComponent<Rigidbody2D>();

        shipLeft = GameObject.FindGameObjectsWithTag("Left").First();
        shipRight = GameObject.FindGameObjectsWithTag("Right").First();
        ship = GameObject.FindGameObjectsWithTag("Unmoved").First();
    }

    // Update is called once per frame
    void Update()
    {
        ShowRelevantShipSprite();
        MoveHorizontal();
        BoostShip();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {        
        var collisionObject = collision.gameObject;
        if (collisionObject.tag == "Border")
        {            
            this.Rigidbody.velocity = Vector2.zero;
            this.Rigidbody.isKinematic = true;
        }
    }

    private void ShowRelevantShipSprite()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            this.shipRight.SetActive(true);
            this.shipLeft.SetActive(false);
            this.ship.SetActive(false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            this.shipLeft.SetActive(true);
            this.shipRight.SetActive(false);
            this.ship.SetActive(false);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            this.ship.SetActive(true);
            this.shipLeft.SetActive(false);
            this.shipRight.SetActive(false);
        }
    }

    private void BoostShip()
    {
        if (this.Rigidbody.velocity.y == 0)
        {
            this.isShipBoosted = false;
        }

        if (!this.isShipBoosted & (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            this.Rigidbody.isKinematic = false;
            this.Rigidbody.AddForce(Vector2.up * GameManager.ShipBoosterVelocity, ForceMode2D.Impulse);
            this.isShipBoosted = true;
        }            
    }

    private void MoveHorizontal()
    {
        transform.position = new Vector3(
            CalculateNewXPosition(),
            transform.position.y,
            transform.position.z);
    }

    private float CalculateNewXPosition()
    {
        //return transform.position.x + Input.GetAxis("Horizontal") * this.speed * Time.deltaTime;
        return Math.Min(8.28f, Math.Max(-8.25f, transform.position.x + Input.GetAxis("Horizontal") * GameManager.ShipHorizontalSpeed * Time.deltaTime));
    }
}
