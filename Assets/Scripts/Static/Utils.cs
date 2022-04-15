using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    public const double TAU = 6.2831853071795862;

    public static bool IsInLayerMask(GameObject obj, LayerMask layer) => ((layer.value & (1 << obj.layer)) > 0);

    public static float PLerp(float v0, float v1, float t) => (1 - t) * v0 + t * v1;

    public static float ILerp(float v0, float v1, float t) => v0 + t * (v1 - v0);

    public static float Remap(float aValue, float aIn1, float aIn2, float aOut1, float aOut2)
    {
        var t = (aValue - aIn1) / (aIn2 - aIn1);
        if (t > 1f)
            return aOut2;
        if (t < 0f)
            return aOut1;
        return aOut1 + (aOut2 - aOut1) * t;
    }

    public static void LoadLevel(int level) => SceneManager.LoadScene(level);

    public static void LoadLevel(string level) => SceneManager.LoadScene(level);

    public static HashSet<T> Find<T>()
    {
        var l = new HashSet<T>();
        var t = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<T>();
        foreach (T i in t)
            l.Add(i);
        return l;
    }

    public static List<T> FindAsList<T>()
    {
        var l = new List<T>();
        var t = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<T>();
        foreach (T i in t)
            l.Add(i);
        return l;
    }

    public static void PlayAtSourceVolumePitchRange(this AudioSource source, AudioClip clip, float minVol = 1, float maxVol = 1, float minPitch = 1, float maxPitch = 1)
    {
        if (source == null || clip == null) return;
        var v = Random.Range(Mathf.Min(minVol, maxVol), Mathf.Max(minVol, maxVol));
        var p = Random.Range(Mathf.Min(minPitch, maxPitch), Mathf.Max(minPitch, maxPitch));
        source.volume = Mathf.Clamp01(v);
        source.pitch = Mathf.Clamp(p, -3, 3);
        source.PlayOneShot(clip);
    }

    public static void DrawCircle(this LineRenderer line, float radius, float lineWidth = .1f)
    {
        if (line == null) return;
        var segments = 360;
        var points = new Vector3[segments];

        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = line.transform.localPosition + new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.loop = true;
        line.SetPositions(points);
    }

    public static void DrawEllipse(this LineRenderer line, float majorAxisWidth, float minorAxisWidth, float lineWidth = .1f)
    {
        if (line == null) return;
        var segments = 360;
        var points = new Vector3[segments];

        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = line.transform.localPosition + new Vector3(Mathf.Sin(rad) * majorAxisWidth, 0, Mathf.Cos(rad) * minorAxisWidth);
        }

        line.loop = true;
        line.SetPositions(points);
    }
}
