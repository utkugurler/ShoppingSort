using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchDragDrop : MonoBehaviour
{
	private StepManager stepManager;

    private float zPosition;
    private Vector3 offset;
    private Camera camera;
    private bool dragging = false; // TODO: Kategoriye göre draggable kapama açmaları kontrol edeceğiz!

	[Space]
	[SerializeField]
	public UnityEvent OnBeginDrag;
	public UnityEvent OnEndDrag;

	public float zDistance;

	private void Start()
	{
		stepManager = GameObject.Find("GameManager").GetComponent<StepManager>();

		dragging = false;
		camera = Camera.main;
		zPosition = camera.WorldToScreenPoint(transform.position).z;
	}

	private void Update()
	{
		zPosition = camera.WorldToScreenPoint(transform.position).z;

		if (stepManager.CurrentStep == 0)
		{
			// 0' da ui hareket ederek şişelere gider
			if (this.gameObject.tag == "Bottle")
			{
				dragging = false;
			}
		}
		else if(stepManager.CurrentStep == 1)
		{
			// Hapları şişelerin ucuna götürür
			if(this.gameObject.tag == "Bottle")
			{
				dragging = false;
			}
			
		}
		else if(stepManager.CurrentStep == 2)
		{

		}

		if(dragging == true)
		{
			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition);
			transform.position = camera.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
		}
	}

	private void OnMouseDown()
	{
		if(dragging == false)
		{
			BeginDrag();
		}
	}

	private void OnMouseUp()
	{
		EndDrag();
	}

	public void BeginDrag()
	{
		OnBeginDrag.Invoke();
		dragging = true;
		offset = camera.WorldToScreenPoint(transform.position) - Input.mousePosition;
	}

	public void EndDrag()
	{
		OnEndDrag.Invoke();
		dragging = false;
	}
}
