using UnityEngine;

public static class Utils
{
    public static void incrementTimer(float timerVar)
    {
        timerVar += Time.deltaTime;
    }

    public static bool checkTimer(float timerVar, float maxTimeVar)
    {
        bool hasTimerElapsed = timerVar >= maxTimeVar;
        return hasTimerElapsed;
    }
}
