using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public static class Log
{
    static Queue<LogMessage> logs = new Queue<LogMessage>();

    public static int MaxLogCount = 30;
    public static int Count => logs.Count;

    public static LogMessage GetLog()
    {
        return logs.Dequeue();
    }

    public static void AddLog(string message)
    {
        LogMessage msg = new LogMessage(message);
        logs.Enqueue(msg);
        if (logs.Count >= MaxLogCount)        
            GetLog();        
    }

    public static string Write()
    {
        return string.Join("\n", logs.Select(x => x.Message));
    }
    
    public static void Clear()
    {
        logs.Clear();
    }
}

public class LogMessage
{
    public string Message => message;

    string message;

    public LogMessage(string message) 
    { 
        this.message = message;
    }
}
