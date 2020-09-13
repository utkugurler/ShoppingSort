using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
	private StepManager stepManager;
	private ScoreManager scoreManager;
	private ColorController colorController;

	private int checkStepOne = 2; // 0 olunca sonrakine geçebilir

	private void Start()
	{
		GameObject gameManagerObj = GameObject.Find("GameManager");
		stepManager = gameManagerObj.GetComponent<StepManager>();
		scoreManager = gameManagerObj.GetComponent<ScoreManager>();
		colorController = GameObject.Find("GameManager").GetComponent<ColorController>();
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
			// İlk stepte yapılacaklar
			if (other.gameObject.name == this.gameObject.name) // İsimle tagler birse puan artar
			{
				scoreManager.IncreasScore();
				stepManager.CheckStep(StepManager.STEPS.Step1);
				if (other.gameObject.name == "Orange")
				{
					for (int i = 0; i < other.transform.childCount; i++)
					{
						if(other.transform.GetChild(i).name == "bottle")
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
				}
				else
				{
					Destroy(this.gameObject);
				}
			}
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
