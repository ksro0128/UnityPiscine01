using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
	[SerializeField] private GameObject[] gameObjects;
	[SerializeField] private Material claireMaterial;
	[SerializeField] private Material johnMaterial;
	[SerializeField] private Material thomasMaterial;

    private void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag("John"))
		{
			foreach (GameObject go in gameObjects)
			{
				go.GetComponent<Renderer>().material = johnMaterial;
				go.layer = other.gameObject.layer + 3;
			}
		}
		else if (other.gameObject.CompareTag("Claire"))
		{
			foreach (GameObject go in gameObjects)
			{
				go.GetComponent<Renderer>().material = claireMaterial;
				go.layer = other.gameObject.layer + 3;
			}
		}
		else if (other.gameObject.CompareTag("Thomas"))
		{
			foreach (GameObject go in gameObjects)
			{
				go.GetComponent<Renderer>().material = thomasMaterial;
				go.layer = other.gameObject.layer + 3;
			}
		}
	}
}
