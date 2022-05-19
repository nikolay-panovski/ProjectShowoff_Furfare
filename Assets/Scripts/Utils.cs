using UnityEngine;

public static class Utils
{
    public static void incrementTimer(ref float timerVar)
    {
        timerVar += Time.deltaTime;
    }

    public static bool checkTimer(float timerVar, float maxTimeVar)
    {
        bool hasTimerElapsed = timerVar >= maxTimeVar;
        return hasTimerElapsed;
    }

    public static void resetTimer(ref float timerVar)
    {
        timerVar = 0.0f;
    }
}
