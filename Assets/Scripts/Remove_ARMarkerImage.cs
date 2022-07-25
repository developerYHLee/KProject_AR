using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class Remove_ARMarkerImage : MonoBehaviour
{
    public Text testText;
    public ARTrackedImageManager trackedImageManager;
    public GameObject robot, sheep, ARObject_Manager;
    
    private void OnEnable()
    {
        testText.text = " " + trackedImageManager.GetComponent<Remove_ARMarkerImage>().enabled + " " + ARObject_Manager.activeSelf;
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        testText.text = " " + trackedImageManager.GetComponent<Remove_ARMarkerImage>().enabled + " " + ARObject_Manager.activeSelf;
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
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            robot.SetActive(false);
            sheep.SetActive(false);
            ARObject_Manager.transform.position = trackedImage.transform.position;
            ARObject_Manager.transform.rotation = trackedImage.transform.rotation;
        }
    }
}
