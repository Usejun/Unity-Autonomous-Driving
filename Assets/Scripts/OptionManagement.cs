using UnityEngine;
using TMPro;

public class OptionManagement : MonoBehaviour
{
    BluetoothManagement bluetooth;
    SceneLoader sceneLoader;

    [SerializeField] GameObject optionScreen;
    [SerializeField] GameObject log;

    [SerializeField] TextMeshProUGUI deviceNameText;

    [SerializeField] TMP_InputField sendMessage;

    [SerializeField] GridMovement gridMovement;

    void Awake()
    {
        bluetooth = BluetoothManagement.Instance;
        sceneLoader = SceneLoader.Instance;

        deviceNameText.text = "연결된 디바이스 : " + bluetooth.DeviceName;
    }

    public void OptionOnOff()
    {
        gridMovement.isActive = optionScreen.activeSelf;
        optionScreen.SetActive(!optionScreen.activeSelf);
    }

    public void LogOnOff()
    {
        log.SetActive(!log.activeSelf);
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

    public void Remote()
    {
        sceneLoader.Load("Remote");
        Log.Clear();
    }
    
}
