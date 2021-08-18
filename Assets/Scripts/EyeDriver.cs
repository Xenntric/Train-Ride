using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeDriver : MonoBehaviour
{
    
    [Header("GameObjects")]
    public GameObject eyeballs;
    
    public GameObject tracker;
    public GameObject rod;
    public GameObject startingTarget;

    public InteractableController IC;
    public GameObject targetGameObject;

    [Header("Vectors")]
    public Vector2 eyeballDimensions;
    [SerializeField] 
    public Vector2 offsetAdjustment;

    [Space(10), Header("Starting Eye Position")]
    public Vector3 startingLocation = new Vector3(1.5F, -0.46F, 0);

    [SerializeField]
    public float moveSpeed;

    private void OnEnable()
    { 
        eyeballDimensions.x = eyeballs.GetComponent<SpriteRenderer>().bounds.size.x;
        eyeballDimensions.y = eyeballs.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IC.grabbedObject != null)
        {
            rod.transform.position = Vector3.MoveTowards(rod.transform.position, IC.grabbedObject.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            rod.transform.position = Vector3.MoveTowards(rod.transform.position, startingTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        
        var  distance = Vector2.Distance(rod.transform.position, tracker.transform.position);
        
        if (!(distance > ((eyeballDimensions.x / 2) - offsetAdjustment.x))) return;
        Vector3 fromOriginToObject = rod.transform.position - tracker.transform.position; //~GreenPosition~ - *BlackCenter*
        fromOriginToObject *= ((eyeballDimensions.x / 2) - offsetAdjustment.x) / distance; //Multiply by radius //Divide by Distance
        rod.transform.position = tracker.transform.position + fromOriginToObject; //*BlackCenter* + all that Math

    }
    
}
