using UnityEngine;
using TMPro;

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
