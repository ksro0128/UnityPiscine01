using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
	[SerializeField] private GameObject[] gameObjects;

    private void OnCollisionEnter(Collision other) 
	{
		if (this.tag == "ButtonHidden")
		{
			foreach (GameObject go in gameObjects)
			{
				go.SetActive(true);
			}
		}
		else
		{
			foreach (GameObject go in gameObjects)
			{
				if (go.layer == 9)
					go.SetActive(false);
			}
		}
	}

	private void OnCollisionExit(Collision other) 
	{
		if (this.tag == "ButtonHidden")
		{
			foreach (GameObject go in gameObjects)
			{
				go.SetActive(false);
			}
		}
		else
		{
			foreach (GameObject go in gameObjects)
			{
				if (go.layer == 9)
					go.SetActive(true);
			}
		}
	}
}
