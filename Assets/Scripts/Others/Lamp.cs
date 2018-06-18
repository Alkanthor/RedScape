using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using VRTK;

public class Lamp : VRTK_InteractableObject
{
    private Light lampLight;


    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);

        TurnLight();
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
    }

    protected void Start()
    {
        lampLight = this.GetComponentInChildren<Light>();
        lampLight.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void TurnLight()
    {
        lampLight.enabled = !lampLight.isActiveAndEnabled;
        Debug.Log("Turned on: " + lampLight.isActiveAndEnabled);
    }

}

