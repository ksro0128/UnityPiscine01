using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private GameObject destination;

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ClaireGoal") || other.CompareTag("JohnGoal") || other.CompareTag("ThomasGoal"))
		{
			Transform parentTransform = other.transform.parent;
			if (parentTransform != null)
			{
				parentTransform.position = destination.transform.position;
			}
		}
	}
}
