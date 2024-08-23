using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
	public static SceneTransitionManager Instance { get; private set; }

	private int currentSceneIndex = 0;
	private string[] sceneNames = { "Stage1", "Stage2", "Stage3", "Stage4", "Stage5" };

	private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

	public void LoadNextScene()
	{
		if (currentSceneIndex < sceneNames.Length - 1)
		{
			currentSceneIndex++;
		}
		else
		{
			currentSceneIndex = 0;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames[currentSceneIndex]);
	}
}
