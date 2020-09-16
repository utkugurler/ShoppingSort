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
	private Rigidbody rb;
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
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		zPosition = camera.WorldToScreenPoint(transform.position).z;

		if (stepManager.CurrentStep == 0) // İlk stepte sadece UIlar hareket edeceği için şişelerin hareketini engelledim
		{
			// 0' da ui hareket ederek şişelere gider
			if (this.gameObject.tag == "Bottle")
			{
				dragging = false;
			}
		}
		else if(stepManager.CurrentStep == 1) // Sonraki stepteyse sadece ilaçlar hareket edecek
		{
			// Hapları şişelerin ucuna götürür
			if(this.gameObject.tag == "Bottle")
			{
				dragging = false;
			}
			else if(this.gameObject.tag == "Pill")
			{
				if (dragging == true)
				{
					zPosition = camera.WorldToScreenPoint(new Vector3(0, 0, 0)).z; // İlaçları şişelere denk getirecek şekilde getirdim.
					rb.isKinematic = true; // Kinematic yaptımki tutunca yerçekiminden dolayı bozulmasın
				}
				else
				{
					rb.isKinematic = false;
				}
			}
		}
		else if(stepManager.CurrentStep == 2)
		{
			
				//dragging = true;
				zPosition = camera.WorldToScreenPoint(new Vector3(0, 0, 49)).z;
			
		}

		if(dragging == true)
		{
			Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition);
			transform.position = camera.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
			
			if(stepManager.CurrentStep == 1)
				transform.rotation = new Quaternion(0, 90, 0, 0); // İlaçları tutunca düzgün bir şekilde gelmesi için rotasyonu oynadım.
		}
	}
	// Tutma kodları
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

	public bool GetDrag()
	{
		return dragging;
	}
}
