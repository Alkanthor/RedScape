using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyScript : MonoBehaviour
{
    public GameObject targetAnomaly;


    // Use this for initialization
    void Start ()
    {
        if (this.Name == "anomaly_a")
        {
            targetAnomaly = this.Transform.Parent.Find("anomaly_b");
        }
        else
        {
            targetAnomaly = this.Transform.Parent.Find("anomaly_a");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider item)
    {
        if (item.Tag=="Teleportable")
        {
            item.transform.position = targetAnomaly.transform.position;
        }
    }
}
