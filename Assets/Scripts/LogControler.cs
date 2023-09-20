using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LogControler : MonoBehaviour
{
    TextMeshProUGUI logText;

    List<LogMessage> logs = new List<LogMessage>();

    private void Awake()
    {        
        logText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        LogUpdate();
    }

    void LogUpdate()
    {
        while (Log.Count > 0)
        {
            logs.Add(Log.GetLog());
            Write();
        }
    }
  
    void Write()
    {
        logText.text = string.Join("\n", logs.Select(x => x.Message));
        while (logs.Count >=  30)
            logs.RemoveAt(0);
    }
}
