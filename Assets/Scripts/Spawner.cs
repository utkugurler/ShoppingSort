using System.Collections;
using System.Collections.Generic;
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
			if (obj.Length >= 10)
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
		waitFlag = false;
		int rnd = Random.Range(0, pills.Length);
		Instantiate(pills[rnd], SpawnTransform.position, Quaternion.identity);
		yield return new WaitForSeconds(2);
		waitFlag = true;
	}

     
}
