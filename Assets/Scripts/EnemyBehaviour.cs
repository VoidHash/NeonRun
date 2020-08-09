using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemySpeed = 50;
    public Transform deadZone = null;
    public GameObject enemyDeathParticles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, deadZone.position, Time.deltaTime * enemySpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DeadZone")
        {
            //gameController.increseScoreByTwo();
            if (PlayerBehaviour.isAlive)
            {
                GameController.playerScore++;
            }
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Shield")
        {
            Instantiate(enemyDeathParticles, transform.position, Quaternion.identity);
            AudioSource audio = GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, gameObject.transform.position);
            if (PlayerBehaviour.isAlive)
            {
                GameController.playerScore = +15;
            }
            Destroy(gameObject);
        }
    }
}
