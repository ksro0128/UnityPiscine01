using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
	public bool isGrounded = true;

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer >= 9 && other.gameObject.layer <= 11)
		{
			if (gameObject.layer + 3 == other.gameObject.layer)
			{
				isGrounded = true;
			}
		}
		else
			isGrounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		isGrounded = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer >= 9 && other.gameObject.layer <= 11)
		{
			if (gameObject.layer + 3 == other.gameObject.layer)
			{
				isGrounded = true;
			}
		}
		else
			isGrounded = true;
	}
}
