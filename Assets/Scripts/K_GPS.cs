using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class K_GPS : MonoBehaviour
{
    public Text gpsOut1, gpsOut2, refresh, text_on;
    bool isUpdating = false;
    double latitude, longitude, tempLat, tempLon;
    int time;
    string button_on_text;
    public GameObject restart_GPS;
    public GameObject[] building_button; //0:샬롬관 1:인사관 2:우원관 3:예술관 4:중앙도서관 5:본관 6:후생관
                                         //7:경천관 8:이공관 9:천은관 10:교육관 11:승리관 12:목양관 13:기숙사
    bool[] toggle_button = new bool[14]; //초기값 false
    double[,] building_GPS = new double[15, 4] { { 37.27459530938347, 127.12982980198065, 37.27517584941466, 127.13025359101508 }, { 37.27481301242459, 127.13055936285483, 37.27552587874539, 127.13112799117953 }, //0, 1
                                                { 37.275329521040504, 127.13153032251977, 37.27612348603744, 127.13213113735341 }, { 37.27563259469132, 127.13074175304408, 37.27618751511025, 127.1313050169506 }, //2, 3
                                                { 37.276119217440396, 127.1320077557193, 37.276550345487216, 127.13267294357084 }, { 37.27597835328759, 127.13283387609867, 37.276187515119005, 127.13377264927625 }, //4, 5
                                                { 37.276622911360974, 127.13352052160674, 37.277036962306084, 127.13372436949673 }, { 37.27631130450856, 127.13349369951595, 37.27656741982412, 127.13433591316671 }, //6, 7
                                                { 37.27672535716766, 127.13365999647884, 37.277194898664476, 127.13458267640196 }, { 37.275346595649516, 127.13403550573571, 37.276033845252286, 127.1341642517715 }, //8, 9
                                                { 37.2753209837394, 127.13289288467605, 37.27537220754609, 127.13369754739972 }, { 37.27445017367331, 127.13225451894013, 37.2744800545771, 127.13264075704747 }, //10, 11
                                                { 37.27380559699946, 127.13198629803888, 37.27443309886123, 127.13213113732915 }, { 37.27785412876229, 127.13349354733563, 37.278485865349374, 127.13526380532761 } , //12, 13
                                                { 37.27801443604467, 127.12849350420384, 37.27819801063032, 127.12868368524481 } }; //0, 2은 latitude, 1, 3은 longitude

    private void Update()
    {
        if (!isUpdating) //GPS 가져오기
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
        for (int i = 0; i < 14; i++) //GPS 거리
        {
            if ((building_GPS[i, 0] <= latitude && building_GPS[i, 2] >= latitude)
            && (building_GPS[i, 1] <= longitude && building_GPS[i, 3] >= longitude))
            {
                if (!toggle_button[i])
                {
                    button_on_text += building_button[i].name + " 버튼 활성화"+ "\n";
                    toggle_button[i] = true;
                }
            }
            else toggle_button[i] = false;
        }

        /*//test
        if ((building_GPS[14, 0] <= latitude && building_GPS[14, 2] >= latitude)
            && (building_GPS[14, 1] <= longitude && building_GPS[14, 3] >= longitude))
        {
            if (!toggle_button[12])
            {
                button_on_text += building_button[12].name + " 버튼 활성화" + "\n";
                toggle_button[12] = true;
            }
        }
        else toggle_button[12] = false;*/

        for (int i = 0; i < 14; i++) //버튼 On/Off
        {
            if (toggle_button[i])
            {
                building_button[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                building_button[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                building_button[i].GetComponent<Image>().color = new Color(255, 255, 255, 50);
                building_button[i].GetComponent<Button>().interactable = false;
            }
        }
        text_on.text = button_on_text;
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
            gpsOut1.text = "";
            gpsOut2.text = "";
            refresh.text = "시간 초과";
            restart_GPS.SetActive(true);
            Input.location.Stop();
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            refresh.text = "Unable to determine device location";
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

                refresh.text = "Continue";
                gpsOut1.text = "Latitude: " + latitude; // + " " + Input.location.lastData.altitude + 100f + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
                gpsOut2.text = "Longitude: " + longitude;
                button_on_text = "";

                time = 0;
                // Access granted and location value could be retrieved
            }
            else refresh.text = "GPS 받아오는 중...(" + time + ")";
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        Input.location.Stop();
    }
    public void SceneToStart()
    {
        Input.location.Stop();
        SceneManager.LoadScene("StartScene");
    }
    public void Restart_GPS()
    {
        restart_GPS.SetActive(false);
        refresh.text = "다시 시작";
        isUpdating = !isUpdating;
        time = 0;
    }
}