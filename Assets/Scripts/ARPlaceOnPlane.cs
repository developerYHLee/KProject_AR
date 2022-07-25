using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject robot, sheep, marker_objects;
    bool isPut, isTouched;
    public static bool isChoose;
    [SerializeField] private Camera arCamera;
    private void OnEnable()
    {
        Debug.Log("isPut : " + isPut + " isChoose : " + isChoose);
        Debug.Log(arRaycaster.GetComponent<Remove_ARMarkerImage>().enabled);
        Debug.Log(arRaycaster.GetComponent<ARPointCloudManager>().enabled);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isChoose)
        {
            arRaycaster.GetComponent<ARTrackedImageManager>().enabled = false;
            arRaycaster.GetComponent<Remove_ARMarkerImage>().enabled = false;
        }
        else
        {
            arRaycaster.GetComponent<ARPointCloudManager>().enabled = false;
            arRaycaster.GetComponent<ARPlaneManager>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChoose) SelectObject();
        else if(!isPut) UpdateCenterObject();
    }
    private void SelectObject()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray;
                RaycastHit hitObj;

                ray = arCamera.ScreenPointToRay(touch.position);

                if(Physics.Raycast(ray, out hitObj))
                {
                    if (hitObj.collider.name.Contains("sheep_Marker"))
                    {
                        arRaycaster.raycastPrefab = sheep;
                        isTouched = true;
                    }
                    else if (hitObj.collider.name.Contains("Robot_Marker"))
                    {
                        arRaycaster.raycastPrefab = robot;
                        isTouched = true;
                    }
                }
            }
            if(touch.phase == TouchPhase.Ended && isTouched)
            {
                marker_objects.SetActive(false);
                arRaycaster.GetComponent<ARPointCloudManager>().enabled = true;
                arRaycaster.GetComponent<ARPlaneManager>().enabled = true;
                arRaycaster.GetComponent<ARTrackedImageManager>().enabled = false;
                arRaycaster.GetComponent<Remove_ARMarkerImage>().enabled = false;
                isChoose = true;
            }
        }
    }
    public void UpdateCenterObject()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (arRaycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(arRaycaster.raycastPrefab, hitPose.position, hitPose.rotation);
                isPut = true;
                arRaycaster.GetComponent<ARPointCloudManager>().enabled = false;
                arRaycaster.GetComponent<ARPlaneManager>().enabled = false;
                arRaycaster.GetComponent<ARRaycastManager>().enabled = false;
            }
        }
    }
}