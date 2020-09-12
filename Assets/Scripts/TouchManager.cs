using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float zPosition;
    private Vector3 offSet;
    public Camera camera;
    bool dragging;

	private void Start()
	{
		zPosition = camera.WorldToScreenPoint(transform.position).z;
	}
}
