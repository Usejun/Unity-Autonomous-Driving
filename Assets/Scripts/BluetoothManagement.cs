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
        get => deviceName;
        set => deviceName = value;
    }

    bool isConnected;
    string deviceName;

    void Awake()
    {
        isConnected = false;
        deviceName = "Not Connected";
    }

}
