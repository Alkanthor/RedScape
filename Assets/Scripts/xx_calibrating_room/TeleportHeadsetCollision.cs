using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


[RequireComponent(typeof(VRTK_HeadsetCollision))]
//[AddComponentMenu("VRTK/Scripts/Presence/VRTK_HeadsetCollisionFade")] 
public class TeleportHeadsetCollision : MonoBehaviour
{

    [Header("Custom Settings")]

    [Tooltip("The VRTK Headset Collision script to use when determining headset collisions. If this is left blank then the script will need to be applied to the same GameObject.")]
    public VRTK_HeadsetCollision headsetCollision;
    // [Tooltip("The VRTK Headset Fade script to use when fading the headset. If this is left blank then the script will need to be applied to the same GameObject.")]
    // public VRTK_HeadsetFade headsetFade;

    private bool _isTeleporting;
    protected virtual void OnEnable()
    {
            //headsetFade = (headsetFade != null ? headsetFade : GetComponentInChildren<VRTK_HeadsetFade>());
        headsetCollision = (headsetCollision != null ? headsetCollision : GetComponentInChildren<VRTK_HeadsetCollision>());

        headsetCollision.HeadsetCollisionDetect += new HeadsetCollisionEventHandler(OnHeadsetCollisionDetect);
       // headsetCollision.HeadsetCollisionEnded += new HeadsetCollisionEventHandler(OnHeadsetCollisionEnded);
    }

    protected virtual void OnDisable()
    {
        headsetCollision.HeadsetCollisionDetect -= new HeadsetCollisionEventHandler(OnHeadsetCollisionDetect);
       // headsetCollision.HeadsetCollisionEnded -= new HeadsetCollisionEventHandler(OnHeadsetCollisionEnded);
    }

    protected virtual void OnHeadsetCollisionDetect(object sender, HeadsetCollisionEventArgs e)
    {
        // Invoke("StartFade", timeTillFade);
        if(e.collider.name == "Teleport" && _isTeleporting == false)
        {
            _isTeleporting = true;
            CalibratingRoomLevelManager.Instance.TeleportPlayer();
        }

        
    }


}