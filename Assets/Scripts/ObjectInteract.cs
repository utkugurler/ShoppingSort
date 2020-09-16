using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ObjectInteract : MonoBehaviour
{
	private StepManager stepManager;
	private ScoreManager scoreManager;
	private ColorController colorController;
	private AnimatorController animatorController;
	public TouchDragDrop touchDragDrop;

	private int checkStepOne = 2; // 0 olunca sonrakine geçebilir

	bool step3TriggerFlag = false;

	public Transform[] step3Transforms;
	
	private BoxCollider[] collide;

	private void Start()
	{
		GameObject gameManagerObj = GameObject.Find("GameManager");
		stepManager = gameManagerObj.GetComponent<StepManager>();
		scoreManager = gameManagerObj.GetComponent<ScoreManager>();
		colorController = GameObject.Find("GameManager").GetComponent<ColorController>();
		animatorController = GameObject.Find("GameManager").GetComponent<AnimatorController>();

		touchDragDrop = this.GetComponent<TouchDragDrop>();
		collide = stepManager.collidersStep3;
	}

	private void Update()
	{
		if(checkStepOne == 0)
		{
			stepManager.ChangeStep(StepManager.STEPS.Step2);
			animatorController.ChangeBottleTransform();

		}

		if (step3TriggerFlag == true && touchDragDrop.GetDrag() == false)
		{
			
			Step3TransformDirect(this.gameObject.name);
			
		}

		if(stepManager.CurrentStep == 2 && this.gameObject.tag == "Bottle")
		{
			foreach (var item in collide)
			{
				if (touchDragDrop.GetDrag() == true)
				{
					item.isTrigger = false;
				}
				else
				{
					item.isTrigger = true;
				}
			}
		}
		

	}

	private void Step3TransformDirect(string name)
	{
		if (name == "Orange")
		{
			// step3Transforms[0]
			//Lerp position
			Transform currentView = step3Transforms[0];
			//transform.position = Vector3.Lerp(transform.position, currentView.position, 2 * Time.deltaTime);
			transform.position = currentView.position;
		}
		else if (name == "Pink")
		{
			Transform currentView = step3Transforms[1];
			//transform.position = Vector3.Lerp(transform.position, currentView.position, 2 * Time.deltaTime);
			transform.position = currentView.position;
		}
		else if (name == "Green")
		{
			Transform currentView = step3Transforms[2];
			//transform.position = Vector3.Lerp(transform.position, currentView.position, 2 * Time.deltaTime);
			transform.position = currentView.position;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int currentStep = stepManager.CurrentStep;
		bool getDrag = touchDragDrop.GetDrag();
		if (currentStep == 0)
		{
			if(this.gameObject.tag == "UI")
			{
				// İlk stepte yapılacaklar
				if (other.gameObject.name == this.gameObject.name) // İsimle tagler birse puan artar
				{
					scoreManager.IncreasScore();
					stepManager.CheckStep(StepManager.STEPS.Step1);
					if (other.gameObject.name == "Orange")
					{
						for (int i = 0; i < other.transform.childCount; i++)
						{
							if (other.transform.GetChild(i).name == "bottle")
							{
								other.transform.GetChild(i).GetComponent<MeshRenderer>().material = colorController.GetMaterial(ColorController.COLOR.ORANGE);
							}
						}
					}
					else if (other.gameObject.name == "Pink")
					{
						for (int i = 0; i < other.transform.childCount; i++)
						{
							if (other.transform.GetChild(i).name == "bottle")
							{
								other.transform.GetChild(i).GetComponent<MeshRenderer>().material = colorController.GetMaterial(ColorController.COLOR.PINK);
							}
						}
					}
					else if (other.gameObject.name == "Green")
					{
						for (int i = 0; i < other.transform.childCount; i++)
						{
							if (other.transform.GetChild(i).name == "bottle")
							{
								other.transform.GetChild(i).GetComponent<MeshRenderer>().material = colorController.GetMaterial(ColorController.COLOR.GREEN);
							}
						}
					}
					Destroy(this.gameObject);
				}
				else
				{
					scoreManager.DescreasScore();
				}
			}
			
		}
		else if (currentStep == 1)
		{
			// İlaç atma
			if(this.gameObject.tag == "Pill")
			{
				// Doğruysa olacak öbür türlü destroy et
				if(this.gameObject.name.Contains(other.gameObject.name))
				{
					this.gameObject.transform.parent = other.gameObject.transform;
					//other.transform.parent = this.gameObject.transform;

					this.gameObject.transform.tag = "SuccessPill";
					StartCoroutine(KinematicFalseWait());
					scoreManager.IncreasScore();
					bool flag = SearchPill();

					if(flag == true)
					{
						animatorController.ChangeBottleTransform();

						stepManager.ChangeStep(StepManager.STEPS.Step3);
						StartCoroutine(stepManager.CupActivate(true));
					}
					else
					{

					}

				}
				else
				{
					Destroy(this.gameObject);
					scoreManager.DescreasScore();
				}
			}
		}
		else if (currentStep == 2 && this.gameObject.tag == "Bottle")
		{

			if (this.gameObject.name == other.gameObject.name)
			{
				step3TriggerFlag = true;
				Debug.Log("Başarılı");
				// touchDragDrop.enabled = false;
				stepManager.Step3Increase();
				if (this.gameObject.name == "Orange")
				{
					stepManager.step3Flags[0] = true;
				}
				else if(this.gameObject.name == "Pink")
				{
					stepManager.step3Flags[1] = true;
				}
				else if(this.gameObject.name == "Green")
				{
					stepManager.step3Flags[2] = true;
				}
			}
			else
			{
				// puan düş yanlış de
			}
			
		}
		else
		{
			// Şuan hiçbir stepte değil!
		}
	}

	private IEnumerator KinematicFalseWait()
	{

		yield return new WaitForSeconds(2);
		this.GetComponent<Rigidbody>().isKinematic = true;

	}

	private void OnTriggerExit(Collider other)
	{
		int currentStep = stepManager.CurrentStep;

		if (currentStep == 2)
		{
			// step3TriggerFlag = false;
		}
	}

	private bool SearchPill()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("SuccessPill");

		if(objs.Length == 12)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}


