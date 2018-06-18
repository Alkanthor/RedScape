using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreasureHuntPuzzle : MonoBehaviour {


    [SerializeField]
    private GameObject _safeZone;
    [SerializeField]
    private GameObject _treasure;
    [SerializeField]
    private GameObject _treasureTrigger;
    [SerializeField]
    private GameObject _finishZone;
    [SerializeField]
    private string _playerName;

    [SerializeField]
    private float _moveStep = 1f;
    private ParentCollision _parentCollision;

    private Vector3 _treasureInitPosition;
    private bool _finishTreasureHunt;
	// Use this for initialization
	void Start () {
       _parentCollision = gameObject.GetComponent<ParentCollision>();
        _parentCollision.OnChildTriggerEnter.AddListener(OnChildTriggerEnter);
       // _parentCollision.OnChildTriggerExit.AddListener(OnChildTriggerExit);
        _treasureInitPosition = _treasure.transform.position;
	}
	
    private void OnChildTriggerEnter(GameObject child, Collider other)
    {
        Debug.Log("Child " + child.name + " trigger enter with " + other.name);
        //player moved close to treasure
        if (child.name == _treasureTrigger.name && other.name == _playerName)
        {

            if (!_finishTreasureHunt)
            {
                var target = PlayerManager.Instance.GetPlayerPosition();
                target.y = _treasure.transform.position.y;
                bool tmp = IsInColliders(_treasure.transform.position);             
                _treasure.transform.position = Vector3.MoveTowards(_treasure.transform.position, target, -1 * _moveStep);
                if (!IsInColliders(_treasure.transform.position))
                {
                    Debug.Log("Treasure left safeZone, reseting puzzle");
                    _treasure.transform.position = _treasureInitPosition;
                }
            }
         
        }

        //treasure moved to finishZone, now it can be picked up
        if (child.name == _finishZone.name && other.name == _treasure.name)
        {
            Debug.Log("Child " + child.name + " trigger enter with " + other.name);
            Debug.Log("Treasure hunt finished");
            _finishTreasureHunt = true;
            _parentCollision.OnChildTriggerEnter.RemoveListener(OnChildTriggerEnter);
            //_parentCollision.OnChildTriggerExit.RemoveListener(OnChildTriggerExit);

        }

    }

    private bool IsInColliders(Vector3 position)
    {
        var colliders =_safeZone.GetComponentsInChildren<Collider>();
        for(int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].bounds.Contains(position))
                return true;
        }
        return false;
    }
}
