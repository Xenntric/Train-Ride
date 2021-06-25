
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Rigidbody2D myRigidBody;
    public PlayerMovement playerMovement;


    public bool inventoryActive;

    private void Awake()
    {

        if (inventoryUI != null)
        {
            inventoryActive = false;
        }
        inventoryUI.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

        playerMovement.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current[Key.E].isPressed) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            if (inventoryUI.activeInHierarchy)
            {
                inventoryActive = true;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                Time.timeScale = 0;
            }
            else
            {
                inventoryActive = false;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                Time.timeScale = 1;
            }

        }
    }
}