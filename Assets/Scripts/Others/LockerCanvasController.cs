using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerCanvasController : MonoBehaviour {

    [SerializeField]
    private string OpenText;
    [SerializeField]
    private Color OpenColor;
    [SerializeField]
    private string LockText;
    [SerializeField]
    private Color LockColor;

    private Text _lockerText;

    private void Start()
    {
        _lockerText = GetComponentInChildren<Text>();
    }
    public void OnIsDoorOpen(GameObject door, bool open)
    {
       var parentDoor = gameObject.GetComponentInParent<LockerController>().gameObject;
        if(door == parentDoor)
        {
            if (open)
            {
                _lockerText.text = OpenText;
                _lockerText.color = OpenColor;
            }
            else
            {
                _lockerText.text = LockText;
                _lockerText.color = LockColor;
            }
        }
        
    }
}
