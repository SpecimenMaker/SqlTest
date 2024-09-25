using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBmanager 
{
    public static string username;
    public static int score;
    public static bool LoggIn { get { return username != null; } }

    public static void LoginOut()
    {
        username = null;
    }
}
