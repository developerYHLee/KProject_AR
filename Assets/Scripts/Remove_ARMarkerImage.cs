using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Remove_ARMarkerImage : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject robot, sheep, ARObject_Manager;
    
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            robot.SetActive(true);
            sheep.SetActive(true);
            ARObject_Manager.transform.position = trackedImage.transform.position;
            ARObject_Manager.transform.rotation = trackedImage.transform.rotation;
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            robot.SetActive(true);
            sheep.SetActive(true);
            ARObject_Manager.transform.position = trackedImage.transform.position;
            ARObject_Manager.transform.rotation = trackedImage.transform.rotation;
        }
    }
}
