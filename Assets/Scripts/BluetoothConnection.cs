using TMPro;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.Android;

public class BluetoothConnection : MonoBehaviour
{
    BluetoothManagement bluetooth;
    SceneLoader sceneLoader;

    [SerializeField] TMP_Dropdown dropdown;

    [SerializeField] TextMeshProUGUI selectedDevice;

    public void Start()
    {
#if UNITY_2020_2_OR_NEWER && UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) ||
            !Permission.HasUserAuthorizedPermission(Permission.FineLocation) ||
            !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN") ||
            !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE") ||
            !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
                Permission.RequestUserPermissions(new string[] {
                                    Permission.CoarseLocation,
                                    Permission.FineLocation,
                                    "android.permission.BLUETOOTH_SCAN",
                                    "android.permission.BLUETOOTH_ADVERTISE",
                                    "android.permission.BLUETOOTH_CONNECT" });

#endif
        bluetooth = BluetoothManagement.Instance;
        sceneLoader = SceneLoader.Instance;
        BluetoothService.CreateBluetoothObject();
    }

    public void Search()
    {
        Log.AddLog("Searching...");
        try
        {
            string[] deviceNames = BluetoothService.GetBluetoothDevices();
            if (deviceNames == null)
                Log.AddLog("Null...");
            else if (deviceNames.Length == 0)
                Log.AddLog("Nothing...");
            else
            {
                dropdown.AddOptions(deviceNames.ToList());
                Log.AddLog(string.Join(", ", deviceNames));
            }
        }
        catch (Exception e)
        {
            Log.AddLog(e.Message);
            Log.AddLog(e.StackTrace);
        }
    }

    public void Connect()
    {
        Log.AddLog("Connecting...");
        try
        {
            bluetooth.Connect(selectedDevice.text);
        }
        catch
        {
            Log.AddLog("Unable to Connect to the device");
        }
    }

    public void Disconnect()
    {
        Log.AddLog($"Disconnecting...");
        try
        {
            bluetooth.Disconnect();
        }
        catch
        {
            Log.AddLog($"Can't Disconnect to {bluetooth.DeviceName}");
        }
    }

    public void Run()
    {
        Log.AddLog("Starting...");
        sceneLoader.Load("Control");
        Log.Clear();
        Log.MaxLogCount = 15;       
    }
}
