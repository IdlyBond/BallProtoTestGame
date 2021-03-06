using UnityEngine;
using System.Collections;
 
public class MyLog : MonoBehaviour
{
    string myLog;
    Queue myLogQueue = new Queue();

    public bool isDebug;
 
    void Start(){
        if (!isDebug)
        {
            gameObject.SetActive(false);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        Debug.Log("Log1");
        Debug.Log("Log2");
        Debug.Log("Log3");
        Debug.Log("Log4");
    }
 
    void OnEnable () {
        Application.logMessageReceived += HandleLog;
    }
     
    void OnDisable () {
        Application.logMessageReceived -= HandleLog;
    }
 
    void HandleLog(string logString, string stackTrace, LogType type){
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach(string mylog in myLogQueue){
            myLog += mylog;
        }
    }
 
    void OnGUI () {
        GUILayout.Label(myLog);
    }
}