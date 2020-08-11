using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBehaviour : MonoBehaviour
{

    public string myId;
    private bool iAmFree = true;
    private OnChildCollision onChildCollision;
    public GameObject parentGameObject;

    void Start()
    {
        onChildCollision = parentGameObject.GetComponent<IABehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            iAmFree = false;
            onChildCollision.CollisionEvent(myId, iAmFree);
        }else if (other.CompareTag("LateralBar"))
        {
            iAmFree = false;
            onChildCollision.CollisionEvent(myId, iAmFree);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            iAmFree = true;
            onChildCollision.CollisionEvent(myId, iAmFree);
        }
        else if (other.CompareTag("LateralBar"))
        {
            iAmFree = false;
            onChildCollision.CollisionEvent(myId, iAmFree);
        }
    }
}
