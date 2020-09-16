using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	private StepManager stepManager;

	public Transform SpawnTransform;
    public GameObject[] pills;
	private bool waitFlag = true;

	private void Start()
	{
		stepManager = GameObject.Find("GameManager").GetComponent<StepManager>();
	}

	private void Update()
	{
		if(stepManager.CurrentStep == 1)
		{
			if (CalculatePills() == true)
			{
				// Yeni spawn etme
			}
			else
			{
				// Spawn et
				if(waitFlag == true)
				{
					StartCoroutine(Spawn());
				}
			}
		}
	}

	// Ekranda ne kadar ilaç var hesaplamasını yapıyor eksikse spawn edicek bir tane daha
	private bool CalculatePills()
	{
		try
		{
			GameObject[] obj = GameObject.FindGameObjectsWithTag("Pill");
			
			if (obj.Length >= 12)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		catch(System.Exception ex)
		{
			Debug.LogError(ex.ToString());
			return false;
		}
		return true;
		
	}

	void SpawnPills()
	{

	}

    public IEnumerator Spawn()
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Pill");
		
		waitFlag = false;
		// int rnd = Random.Range(0, pills.Length);

		GameObject pillObj = tespitEt();
		if (pillObj != null)
			Instantiate(pillObj, SpawnTransform.position, Quaternion.identity);
		yield return new WaitForSeconds(1);
		waitFlag = true;
	}

	private GameObject tespitEt()
	{
		List<GameObject> obj = new List<GameObject>();
		foreach (var item in GameObject.FindGameObjectsWithTag("Pill"))
		{
			obj.Add(item);
		}
		foreach (var item in GameObject.FindGameObjectsWithTag("SuccessPill"))
		{
			obj.Add(item);
		}

		int pink = 0, green = 0, orange = 0;
		for (int i = 0; i < obj.Count; i++)
		{
			if (obj[i].name.Contains("Pink") == true)
			{
				pink++;
			}
			else if (obj[i].name.Contains("Green") == true)
			{
				green++;
			}
			else if (obj[i].name.Contains("Orange") == true)
			{
				orange++;
			}
		}

		if (pink < 4)
		{
			return pills[0];
		}
		else if (orange < 4)
		{
			return pills[2];
		}
		else if (green < 4)
		{
			return pills[1];
		}
		else
		{
			return null;
		}
	}


}
