using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    private static readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    
    /// PUBLIC
    
    [Space(10)]
    public GameObject grabbedObject;

    public bool objectNabbed;
    
    [Space(10)]
    [SerializeField, Range(10, 30)] 
    public float targetOutlineWidth;
    public float currentOutlineWidth;
    
    
    [Space(10)]
    [SerializeField, Range(0, 10)] 
    public float frequency;
    [SerializeField, Range(0, 10)] 
    public float amplitude;
    
    /// PRIVATE
    
    private int _hitboxCount;
    
    private float _lerp;
    private float _time;
    private float _sine;
    
    public bool objectWithinRange;
    private bool _lerping;
    private bool _timeRunning;
   
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable") && _hitboxCount >= 0)
        {
            _hitboxCount++;
            if (_hitboxCount == 1)
            {
                _timeRunning = true;
                _lerp = currentOutlineWidth;
                grabbedObject = other.gameObject;
                
                _timeRunning = true;
                objectWithinRange = true;
                
                StartCoroutine(LerpIn());
                //_startingThickness = _currentInteractable.GetComponent<Renderer>().material.GetFloat(OutlineThickness);
                                     
            }
        }
    }
    
    IEnumerator LerpIn()
    {
        _time = 0;
        //Debug.Log("LERPING IN");
        while (_lerp < targetOutlineWidth)
        {
            if (!objectWithinRange)
            {
                yield break;
            }
            _lerping = true;
            //Debug.Log(time);
            _lerp = Mathf.Lerp(0, targetOutlineWidth, _time * frequency);
            grabbedObject.GetComponent<Renderer>().material.SetFloat(OutlineThickness, _lerp);
           
            yield return null;
        }

        _time = 0;
        _lerping = false;
        //Debug.Log("DONE");
    }

    void Update()
    {
        if (_timeRunning)
        {
            _time += Time.deltaTime;
            currentOutlineWidth = grabbedObject.GetComponent<Renderer>().material.GetFloat(OutlineThickness);
        }
        if (objectWithinRange)
        {
            if (!_lerping)
            {
                _sine = targetOutlineWidth + (Mathf.Sin(_time * frequency) * amplitude);
                //Debug.Log( _currentInteractable.GetComponent<Renderer>().material.GetFloat(OutlineThickness));
                grabbedObject.GetComponent<Renderer>().material.SetFloat(OutlineThickness, _sine);
                
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_hitboxCount > 0)
        {
            _hitboxCount--;
            if (_hitboxCount == 0)
            { 
                objectWithinRange = false;
                StartCoroutine(LerpOut());
            }
        }
    }
    
    IEnumerator LerpOut()
    {
        //Debug.Log("LERPING OUT");
        _time = 0;
        while (currentOutlineWidth >= 0)
        {
            if (objectWithinRange)
            {
                yield break;
            }
            _lerping = true;
            //Debug.Log(time);
            _lerp = Mathf.Lerp(currentOutlineWidth, -.1F, _time * frequency);
            grabbedObject.GetComponent<Renderer>().material.SetFloat(OutlineThickness, _lerp);
           
            yield return null;
        }
        if (objectWithinRange)
        {
            yield break;
        }
        grabbedObject.GetComponent<Renderer>().material.SetFloat(OutlineThickness, -.1F);
        _lerping = false;
        _timeRunning = false;
        grabbedObject = null;
        //Debug.Log("DONE");
    }
}
