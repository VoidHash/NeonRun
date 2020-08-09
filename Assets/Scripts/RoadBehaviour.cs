using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    GameObject newRoad = null;
    private bool doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        newRoad = GameObject.Find("GameController").GetComponent<GameController>().road;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward *  Time.deltaTime *  -50;

        if(!doOnce && transform.position.z <= -150)
        {
            doOnce = true;
            Instantiate(newRoad, new Vector3(-10.35f, -15.45f, 1049.899f), Quaternion.identity);
        }

        if(transform.position.z <= -1050)
        {
            Destroy(gameObject);
        }
    }


}
