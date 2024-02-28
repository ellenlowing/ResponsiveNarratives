using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSnapLocation : MonoBehaviour
{
    [SerializeField] private Transform _pinchTransform;
    [SerializeField] private OVRHand _rightHand;
    private int _layerMask;

    void Start()
    {
        _layerMask = 1 << 3;
    }

    void Update()
    {
        RaycastHit hit;
        bool snapLocationFound = Physics.Raycast(_pinchTransform.position, _pinchTransform.forward, out hit, Mathf.Infinity, _layerMask );
        
        if(_rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && snapLocationFound)
        {
            UpdateSnapLocation(hit);
            GetSceneAnchorDimensions(hit);
        }
    }

    public void UpdateSnapLocation(RaycastHit hit)
    {
        transform.position = hit.point;
        transform.rotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
    }

    // note: this should probably be called on unselect for snap
    public void GetSceneAnchorDimensions(RaycastHit hit)
    {
        var collider = hit.collider;
        OVRSceneAnchor sceneAnchor = collider.GetComponentInParent<OVRSceneAnchor>();
        OVRSemanticClassification classification = collider.GetComponentInParent<OVRSemanticClassification>();

        var plane = sceneAnchor.GetComponent<OVRScenePlane>();
        var volume = sceneAnchor.GetComponent<OVRSceneVolume>();

        var dimensions = volume ? volume.Dimensions : plane ? plane.Dimensions : Vector3.one;

        if(classification.Contains(OVRSceneManager.Classification.WallArt))
        {
            
        }


        // Debug
        // if(classification && classification.Labels.Count > 0)
        // {
            // foreach(var label in classification.Labels)
            // {
            //     Debug.Log(label);
            // }
        // }
        
    }


}
