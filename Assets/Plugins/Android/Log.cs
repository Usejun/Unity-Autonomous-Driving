using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Log
{
    static Queue<LogMessage> logs = new Queue<LogMessage>();

    public static int Count => logs.Count;

    public static LogMessage GetLog()
    {
        return logs.Dequeue();
    }

    public static void AddLog(string message)
    {
        LogMessage msg = new LogMessage(message);
        logs.Enqueue(msg);
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
