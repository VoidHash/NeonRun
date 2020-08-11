using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour, OnChildCollision
{
    public GameObject leftSensor;
    public GameObject frontSensor;
    public GameObject rightSensor;

    private bool isLeftFree = true;
    private bool isFrontFree = true;
    private bool isRightFree = true;

    public static bool isShielded = false;
    public GameObject playerDeathParticles;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isShielded == false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Instantiate(playerDeathParticles, transform.position, Quaternion.identity);
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audio.clip, gameObject.transform.position);
                PlayerBehaviour.isAlive = false;
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("PowerUp"))
            {
                GameController.playerScore = +7;
                shield.SetActive(true);
                isShielded = true;
                ShieldBehaviour.shieldEnergy = 2;
                AudioSource audio = other.gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audio.clip, other.gameObject.transform.position);
                Destroy(other.gameObject);
            }

        }
    }

    void OnChildCollision.CollisionEvent(string id, bool isFree)
    {
        switch (id)
        {
            case "Left":
                isLeftFree = isFree;
                break;
            case "Front":
                isFrontFree = isFree;
                break;
            case "Right":
                isRightFree = isFree;
                break;
        }
        
        if (!isFrontFree) {
            if(isLeftFree)
            {
                gameObject.transform.position = new Vector3(-10, -12, 37);
            }else if (isRightFree)
            {
                gameObject.transform.position = new Vector3(10, -12, 37);
            }
        }

        if (!isFrontFree &&  isLeftFree && !isRightFree)
        {
            gameObject.transform.position = new Vector3(0, -12, 37);
        }

        if (!isFrontFree && !isLeftFree && isRightFree)
        {
            gameObject.transform.position = new Vector3(0, -12, 37);
        }
    }
}

interface OnChildCollision
{
    void CollisionEvent(string id, bool isFree);
}
