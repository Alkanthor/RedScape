using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object " + this.name + " collided with" + collision.gameObject.name);
    }
}
