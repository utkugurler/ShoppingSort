using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform SpawnTransform;
    public GameObject[] pills;
	
	private void Update()
	{
		if (CalculatePills() == true)
		{
			// Yeni spawn etme
		}
		else
		{
			// Spawn et
			GameObject obj = Spawn();
		}
	}

	// Ekranda ne kadar ilaç var hesaplamasını yapıyor eksikse spawn edicek bir tane daha
	private bool CalculatePills()
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Pill");
		if(obj.Length >= 10)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void SpawnPills()
	{

	}

    public GameObject Spawn()
	{
		int rnd = Random.Range(0, pills.Length);
		return Instantiate(pills[rnd], SpawnTransform.position, Quaternion.identity);
	}

     
}
