using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class TeleportObjectHandler : MonoBehaviour {


    private VRTK_InteractableObject _interactableObject;

    public GameObject _leftBall;
    public GameObject _rightBall;

	// Use this for initialization
	void Start () {
        _interactableObject = GetComponent<VRTK_InteractableObject>();
        _interactableObject.InteractableObjectGrabbed += TeleportGrabbed;
        gameObject.SetActive(false);

        _leftBall.SetActive(false);
        _rightBall.SetActive(false);

    }

    private void TeleportGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("teleport grabbed");
        LevelManagerPrisonCell00.Instance.GrabbedTeleport = true;
        Destroy(this.gameObject);
        _leftBall.SetActive(true);
        _rightBall.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void EnableTeleport()
    {
       
        gameObject.SetActive(true);
    }
}
