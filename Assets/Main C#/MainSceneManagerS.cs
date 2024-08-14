using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class MainSceneManagerS : MonoBehaviour
{
    public CinemachineVirtualCamera I_Cam;
    bool Is_I_Cam = false;

    public bool CanControlButton = false;

    public GameObject Light1, Light2;
    public GameObject Button1, Button2, Button3;
    bool ButtonSee1 = false, ButtonSee2 = false, ButtonSee3 = false;

    bool TurnOffLight = false;
    float TimeD = 0;
    float ButtonTime = 0;
    float LightValue = 1;
    private void Start()
    {

    }

    private void Update()
    {


        ButtonSee();
        LightAdministrator();
    }

    public void I_Button()
    {
        if (CanControlButton)
        {
            Is_I_Cam = !Is_I_Cam;
            I_Cam.Priority = Is_I_Cam ? 20 : 0;
        }
        
    }
    public void StartGame()
    {
        if (CanControlButton)
        {
            CanControlButton = false;
            Button1.GetComponent<MainSceneButtonS>().SetButton(false);
            Button2.GetComponent<MainSceneButtonS>().SetButton(false);
            Button3.GetComponent<MainSceneButtonS>().SetButton(false);

            TurnOffLight = true;
        }

    }

    public void ExitButton()
    {
        if (CanControlButton)
        {
            CanControlButton = false;
            Quit();
        }
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play모드를 false로.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //구글웹으로 전환
#else
        Application.Quit(); //어플리케이션 종료
#endif
    }






    void ButtonSee()
    {
        ButtonTime += Time.deltaTime;
        if (ButtonTime > 5.0f)
        {
            if (!ButtonSee1)
            {
                ButtonSee1 = true;
                Button1.GetComponent<MainSceneButtonS>().SetButton(true);
            }

        }
        if (ButtonTime > 5.5f)
        {
            if (!ButtonSee2)
            {
                Debug.Log("Ddd");
                ButtonSee2 = true;
                Button2.GetComponent<MainSceneButtonS>().SetButton(true);
            }

        }
        if (ButtonTime > 6.0f)
        {
            if (!ButtonSee3)
            {
                ButtonSee3 = true;
                Button3.GetComponent<MainSceneButtonS>().SetButton(true);

            }

        }
        if (!CanControlButton && ButtonTime > 7.0f)
        {
            CanControlButton = true;
        }
    }

    void LightAdministrator()
    {
        if (TurnOffLight)
        {
            TimeD += Time.deltaTime;
            if (TimeD < 0.2f)
            {
                LightValue = 1 + TimeD * 1f;
            }
            else if (TimeD < 3.2f)
            {
                LightValue = 1.2f - (TimeD - 0.2f) / 2;
            }
            else
            {
                TurnOffLight = false;
                SceneManager.LoadScene("GameScene");
            }



            Light1.GetComponent<Light>().intensity = LightValue * 1.2f;
            Light2.GetComponent<Light>().intensity = LightValue * 4f;
        }
    }

}
