using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour

{
    [SerializeField]
    Canvas itemText;
    Animator animator;


    void Start()
    {
        itemText.enabled = false;

       

    }

    

    void OnTriggerEnter2D (Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            ItemTextOn();
            
        }   
    }

    void OnTriggerStay2D(Collider2D player)
    {

        if (player.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Debug.Log("item picked up");
            Destroy(this.gameObject);

        }
       
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Debug.Log("exit");
            ItemTextOff();
            
        }

    }

    private void Indicator()
    {
        bool isIndicated = animator.GetBool("indicated");
        animator.GetBool(0);
        animator.SetBool("indicated", !isIndicated);
    }

    private void ItemTextOn()
    {
        itemText.enabled = true;
    }

    private void ItemTextOff()
    {
        itemText.enabled = false;
    }

}
