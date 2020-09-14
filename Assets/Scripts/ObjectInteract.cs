using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
	private StepManager stepManager;
	private ScoreManager scoreManager;
	private ColorController colorController;
	private TouchDragDrop touchDragDrop;

	private int checkStepOne = 2; // 0 olunca sonrakine geçebilir

	private void Start()
	{
		GameObject gameManagerObj = GameObject.Find("GameManager");
		stepManager = gameManagerObj.GetComponent<StepManager>();
		scoreManager = gameManagerObj.GetComponent<ScoreManager>();
		colorController = GameObject.Find("GameManager").GetComponent<ColorController>();
		touchDragDrop = this.GetComponent<TouchDragDrop>();
	}

	private void Update()
	{
		if(checkStepOne == 0)
		{
			stepManager.ChangeStep(StepManager.STEPS.Step2);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int currentStep = stepManager.CurrentStep;
		if(currentStep == 0)
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

					scoreManager.IncreasScore();
					bool flag = SearchPill();

					if(flag == true)
					{
						stepManager.ChangeStep(StepManager.STEPS.Step3);
						stepManager.CupActivate(true);
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
		else if (currentStep == 2)
		{
			if (touchDragDrop.dragging == false)
			{
				if (this.gameObject.name == other.gameObject.name)
				{
					// Işınla
					Debug.Log("Başarılı");
				}
				else
				{
					// puan düş yanlış de
				}
			}
		}
		else
		{
			// Şuan hiçbir stepte değil!
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


