using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject playButton;
	private StepManager stepManager;

	private void Start()
	{
		stepManager = GameObject.Find("GameManager").GetComponent<StepManager>();
		playButton.SetActive(true);
	}

	public void Play()
	{
		stepManager.ChangeStep(StepManager.STEPS.Step1);
		playButton.SetActive(false);
	}
}
