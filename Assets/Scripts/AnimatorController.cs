using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
	float lerpTime = 2;
	public Transform[] transforms;
	public GameObject bottlesObject;
	private bool fire = false;
	int posIndex = 0;
	int length;
	float t = 0f;

	private void Start()
	{
		// bottlesObject = GameObject.Find("Bottles");
		length = transforms.Length;
	}

	public void ChangeBottleTransform()
	{
		fire = true;
	}

	private void Update()
	{
		if(fire == true)
		{
			if(posIndex != 1)
				lerpTime = 2;

			float a = Mathf.Round(bottlesObject.transform.position.x);
			bottlesObject.transform.position = Vector3.Lerp(bottlesObject.transform.position, transforms[posIndex].position, lerpTime * Time.deltaTime);
			t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
			if(t > .9f)
			{
				t = 0f;
			
				
				posIndex++;
				if (posIndex == 1)
				{
					lerpTime = 1000;
				}
				if (posIndex == 3)
				{
					fire = false;
				}
				posIndex = (posIndex >= length) ? 0 : posIndex;
			}
			// transform.position += transforms[0].position * 2 * Time.deltaTime;

		}
	}
}
