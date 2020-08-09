using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    private GameObject player;
    public static int shieldEnergy = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            shieldEnergy--;
            if (shieldEnergy <= 0)
            {
                gameObject.SetActive(false);
                PlayerBehaviour.isShielded = false;
            }
        }else  if (other.gameObject.CompareTag("PowerUp"))
        {
            ShieldBehaviour.shieldEnergy = 2;
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, gameObject.transform.position);
            Destroy(other.gameObject);
        }
    }
}
