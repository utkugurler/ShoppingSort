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
	private AnimatorController animatorController;
	#endregion

	#region PUBLIC VARIABLES
	public int CurrentStep;
	public Animator animator;
	public GameObject[] StepEnvironments;
	public BoxCollider[] collidersStep3;
	public TouchDragDrop touchDragDrop;
	public bool[] step3Flags = new bool[3] { false, false, false };
	public GameObject finalPanel;
	#endregion

	private void Start()
	{
		animatorController = GameObject.Find("GameManager").GetComponent<AnimatorController>();
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
					StartCoroutine(CupActivate(false));
					animatorController.ChangeBottleTransform();

				}
				else if (checkers[(int)STEPS.Step2] == 0)
				{
					ChangeStep(STEPS.Step3);
					checkers[(int)STEPS.Step2] = -1;
					StartCoroutine(CupActivate(true));
					PillRigidbodyGravityActivate(false);
					animatorController.ChangeBottleTransform();

				}
				else if(checkers[(int)STEPS.Step3] == 0)
				{
					// ChangeStep();
					checkers[(int)STEPS.Step3] = -1;
				}
			}
		}

		if(CurrentStep == 2)
		{

			if (step3Flags[0] && step3Flags[1] && step3Flags[2])
				finalPanel.SetActive(true);

		}
	}
	public void Step3Increase()
	{
		checkers[(int)STEPS.Step3]--;
	}

	private void PillRigidbodyGravityActivate(bool flag)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("SuccessPill");
		foreach (var item in objs)
		{
			item.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	public IEnumerator CupActivate(bool flag)
	{
		yield return new WaitForSeconds(1);

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
