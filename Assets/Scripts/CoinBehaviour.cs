using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public float coinSpeed = 50;
    public Transform deadZone = null;
    public GameObject coinDeathParticles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, deadZone.position, Time.deltaTime * coinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Shield"))
        {
            GameController.playerScore = +10;
            Instantiate(coinDeathParticles, transform.position, Quaternion.identity);
            AudioSource audio = GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, gameObject.transform.position);
            Destroy(gameObject);
        }
        else if( other.gameObject.CompareTag("Enemy") || 
            other.gameObject.CompareTag("DeadZone") ||
            other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}
