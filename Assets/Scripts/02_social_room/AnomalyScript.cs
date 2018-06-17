using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AnomalyScript : MonoBehaviour
{
    public Transform[] Anomalies;
    public float AnomalyTeleportDelay = 4;
    public AudioSource teleportSound;

    private Dictionary<GameObject, int> currentAnomalyIndexes;
    private ParentCollision parentCollisitionEvents;

   private Dictionary<GameObject, Coroutine> anomalyCoroutines;

    private HashSet<GameObject> objectsInAnomaly;

    private void Start()
    {
        currentAnomalyIndexes = new Dictionary<GameObject, int>();
        anomalyCoroutines = new Dictionary<GameObject, Coroutine>();
        objectsInAnomaly = new HashSet<GameObject>();

        parentCollisitionEvents = this.GetComponent<ParentCollision>();
        parentCollisitionEvents.OnChildTriggerEnter.AddListener(OnChildTriggerEnter);
        parentCollisitionEvents.OnChildTriggerExit.AddListener(OnChildTriggerExit);
    }

    private int GetAnomalyIndex(GameObject anomaly)
    {
        for(int i = 0; i < Anomalies.Length; ++i)
        {
            if (Anomalies[i].name == anomaly.name)
                return i;
        }
        return -1;
    }
    private void OnChildTriggerEnter(GameObject child, Collider other)
    {
        if(other.tag == UnityStrings.TAG_TELEPORTABLE)
        {
            objectsInAnomaly.Add(other.gameObject);
            var currentIndex = GetAnomalyIndex(child);
            if(currentIndex >= 0)
            {
                currentAnomalyIndexes.Add(other.gameObject, currentIndex);
                Debug.Log("object " + other.name + " will be teleported");
                anomalyCoroutines.Add(other.gameObject, StartCoroutine(TeleportObjectToNextAnomaly(other.gameObject)));
            }

        }
    }

    private IEnumerator TeleportObjectToNextAnomaly(GameObject objectToTeleport)
    {
        yield return new WaitForSeconds(AnomalyTeleportDelay);
        var nextAnomalyIndex = currentAnomalyIndexes[objectToTeleport] + 1;
        if (nextAnomalyIndex >= Anomalies.Length)
            nextAnomalyIndex = 0;
        Debug.Log("teleporting object " + objectToTeleport.name + " from " + Anomalies[currentAnomalyIndexes[objectToTeleport]].name + " to " + Anomalies[nextAnomalyIndex].name);
        objectToTeleport.transform.position = Anomalies[nextAnomalyIndex].position;
        teleportSound.Play();

    }

    private void OnChildTriggerExit(GameObject child, Collider other)
    {
        if(objectsInAnomaly.Contains(other.gameObject))
        {
            StopCoroutine(anomalyCoroutines[other.gameObject]);
            anomalyCoroutines.Remove(other.gameObject);
            objectsInAnomaly.Remove(other.gameObject);
            currentAnomalyIndexes.Remove(other.gameObject);
        }
    }

}
