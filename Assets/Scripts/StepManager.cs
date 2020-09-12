using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
	public enum STEPS
	{
		Step1,
		Step2,
		Step3
	}
	#region PRIVATE VARIABLES
	private int stepCount;
	private int[] checkers = new int[3] { 3, 3, 3 };
	#endregion

	#region PUBLIC VARIABLES
	public int CurrentStep;
	public GameObject[] StepEnvironments;
	#endregion

	private void Start()
	{
		stepCount = StepEnvironments.Length;
	}

	private void Update()
	{
		StepChecker(stepCount);

		for (int i = 0; i < checkers.Length; i++)
		{
			if(checkers[i] == 0)
			{
				if(checkers[(int)STEPS.Step1] == 0)
				{
					CurrentStep++;
					checkers[(int)STEPS.Step1] = -1;
				}
			}
		}
	}

	private void StepChecker(int stepCount)
	{
		for (int i = 0; i < stepCount; i++)
		{
			if(CurrentStep == i)
			{
				StepEnvironments[i].SetActive(true);
			}
			else
			{
				StepEnvironments[i].SetActive(false);
			}
		}
	}

	public void ChangeStep()
	{
		CurrentStep++;
	}

	public void CheckStep(STEPS steps)
	{
		checkers[(int)steps]--;
	}
}
