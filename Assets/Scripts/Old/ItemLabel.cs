using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLabel : MonoBehaviour
{
    public ItemTrigger triggerItem;
    public Animator animator;
    public GameObject panel;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isVisible = animator.GetBool("isVisible");

                animator.SetBool("isVisible", !isVisible);

            }
        }
    }
}
