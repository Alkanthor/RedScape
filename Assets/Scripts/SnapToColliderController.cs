using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SnapToColliderController : MonoBehaviour {

    public bool ObjectIsSnapped;

    [SerializeField]
    private VRTK_SnapDropZone _snapDropZone;

    private Collider[] _colliders;
	// Use this for initialization
	void Start () {

        
        _colliders = GetComponents<Collider>();

        //at the beggining disable colliders
        EnableColliders(false);

        _snapDropZone.ObjectSnappedToDropZone += (sender, e) =>
        {
            ObjectIsSnapped = true;
            EnableColliders(true);
        };

        _snapDropZone.ObjectUnsnappedFromDropZone += (sender, e) =>
        {
            ObjectIsSnapped = false;
            EnableColliders(false);
        };

       
    }

    private void EnableColliders(bool enable)
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = enable;
        }
    }

}
