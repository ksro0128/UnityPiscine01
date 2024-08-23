using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Claire") || other.CompareTag("John") || other.CompareTag("Thomas"))
		{
			other.gameObject.SetActive(false);
		}
	}
}
