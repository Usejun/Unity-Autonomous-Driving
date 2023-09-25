using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;
using TMPro;
using System.Linq;

public class BluetoothManagement : Singleton<BluetoothManagement>
{
    public bool IsConnected => isConnected;
    public string DeviceName
    {
        get
        {
            if (deviceName == null || deviceName == "")            
                return "Not Connected";
            return deviceName;
        }
           
        set => deviceName = value;
    }

    bool isConnected;
    string deviceName;

    private void Awake()
    {
        isConnected = false;
        deviceName = "Not Connected";
    }

    private void Update()
    {
        if (IsConnected)
        {
            try
            {
                string recivedData = BluetoothService.ReadFromBluetooth();
                Log.AddLog(recivedData);
            }
            catch
            {
            }
        }
    }

    public bool Connect(string name)
    {
        deviceName = name;
        bool connectionCondition = BluetoothService.StartBluetoothConnection(deviceName);
        isConnected = connectionCondition;
        return connectionCondition;
    }

    public void Disconnect()
    {
        BluetoothService.StopBluetoothConnection();
        isConnected = false;
    }

    public void Send(string text)
    {
        BluetoothService.WritetoBluetooth(text);
    }

    public string Read()
    {
        return BluetoothService.ReadFromBluetooth();
    }

}
