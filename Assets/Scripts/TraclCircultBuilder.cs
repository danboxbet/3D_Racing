using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TraclCircultBuilder
{
    public static TrackPoint[] Build(Transform trackTransform, TrackType type)
    {
        TrackPoint[] points = new TrackPoint[trackTransform.childCount];

        ResetPoint(trackTransform, points);

        MakeLinks(points, type);

        MarkPoint(points,type);

        return points;
    }

   
    private static void ResetPoint(Transform trackTransform,TrackPoint[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();
            if (points[i] == null)
            {
                Debug.LogError("There is ni TrackPoint script on one of the child object");
                return;
            }
            points[i].ResetValue();
        }
    }
    private static void MakeLinks(TrackPoint[] points, TrackType type)
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].Next = points[i + 1];
        }
        if (type == TrackType.Circular)
        {
            points[points.Length - 1].Next = points[0];
        }
    }
    private static void MarkPoint(TrackPoint[] points, TrackType type)
    {
        points[0].IsFirst = true;

        if (type == TrackType.Sprint)
            points[points.Length - 1].IsLast = true;

        if (type == TrackType.Circular)
            points[0].IsLast = true;
    }

}
