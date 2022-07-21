using UnityEngine;
using UnityEngine.UI;

public class Button_Manager : MonoBehaviour
{
    public GameObject clicked, imformation;
    public Sprite[] K_Building_Image;
    public Text name_Clicked, imformation_Next, imformation_Text;
    bool toggle, option_toggle = true, clicked_imformation_Next;
    GameObject field, menu;
    Slider slider;
    static float Button_Manager_score = 0;
    float plus = 100.0f / 14;
    bool[] toggle_button = new bool[14]; //초기값 false
    int index;

    private void Start()
    {
        field = GameObject.Find("Option").transform.Find("InputField").gameObject;
        menu = GameObject.Find("Option").transform.Find("Menu").gameObject;
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        if (Button_Manager_score == 0) slider.transform.Find("Fill Area").gameObject.SetActive(false);
        else slider.value = Button_Manager_score;
    }
    public void Menu()
    {
        toggle = field.activeSelf;
        field.SetActive(!field.activeSelf);

        if (!toggle) menu.SetActive(false);
        else menu.SetActive(true);

        toggle = !toggle;
    }
    public void Option()
    {
        if (option_toggle)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
            field.SetActive(false);
        }
        option_toggle = !option_toggle;
    }
    public void K_Building(int i)
    {
        index = i;

        if (!toggle_button[index])
        {
            slider.transform.Find("Fill Area").gameObject.SetActive(true);
            slider.value += plus;
            Button_Manager_score += plus;
            toggle_button[index] = true;
        }

        clicked.SetActive(true);
        clicked.GetComponent<Image>().sprite = K_Building_Image[index];
        //0:샬롬관 1:인사관 2:우원관 3:예술관 4:중앙도서관 5:본관 6:후생관
        //7:경천관 8:이공관 9:천은관 10:교육관 11:승리관 12:목양관 13:기숙사
        switch (index)
        {
            case 0 :
                name_Clicked.text = "샬롬관 정보";
                break;
            case 1:
                name_Clicked.text = "인사관 정보";
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
        }
    }
    public void Clicked_Imformation()
    {
        clicked.SetActive(false);
        imformation.SetActive(true);
        Write_Text();
    }
    public void Write_Text()
    {
        if(!clicked_imformation_Next)
        {
            imformation_Next.text = ">";
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    imformation_Text.text = "인문사회관\n복지융합대학 경영관리대학\n\n사용하는 학과\n" +
                        "사회복지학부 융복합자유전공학부 글로벌 경영학부 공공인재학과 경제세무학과";
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
            }
        }
        else
        {
            imformation_Next.text = "<";
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    imformation_Text.text = "총학생회실, 복융대 학생회실, 부동산건설학부 학생회실, 실버산업학과 학생회실," + 
                        "공공인재학과 학생회실, 글로벌 경영학부 학생회실, 경제 세무학과 학생회실 교수 학습 지원센터," + 
                        "진로 취창업 센터, 장학복지팀, 동아리 연합회 회의실, 문구점/서점, 중앙 동아리들의 동아리 실이 위치해 있다.";
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
            }
        }

        clicked_imformation_Next = !clicked_imformation_Next;
    }
    public void Quit_Imformation()
    {
        clicked_imformation_Next = false;
        imformation_Text.text = "";
        imformation.SetActive(false);
    }
}