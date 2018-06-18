using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class CardAttach : MonoBehaviour {

    private GameObject _attachTo;
    public bool Attach;

    private void Start()
    {
        this.GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += OnCardGrabbed;
    }

    private void OnCardGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Attach = false;
    }


    public void SetAttachTo(GameObject attachTo)
    {
        _attachTo = attachTo;
    }
    private void Update()
    {
        if(Attach)
        {
            this.transform.position = _attachTo.transform.position;
        }
    }

}
