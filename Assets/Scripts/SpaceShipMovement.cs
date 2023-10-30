using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private GameObject ship;
    private GameObject shipRight;
    private GameObject shipLeft;

    // Start is called before the first frame update
    void Start()
    {
        shipLeft = GameObject.FindGameObjectsWithTag("Left").First();
        shipRight = GameObject.FindGameObjectsWithTag("Right").First();
        ship = GameObject.FindGameObjectsWithTag("Unmoved").First();
    }

    // Update is called once per frame
    void Update()
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
}
