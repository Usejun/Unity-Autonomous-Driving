using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionManagement : MonoBehaviour
{
    BluetoothManagement bluetooth;
    SceneLoader sceneLoader;

    [SerializeField]
    GameObject optionScreen;

    [SerializeField]
    GameObject log;

    [SerializeField]
    TextMeshProUGUI speedText;

    [SerializeField]
    Slider speedSlider;

    [SerializeField]
    TextMeshProUGUI deviceNameText;

    [SerializeField]
    NavigationMovement navMovement;

    [SerializeField]
    TMP_InputField sendMessage;

    private void Awake()
    {
        bluetooth = BluetoothManagement.Instance;
        sceneLoader = SceneLoader.Instance;

        deviceNameText.text = "연결된 디바이스 : " + bluetooth.DeviceName;
    }

    private void Update()
    {
        speedText.text = "현재 속도 \n" + Mathf.Round(navMovement.Speed * 100) / 100;
    }

    public void OptionOnOff()
    {
        navMovement.isMoving = optionScreen.activeSelf;
        optionScreen.SetActive(!optionScreen.activeSelf);

        speedSlider.value = navMovement.Speed;
    }

    public void LogOnOff()
    {
        log.SetActive(!log.activeSelf);
    }

    public void ChangeSpeedValue()
    {
        navMovement.SetSpeed(speedSlider.value);
    }

    public void BluetoothSend()
    {
        bluetooth.Send(sendMessage.text);
    }

    public void Back()
    {
        sceneLoader.Load("Connect");
        Log.Clear();
    }
    
}
