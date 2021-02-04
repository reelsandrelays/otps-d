using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticMethods
{
    public static string GetLeftTimeText(long deadline)
    {
        TimeSpan leftTime = DateTime.FromBinary(deadline) - DateTime.Now;
        string text = "";

        if (leftTime.Hours > 0) text += string.Format("{0}h", leftTime.Hours);
        if (leftTime.Minutes > 0) text += string.Format(" {0}m", leftTime.Minutes);
        if (leftTime.Seconds > 0) text += string.Format(" {0}s", leftTime.Seconds);

        return text;
    }

    public static float GetElapsedTimeRate(long start, long end)
    {
        float totalLength = end - start;
        float elapsedLength = DateTime.Now.ToBinary() - start;

        return elapsedLength / totalLength;
    }

    public static bool TimeUp(long deadline) { return DateTime.Now.ToBinary() > deadline; }

    //public string 
    //string.Join("", flowerName.Split(' '));
}
