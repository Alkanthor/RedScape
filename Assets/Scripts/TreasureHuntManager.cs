using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreasureHuntManager : MonoBehaviour {


    [SerializeField]
    private GameObject _safeZone;
    [SerializeField]
    private GameObject _treasure;
    [SerializeField]
    private GameObject _finishZone;
    [SerializeField]
    private string _playerName;

    [SerializeField]
    private float _moveStep = 1f;
    private ParentCollisionManager _parentCollisionManager;

    private Vector3 _treasureInitPosition;
    private bool _finishTreasureHunt;
	// Use this for initialization
	void Start () {
       _parentCollisionManager = gameObject.GetComponent<ParentCollisionManager>();
        _parentCollisionManager.OnChildTriggerEnterEvent.AddListener(OnChildTriggerEnter);
        _parentCollisionManager.OnChildTriggerExitEvent.AddListener(OnChildTriggerExit);
        _treasureInitPosition = _treasure.transform.position;
	}
	
    private void OnChildTriggerEnter(GameObject child, Collider other)
    {
        Debug.Log("Child " + child.name + " trigger enter with " + other.name);
        //player moved close to treasure
        if(child.name == _treasure.name && other.name == _playerName)
        {
            if(!_finishTreasureHunt)
            {
                var target = other.gameObject.transform.position;
                target.y = child.transform.position.y;
                _treasure.transform.position = Vector3.MoveTowards(child.transform.position, target, -1 * _moveStep);
            }
         
        }

        //treasure moved to finishZone, now it can be picked up
        if (child.name == _finishZone.name && other.name == _treasure.name)
        {
            Debug.Log("Treasure hunt finished");
            _finishTreasureHunt = true;
            _parentCollisionManager.OnChildTriggerEnterEvent.RemoveListener(OnChildTriggerEnter);
            _parentCollisionManager.OnChildTriggerExitEvent.RemoveListener(OnChildTriggerExit);

        }

    }

    private void OnChildTriggerExit(GameObject child, Collider other)
    {
        Debug.Log("Child " + child.name + " trigger exit with " + other.name);

        //treasure moved from safeZone, treaure put to start position
        if (child.name == _safeZone.name && other.name == _treasure.name)
        {
            _treasure.transform.position = _treasureInitPosition;
        }
    }
 

}
