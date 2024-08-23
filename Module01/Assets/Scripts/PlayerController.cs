using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
	[SerializeField] private GameObject claire;
	[SerializeField] private GameObject john;
	[SerializeField] private GameObject thomas;

	[SerializeField] private GameObject claireGoal;
	[SerializeField] private GameObject johnGoal;
	[SerializeField] private GameObject thomasGoal;
	[SerializeField] private GameObject[] stage4objects;
	[SerializeField] private Material clarieMaterial; 

	private GameObject activeCharacter;

	private Rigidbody rb;
	private float speed = 5.0f;
	private float jumpForce = 5.0f;
	private bool isGrounded = true;

	private Vector3 initCameraPosition;
	private Vector3 initClairePosition;
	private Vector3 initJohnPosition;
	private Vector3 initThomasPosition;

	private Quaternion initCameraRotation;
	private Quaternion initClaireRotation;
	private Quaternion initJohnRotation;
	private Quaternion initThomasRotation;

    void Start()
    {
		activeCharacter = null;
		saveInitalPosition();		
    }

	private void saveInitalPosition()
	{
		initCameraPosition = Camera.main.transform.position;
		initClairePosition = claire.transform.position;
		initJohnPosition = john.transform.position;
		initThomasPosition = thomas.transform.position;
		initCameraRotation = Camera.main.transform.rotation;
		initClaireRotation = claire.transform.rotation;
		initJohnRotation = john.transform.rotation;
		initThomasRotation = thomas.transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
		if (claire.activeSelf == false || john.activeSelf == false || thomas.activeSelf == false)
		{
			ResetStage();
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetActiveCharacter(claire);
			SetCameraPosition();
			SetCharacterAttributes(2.0f, 6.0f);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SetActiveCharacter(john);
			SetCameraPosition();
			SetCharacterAttributes(6.0f, 10.0f);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SetActiveCharacter(thomas);
			SetCameraPosition();
			SetCharacterAttributes(4.0f, 8.0f);
		}
		else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Slash))
		{
			if (activeCharacter != null)
			{
				ResetStage();	
			}
		}
		
		if (activeCharacter != null)
		{
			Move();
			SetCameraPosition();
			isGrounded = IsGroundedCheck();
			if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
				Jump();
		}

		if (CheckGoal())
		{
			Debug.Log("All Players have reached the goal!");
			SceneTransitionManager.Instance.LoadNextScene();
		}
    }

	private bool CheckGoal()
	{
		if (claireGoal.GetComponent<GoalScript>().isReached && johnGoal.GetComponent<GoalScript>().isReached && thomasGoal.GetComponent<GoalScript>().isReached)
			return true;
		
		return false;
	}

	private void ResetStage()
	{
		activeCharacter = null;
		claire.SetActive(true);
		john.SetActive(true);
		thomas.SetActive(true);
		Camera.main.transform.position = initCameraPosition;
		claire.transform.position = initClairePosition;
		john.transform.position = initJohnPosition;
		thomas.transform.position = initThomasPosition;
		Camera.main.transform.rotation = initCameraRotation;
		claire.transform.rotation = initClaireRotation;
		john.transform.rotation = initJohnRotation;
		thomas.transform.rotation = initThomasRotation;
		claire.GetComponent<Rigidbody>().velocity = Vector3.zero;
		john.GetComponent<Rigidbody>().velocity = Vector3.zero;
		thomas.GetComponent<Rigidbody>().velocity = Vector3.zero;

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stage4")
		{
			foreach (GameObject obj in stage4objects)
			{
				obj.GetComponent<Renderer>().material = clarieMaterial;
				obj.layer = 9;
			}
		}
	}

	private void SetCameraPosition()
	{
		Camera.main.transform.position = new Vector3(activeCharacter.transform.position.x, activeCharacter.transform.position.y + 5f, activeCharacter.transform.position.z - 15f);
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stage5")
		{
			if (Camera.main.transform.position.y < -3f)
			{
				Camera.main.transform.position = new Vector3(activeCharacter.transform.position.x, -3f, activeCharacter.transform.position.z - 15f);
				if (activeCharacter.transform.position.y < -50f)
				{
					Debug.Log("Game Over! Player fell off the stage! Restart the level.");
					activeCharacter.SetActive(false);
				}
			}
		}
		Camera.main.transform.rotation = Quaternion.Euler(25, 0, 0);
	}

	private void SetCharacterAttributes(float speed, float jumpForce)
	{
		this.speed = speed;
		this.jumpForce = jumpForce;
	}

	private bool IsGroundedCheck()
    {
		return activeCharacter.GetComponentInChildren<IsGround>().isGrounded ;
    }

	private void SetActiveCharacter(GameObject character)
	{
		activeCharacter = character;
		rb = activeCharacter.GetComponent<Rigidbody>();
	}

	private void Move()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3(horizontalInput, 0, 0);
		rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, rb.velocity.z);
	}

	private void Jump()
	{
		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
