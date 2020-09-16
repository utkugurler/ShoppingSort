using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private StepManager stepManager;
    
    private Transform currentView;
    private Camera camera;

    public Transform[] views;
    public float transitionSpeed;

    // Use this for initialization
    void Start()
    {
        stepManager = GameObject.Find("GameManager").GetComponent<StepManager>();
        camera = Camera.main;
    }

    void Update()
    {
        int currentStep = stepManager.CurrentStep; // Şu anki stepi çekip ona göre kamerayı konumlandırıyor
        if (currentStep == -1)
        {
            currentView = views[0];
        }
        else if (currentStep == 0)
        {
            currentView = views[1];
        }
        else if (currentStep == 1)
        {
            currentView = views[2];
        }
        else if (currentStep == 2)
		{
            currentView = views[3];
		}
        else
        {

        }
    }

    void LateUpdate()
    {
        //Lerp position
        camera.transform.position = Vector3.Lerp(camera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
         Mathf.LerpAngle(camera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(camera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
         Mathf.LerpAngle(camera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        camera.transform.eulerAngles = currentAngle;
    }
}
