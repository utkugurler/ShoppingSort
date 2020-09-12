using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
	private StepManager stepManager;
	private ScoreManager scoreManager;

	private int checkStepOne = 2; // 0 olunca sonrakine geçebilir

	private void Start()
	{
		GameObject gameManagerObj = GameObject.Find("GameManager");
		stepManager = gameManagerObj.GetComponent<StepManager>();
		scoreManager = gameManagerObj.GetComponent<ScoreManager>();
	}

	private void Update()
	{
		if(checkStepOne == 0)
		{
			stepManager.ChangeStep();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int currentStep = stepManager.CurrentStep;
		if(currentStep == 0)
		{
			// İlk stepte yapılacaklar
			if (other.gameObject.name == this.gameObject.name) // İsimle tagler birse puan artar
			{
				scoreManager.IncreasScore();
				stepManager.CheckStep(StepManager.STEPS.Step1);
				Destroy(this.gameObject);
			}
			else
			{
				scoreManager.DescreasScore();
			}
		}
		else if (currentStep == 1)
		{

		}
		else if (currentStep == 2)
		{

		}
		else
		{
			// Şuan hiçbir stepte değil!
		}
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}
