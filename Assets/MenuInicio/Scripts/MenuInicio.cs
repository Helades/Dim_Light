using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    public AudioMixer mainAudio;
    public CanvasGroup opcionesCG, inicioCG;
    public GameObject opcionesGO, inicioGO;
    //public Dropdown resolutionDropdown;

    //Resolution[] resolutions;

    private bool options, main, activeOptions, activeMain;

    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    MenuInicio()
    {
        options = false;
        main = true;
        activeOptions = false;
        activeMain = true;
    }

    public void Start ()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /*
                resolutions = Screen.resolutions;
                resolutionDropdown.ClearOptions();
                List<string> options = new List<string>();

                int currentResolutionIndex = 0;

                for (int i = 0; i < resolutions.Length; i++)
                {
                    string option = resolutions[i].width + " x " + resolutions[i].height;
                    options.Add(option);

                    if (resolutions[i].width == Screen.currentResolution.width &&
                        resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = i;
                    }
                }

                resolutionDropdown.AddOptions(options);
                resolutionDropdown.value = currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
            */
    }
    
    public void Update()
    {
        if (options)
        {
            if (inicioCG.alpha > 0 && activeMain)
            {
                inicioCG.alpha -= Time.deltaTime;

                if (inicioCG.alpha <= 0 && activeMain)
                {
                    activeMain = false;
                    activeOptions = true;
                    inicioGO.SetActive(false);
                    opcionesGO.SetActive(true);
                }
            }
            else if (inicioCG.alpha < 1)
            {
                opcionesCG.alpha += Time.deltaTime;
            }  
        }
        else if (main)
        {  
            if (opcionesCG.alpha > 0 && activeOptions)
            {
                opcionesCG.alpha -= Time.deltaTime;

                if (opcionesCG.alpha <= 0 && activeOptions)
                {
                    activeMain = true;
                    activeOptions = false;
                    opcionesGO.SetActive(false);
                    inicioGO.SetActive(true);
                }
            }
            else if (inicioCG.alpha < 1)
            {
                inicioCG.alpha += Time.deltaTime;
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MenuCarga1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EnterOptions()
    {
        options = true;
        main = false;
    }

    public void QuitOptions()
    {
        main = true;
        options = false;
    }

    public void SetVolume(float volume)
    {
        mainAudio.SetFloat("MainVolume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (int indexScreen)
    {
        if (indexScreen == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (indexScreen == 2)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (indexScreen == 3)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
    }
    /*
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    */
}