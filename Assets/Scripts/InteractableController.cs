using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{

    public CapsuleCollider2D thisCollider;
    private static readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    private   Material m_Material;
    public float sine;
    [SerializeField] 
    public float amplitude;

    private GameObject _currentInteractable;
    private bool _currentlyFuckingWith;
    private float _startingThickness;

    // Start is called before the first frame update
    void OnEnable()
    {
        thisCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_currentlyFuckingWith)
        { 
            sine = (20 + (Mathf.Sin(Time.fixedTime) * amplitude));
            Debug.Log( _currentInteractable.GetComponent<Renderer>().material.GetFloat(OutlineThickness));
            _currentInteractable.GetComponent<Renderer>().material.SetFloat(OutlineThickness, sine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Interactable"))
        {
            _currentlyFuckingWith = true;
            _currentInteractable = other.gameObject;
            
            _startingThickness = _currentInteractable.GetComponent<Renderer>().material.GetFloat(OutlineThickness);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _currentlyFuckingWith = false;
    }
}
