using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
	public bool isReached = false;

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == this.gameObject.tag)
		{
			isReached = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == this.gameObject.tag)
		{
			isReached = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == this.gameObject.tag)
		{
			isReached = true;
		}
	}
}
