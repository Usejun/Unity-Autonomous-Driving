using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class BluetoothConnection : MonoBehaviour
{
    BluetoothManagement bluetooth;

    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    TextMeshProUGUI selectedDevice;

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
        BluetoothService.CreateBluetoothObject();
    }

    public void Update()
    {
        if (bluetooth.IsConnected)
        {
            try
            {
                string recivedData = BluetoothService.ReadFromBluetooth();
                Log.AddLog(recivedData);
            }
            catch (Exception)
            {
            }
        }
    }

    public void Search()
    {
        Log.AddLog("Searching...");
        try
        {
            List<string> deviceNames = BluetoothService.GetBluetoothDevices().ToList();
            if (deviceNames == null)
                Log.AddLog("Null...");
            else if (deviceNames.Count == 0)
                Log.AddLog("Nothing...");
            else
            {
                dropdown.AddOptions(deviceNames);
                Log.AddLog(string.Join(" ", deviceNames));
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
        try
        {
            bluetooth.DeviceName = selectedDevice.text;
            if (BluetoothService.StartBluetoothConnection(bluetooth.DeviceName))
                Log.AddLog($"Successfully Connected to {bluetooth.DeviceName}");
        }
        catch (Exception)
        {
            Log.AddLog("Unable to Connect to the device");
        }
    }

    public void Run()
    {
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
