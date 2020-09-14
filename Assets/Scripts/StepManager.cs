using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
	public enum STEPS
	{
		Step1,
		Step2,
		Step3,
		StepMenu
	}

	#region PRIVATE VARIABLES
	private int stepCount;
	private int[] checkers = new int[3] { 3, 12, 3 };
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
					ChangeStep(STEPS.Step2);
					checkers[(int)STEPS.Step1] = -1;
					// İkinci levelda kapaklar olmayacağından gizliyoruz sahnedekilerini
					CupActivate(false);
				}
				else if (checkers[(int)STEPS.Step2] == 0)
				{
					ChangeStep(STEPS.Step3);
					checkers[(int)STEPS.Step2] = -1;
					CupActivate(true);
					PillRigidbodyGravityActivate(false);
				}
				else if(checkers[(int)STEPS.Step3] == 0)
				{
					// ChangeStep();
					checkers[(int)STEPS.Step3] = -1;
				}
			}
		}
	}

	private void PillRigidbodyGravityActivate(bool flag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Pill");
		foreach (var item in objs)
		{
			item.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	public void CupActivate(bool flag)
	{
		GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottle");
		foreach (var item in bottles)
		{
			for (int i = 0; i < item.transform.childCount; i++)
			{
				if (item.transform.GetChild(i).name == "cup")
				{
					item.transform.GetChild(i).gameObject.SetActive(flag);
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

	public void ChangeStep(STEPS steps)
	{
		if(steps == STEPS.Step1)
		{
			CurrentStep = 0;
		}
		else if(steps == STEPS.Step2)
		{
			CurrentStep = 1;
		}
		else if(steps == STEPS.Step3)
		{
			CurrentStep = 2;
		}
		else if(steps == STEPS.StepMenu)
		{
			CurrentStep = -1;
		}
	}

	public void CheckStep(STEPS steps)
	{
		checkers[(int)steps]--;
	}
}
