using System;
using System.Collections;
using UnityEngine;

public class AnomalyScript : MonoBehaviour
{

    public float AnomalyTeleportDelay = 4.0f;

    private Transform otherAnomaly;
    private Collider other;
    private AudioSource sound;

    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        if (name == "anomaly_a")
            otherAnomaly = transform.parent.Find("anomaly_b");
        else
            otherAnomaly = transform.parent.Find("anomaly_a");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "Teleportable")
        {
            other = otherObject;
            StartCoroutine(WaitAndTeleport());
        }
    }

    private IEnumerator WaitAndTeleport()
    {
        yield return new WaitForSeconds(5);
        if (other != null)
        {
            other.transform.position = new Vector3(otherAnomaly.transform.position.x, other.transform.position.y, otherAnomaly.transform.position.z);
            sound.Play();
        }
    }

    void OnTriggerExit(Collider otherObject)
    {
        if (otherObject.gameObject.tag == "Teleportable")
        {
            StopCoroutine(WaitAndTeleport());
            other = null;
        }
    }
}
