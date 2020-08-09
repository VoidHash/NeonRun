using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public static bool isShielded = false;
	public static bool isAlive = true;
	private Touch theTouch;
	public GameObject playerDeathParticles;
	public GameObject shield;

	// Start is called before the first frame update
	void Start()
    {
		shield.SetActive(false);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		playerMovement();
	}

	private void resolvedShield ()
    {

    }

	private void playerMovement()
    {
		if (Input.touchCount > 0)
		{
			theTouch = Input.GetTouch(0);

			if (theTouch.phase == TouchPhase.Moved)
			{
				if (transform.position.x >= -11 && transform.position.x <= 11)
				{
					transform.position = new Vector3(
						transform.position.x + theTouch.deltaPosition.x * 0.05f,
						transform.position.y,
						transform.position.z);
				}
				else
				{
					if (transform.position.x >= -11)
					{
						transform.position = new Vector3(10.9f, transform.position.y, transform.position.z);
					}
					else if (transform.position.x <= 11)
					{
						transform.position = new Vector3(-10.9f, transform.position.y, transform.position.z);
					}

				}

			}
		}
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
				GameController.playerScore =+ 7;
				shield.SetActive(true);
				isShielded = true;
				ShieldBehaviour.shieldEnergy = 2;
				AudioSource audio = other.gameObject.GetComponent<AudioSource>();
				AudioSource.PlayClipAtPoint(audio.clip, other.gameObject.transform.position);
				Destroy(other.gameObject);
			}

		}
	}
}
