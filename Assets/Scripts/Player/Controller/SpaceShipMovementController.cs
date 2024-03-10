using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controller
{
    /// <summary>
    /// The space ship movement controller.
    /// </summary>
    public class SpaceShipMovementController : MonoBehaviour
    {
        [SerializeField]
        private float playerRightBorder;
    
        [SerializeField] 
        private float playerLeftBorder;
        
        [SerializeField]
        private float playerTopBorder;
    
        [SerializeField] 
        private float playerBottomBorder;
    
        //Sprites
        private GameObject ship;
        private GameObject shipRight;
        private GameObject shipLeft;

        //new input system
        private Vector2 moveInputValue = Vector2.zero;

        void Start()
        {
            shipLeft = GameObject.FindGameObjectsWithTag("Left").First();
            shipRight = GameObject.FindGameObjectsWithTag("Right").First();
            ship = GameObject.FindGameObjectsWithTag("Unmoved").First();
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                CalculateNewPosition();
                ShowRelevantShipSprite(this.moveInputValue.x);
            }
        }

        private void ShowRelevantShipSprite(float xMovement)
        {
            if (xMovement > 0)
            {
                this.shipRight.SetActive(true);
                this.shipLeft.SetActive(false);
                this.ship.SetActive(false);
            }
            else if (xMovement < 0)
            {
                this.shipLeft.SetActive(true);
                this.shipRight.SetActive(false);
                this.ship.SetActive(false);
            }
            else if (xMovement == 0)
            {
                this.ship.SetActive(true);
                this.shipLeft.SetActive(false);
                this.shipRight.SetActive(false);
            }      
        }

        private void CalculateNewPosition()
        {
            transform.position = new Vector3(
                CalculateNewXPosition(),
                CalculateNewYPosition(),
                transform.position.z);
        }

        private float CalculateNewXPosition()
        {       
            return Math.Min(this.playerRightBorder, Math.Max(this.playerLeftBorder, transform.position.x + this.moveInputValue.x * GameManager.Instance.ShipHorizontalSpeed * Time.deltaTime));
        }
        
        private float CalculateNewYPosition()
        {       
            return Math.Min(this.playerTopBorder, Math.Max(this.playerBottomBorder, transform.position.y + this.moveInputValue.y * GameManager.Instance.ShipVerticalSpeed * Time.deltaTime));
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                this.moveInputValue = context.ReadValue<Vector2>();   
            }

            if (context.canceled)
            {
                this.moveInputValue = Vector2.zero;
            }
        }
    }
}
