using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSnapLocation : MonoBehaviour
{
    [SerializeField] private Transform _pinchTransform;
    [SerializeField] private OVRHand _rightHand;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateSnapLocation();
    }

    public void UpdateSnapLocation()
    {
        RaycastHit hit;
        int layerMask = 1 << 3;
        if(_rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if(Physics.Raycast(_pinchTransform.position, _pinchTransform.forward, out hit, Mathf.Infinity, layerMask ))
            {
                transform.position = hit.point;
                transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
            }
        }
    }


}
