using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LogControler : MonoBehaviour
{
    TextMeshProUGUI logText;

    private void Awake()
    {        
        logText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        logText.text = Log.Write();
    }
}
