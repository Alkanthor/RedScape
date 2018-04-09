using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorAccessPanelController : MonoBehaviour {

    public GameObject _door;
    public GameObject _doorText;
    public string IdCardName;
    public float AccessingDoorTime = 4;
    public float DoorOpenSpeed = 2;
    public float DoorOpenLimit;

    private bool _doorUnlocked;
    
    private bool _isAccessing;
    public bool IsAccessing
    {
        get
        {
            return _isAccessing;
        }
        set
        {
            _isAccessing = value;
            if(_isAccessing)
            {
                StartCoroutine(AccessingDoor());
            }
            else
            {

                StopCoroutine(AccessingDoor());
            }
        }
     }

    IEnumerator AccessingDoor()
    {

        yield return new WaitForSeconds(AccessingDoorTime);
        _doorUnlocked = true;
        Debug.Log("Door access granted...opening door");
        yield return new WaitForSeconds(1);
        var doorTo = _door.transform.position + _door.transform.up * DoorOpenLimit;
        StartCoroutine(OpeningDoor(_door.transform.position, doorTo, DoorOpenSpeed));
    }

    IEnumerator OpeningDoor(Vector3 from, Vector3 to, float speed)
    {
        float step = (speed / (from - to).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            _door.transform.position = Vector3.Lerp(from, to, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        _door.transform.position = to;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == IdCardName)
        {
            _doorText.GetComponent<Text>().text = "Scanning...";
            Debug.Log("Start Accessing door");
            IsAccessing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == IdCardName)
        {
            _doorText.GetComponent<Text>().text = "Insert Card";
            Debug.Log("Stopped Accessing door");
            IsAccessing = false;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
