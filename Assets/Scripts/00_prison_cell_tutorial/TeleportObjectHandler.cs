using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class TeleportObjectHandler : MonoBehaviour {


    private VRTK_InteractableObject _interactableObject;

	// Use this for initialization
	void Start () {
        _interactableObject = GetComponent<VRTK_InteractableObject>();
        _interactableObject.InteractableObjectGrabbed += TeleportGrabbed;
        gameObject.SetActive(false);
	}

    private void TeleportGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("teleport grabbed");
        LevelManagerPrisonCell00.Instance.GrabbedTeleport = true;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void EnableTeleport()
    {
       
        gameObject.SetActive(true);
    }
}
