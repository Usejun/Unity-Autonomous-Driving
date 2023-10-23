using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManagement : MonoBehaviour
{
    BluetoothManagement bluetooth;

    [SerializeField] GameObject optionScreen;
    [SerializeField] GameObject log;

    [SerializeField] TextMeshProUGUI deviceNameText;

    [SerializeField] TMP_InputField sendMessage;

    [SerializeField] GridMovement gridMovement;

    void Awake()
    {
        bluetooth = BluetoothManagement.Instance;

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
        SceneManager.LoadScene("Connect");
        Log.Clear();
    }

    public void Remote()
    {
        SceneManager.LoadScene("Remote");
        Log.Clear();
    }
    
}
