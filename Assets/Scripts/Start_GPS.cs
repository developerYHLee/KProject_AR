using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_GPS : MonoBehaviour
{
    public Text time_GPS;
    int time;
    bool isUpdating = false, activated;
    double latitude, longitude, tempLat, tempLon;
    public GameObject kangnamUniversity, restart_GPS;
    private double[] location = new double[4] { 37.272811643876416, 127.12797315826626, 37.273238522095966, 127.1292928050516 }, 
        location_room = new double[4] { 37.27785517247602, 127.12846395894812, 37.27813155371521, 127.12861921771436 }; //0 머리띠, 1 방
    // Start is called before the first frame update
    void Start()
    {
        kangnamUniversity.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
        if (!activated)
        {
            if ((location[0] <= latitude && location[2] >= latitude && location[1] <= longitude && location[3] >= longitude) ||
                (location_room[0] <= latitude && location_room[2] >= latitude && location_room[1] <= longitude && location_room[3] >= longitude))
            {
                kangnamUniversity.SetActive(true);
                activated = true;
            }
            else kangnamUniversity.SetActive(false);
        }
    }
    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        while (Input.location.status == LocationServiceStatus.Initializing && time < 20)
        {
            time++;
            yield return new WaitForSeconds(1);
        }

        // Service didn't initialize in 20 seconds
        if (time >= 20)
        {
            time_GPS.text = "시간 초과";
            restart_GPS.SetActive(true);
            Input.location.Stop();
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            time_GPS.text = "Unable to determine device location";
            Input.location.Stop();
            yield break;
        }
        else
        {
            latitude = (double)Input.location.lastData.latitude;
            longitude = (double)Input.location.lastData.longitude;
            
            if (tempLat != latitude || tempLon != longitude)
            {
                tempLat = latitude; tempLon = longitude;
                time_GPS.text = "Continue";
                time = 0;
            }
            else time_GPS.text = "GPS 받아오는 중...(" + time + ")";
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        Input.location.Stop();
    }

    public void Yes()
    {
        ARPlaceOnPlane.isChoose = true;
        Input.location.Stop();
        SceneManager.LoadScene("ARScene");
    }
    public void No()
    {
        kangnamUniversity.SetActive(false);
    }
    public void SceneToAR()
    {
        ARPlaceOnPlane.isChoose = false;
        Input.location.Stop();
        SceneManager.LoadScene("ARScene");
    }
    public void Quit()
    {
        Input.location.Stop();
        Application.Quit();
    }
    public void Restart_GPS()
    {
        restart_GPS.SetActive(false);
        time_GPS.text = "다시 시작";
        isUpdating = !isUpdating;
        time = 0;
    }
}