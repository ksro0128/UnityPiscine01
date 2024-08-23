using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private GameObject pathA;
	[SerializeField] private GameObject pathB;

	private bool isVisible = true;

    void Update()
    {
		if (Vector3.Distance(transform.position, pathB.transform.position) < 0.1f)
		{
			transform.position = pathA.transform.position;
			if (!isVisible)
			{
				isVisible = true;
				GetComponent<Renderer>().enabled = true;
				GetComponent<Collider>().enabled = true;
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, pathB.transform.position, speed * Time.deltaTime);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (isVisible)
		{
			if (other.gameObject.CompareTag("Claire") || other.gameObject.CompareTag("John") || other.gameObject.CompareTag("Thomas"))
			{
				other.gameObject.SetActive(false);
				isVisible = false;
				GetComponent<Renderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
				Debug.Log("Game Over! " + other.gameObject.tag + " has been hit by a bullet! Restart the level.");
			}
			else if (other.gameObject.CompareTag("Wall"))
			{
				isVisible = false;
				GetComponent<Renderer>().enabled = false;
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
