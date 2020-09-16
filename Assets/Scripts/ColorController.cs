using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public enum COLOR
	{
		GREEN,
		PINK,
		ORANGE
	}

	public Material[] materials;

	// Gelen renge göre materyali döndürüyor
	public Material GetMaterial(COLOR color)
	{
		if(color == COLOR.GREEN)
		{
			return materials[0];
		}
		else if (color == COLOR.ORANGE)
		{
			return materials[1];
		}
		else if (color == COLOR.PINK)
		{
			return materials[2];
		}
		else
		{
			return materials[0];
		}
	}
}
