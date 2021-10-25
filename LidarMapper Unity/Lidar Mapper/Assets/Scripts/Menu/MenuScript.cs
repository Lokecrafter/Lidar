using System;
using UnityEngine;
using LidarProject;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuContainer;
    [SerializeField] private TMP_InputField portNameField;
    [SerializeField] private TMP_InputField baudrateField;
    [SerializeField] private Slider speedSlider;

    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    private void Awake()
    {
        portNameField.text = PlayerPrefs.GetString("portName");
        baudrateField.text = PlayerPrefs.GetString("baud");
        speedSlider.value = PlayerPrefs.GetFloat("speed");
    }

    public void ConnectPort()
    {
        UpdateSettings();

        string portName = portNameField.text;
        int baudrate = int.Parse(baudrateField.text);

        LidarHandler.singleton.OpenNewSerialPort(portName, baudrate);
    }
    public void ToggleSettings(GameObject settingsBox)
    {
        if (settingsBox.activeSelf)
        {
            settingsBox.SetActive(false);
        }
        else
        {
            settingsBox.SetActive(true);
        }
    }
    public void UpdateSettings()
    {
        string portName = portNameField.text;
        string baudrate = baudrateField.text;
        float speed = speedSlider.value;

        PlayerPrefs.SetString("portName", portName);
        PlayerPrefs.SetString("baud", baudrate);
        PlayerPrefs.SetFloat("speed", speed);
    }

    public void QuitApp()
    {
        ArduinoComm.arduino.Port.Close();
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            if (menuContainer.activeSelf)
            {
                menuContainer.SetActive(false);
            }
            else
            {
                menuContainer.SetActive(true);
            }
        }
    }
}
