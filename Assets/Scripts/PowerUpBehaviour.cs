using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{

    public static float speed = 0;
    [HideInInspector]
    public Transform deadZone = null;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, deadZone.position, Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone") || other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
