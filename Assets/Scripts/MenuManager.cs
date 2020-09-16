using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject playButton;
	private StepManager stepManager;
	private AnimatorController animatorController;
	public Animator animator;
	private SceneManager sceneManager;

	private void Start()
	{
		stepManager = GameObject.Find("GameManager").GetComponent<StepManager>();
		animatorController = GameObject.Find("GameManager").GetComponent<AnimatorController>();
		playButton.SetActive(true);
	}

	public void Play()
	{
		animatorController.ChangeBottleTransform();
		stepManager.ChangeStep(StepManager.STEPS.Step1);
		playButton.SetActive(false);
	}

	public void Restart()
	{
		Scene scene = SceneManager.GetActiveScene();

		SceneManager.LoadScene(scene.buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
