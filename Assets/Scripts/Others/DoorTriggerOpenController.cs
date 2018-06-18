using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerOpenController : MonoBehaviour {


    public AudioClip OpenDoorClip;
    public string PlayerColliderName;
    public float DoorOpenSpeed = 2;
    public float DoorOpenLimit;

    private GameObject _door;
    private AudioSource _sound;
    private Coroutine _doorCoroutine;
    private Vector3 _initDoorPosition;
    private ParentCollision _parentCollision;
    private bool _isOpening;
    private bool _isClosing;
    IEnumerator OpeningDoor(Vector3 from, Vector3 to, float speed)
    {
        yield return new WaitForSeconds(0.5f);
        _sound.Play();
        float step = (speed / (from - to).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            _door.transform.position = Vector3.Lerp(from, to, t); // Move objectToMove closer to b
            if (!_isClosing && !_isOpening)
            {
                _sound.Stop();
            }
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame

        }
        _door.transform.position = to;
        _isOpening = false;
        _isClosing = false;
    }

    private void OnChildTriggerEnter(GameObject child, Collider other)
    {
        if(other.name == PlayerColliderName)
        {
            Debug.Log("opening door " + this.name);
            var doorTo = _initDoorPosition + _door.transform.up * DoorOpenLimit;
            if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
            _isClosing = false;
            _isOpening = true;
            _doorCoroutine = StartCoroutine(OpeningDoor(_door.transform.position, doorTo, DoorOpenSpeed));
        }
    }
    private void OnChildTriggerExit(GameObject child, Collider other)
    {
        if (other.name == PlayerColliderName)
        {
            Debug.Log("closing door " + this.name);
            if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
            _isOpening = false;
            _isClosing = true;
            _doorCoroutine = StartCoroutine(OpeningDoor(_door.transform.position, _initDoorPosition, DoorOpenSpeed));
        }
    }
    // Use this for initialization
    void Start()
    {
        _sound = this.GetComponent<AudioSource>();
        _sound.clip = OpenDoorClip;
        _sound.loop = false;
        _door = this.gameObject;
        _initDoorPosition = _door.transform.position;
        _parentCollision = this.GetComponent<ParentCollision>();
        _parentCollision.OnChildTriggerEnter.AddListener(OnChildTriggerEnter);
        _parentCollision.OnChildTriggerExit.AddListener(OnChildTriggerExit);

    }
}
