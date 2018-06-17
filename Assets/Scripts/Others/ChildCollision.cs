using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour {

    public ParentCollision _parent;

    private void OnTriggerEnter(Collider other)
    {
        _parent.OnChildTriggerEnter.Invoke(this.gameObject, other);
    }

    private void OnTriggerStay(Collider other)
    {
        _parent.OnChildTriggerStay.Invoke(this.gameObject, other);
    }

    private void OnTriggerExit(Collider other)
    {
        _parent.OnChildTriggerExit.Invoke(this.gameObject, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _parent.OnChildCollisionEnter.Invoke(this.gameObject, collision);   
    }

    private void OnCollisionStay(Collision collision)
    {
        _parent.OnChildCollisionStay.Invoke(this.gameObject, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        _parent.OnChildCollisionExit.Invoke(this.gameObject, collision);
    }

}
