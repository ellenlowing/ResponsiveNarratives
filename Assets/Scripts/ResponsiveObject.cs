using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveObject : MonoBehaviour
{
    // Morpher Slider Value
    // 0 : cube
    // 1 : plane
    [SerializeField] private Morpher _morpher;    
    [SerializeField] private float _morphSpeed;
    [SerializeField] private Transform _snapLocationTransform;
    private bool _isSnapSurfaceHorizontal = true;
 

    void Start()
    {
        // Responsive Object always starts as a cube
        _morpher.SetSlider(0f);
        _isSnapSurfaceHorizontal = true;
    }

    void Update()
    {
        
    }

    public void HandleUnselect()
    {
        // when unselect, object snaps into position on wall
        // look at surface hit normal and check if it's horizontal or vertical
        // if it's different from current form, start coroutine handleMorphTransition

        // If snap location surface is horizontal 
        // Caveat: surface is not always 100% up, so dot product is not always completely 0 or 1
        bool isFutureSnapSurfaceHorizontal = Mathf.Abs(Vector3.Dot(_snapLocationTransform.forward, Vector3.up)) >= 0.98;

        // If future snap location is different from current
        if(isFutureSnapSurfaceHorizontal != _isSnapSurfaceHorizontal)
        {
            if(isFutureSnapSurfaceHorizontal)
            {
                StartCoroutine(handleMorphTransition(1f, 0f));
            }
            else
            {
                StartCoroutine(handleMorphTransition(0f, 1f));
            }
        }
    }

    IEnumerator handleMorphTransition(float from, float to)
    {
        float t = 0;
        if(to == 0f)
        {
            Debug.Log("transitioning to cube for horizontal surface");
            _isSnapSurfaceHorizontal = true;
        }
        else
        {
            Debug.Log("transitioning to plane for vertical surface");
            _isSnapSurfaceHorizontal = false;
        }
        while(t < _morphSpeed)
        {
            _morpher.SetSlider(Mathf.Lerp(from, to, t));
            t += Time.deltaTime;
            yield return null;
        }

    }
}
