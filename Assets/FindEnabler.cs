using UnityEngine;
using System.Diagnostics;

public class FindEnabler : MonoBehaviour
{
    void OnEnable()
    {
        UnityEngine.Debug.LogWarning($"[FindEnabler] '{name}' was ENABLED!", this);
        FindSetActiveCaller();
    }

    void OnDisable()
    {
        UnityEngine.Debug.LogWarning($"[FindEnabler] '{name}' was DISABLED!", this);
    }

    void FindSetActiveCaller()
    {
        try
        {
            var stack = new StackTrace(true);
            var frames = stack.GetFrames();

            if (frames == null) return;

            UnityEngine.Debug.Log("=== Looking for SetActive caller ===");

            for (int i = 0; i < frames.Length; i++)
            {
                var frame = frames[i];
                var method = frame.GetMethod();
                var declaringType = method.DeclaringType;

                if (declaringType != null)
                {
                    string methodName = method.Name;
                    string typeName = declaringType.Name;
                    string fileName = frame.GetFileName();
                    int lineNumber = frame.GetFileLineNumber();

                    // Log every frame to see the complete call stack
                    UnityEngine.Debug.Log($"Frame {i}: {typeName}.{methodName}() at {fileName}:{lineNumber}");

                    // Look for SetActive specifically
                    if (methodName.Contains("SetActive") ||
                        (declaringType.Namespace != null &&
                         !declaringType.Namespace.StartsWith("UnityEngine") &&
                         !declaringType.Namespace.StartsWith("System") &&
                         typeName != "FindEnabler"))
                    {
                        UnityEngine.Debug.LogWarning($"🚨 POTENTIAL CULPRIT: {typeName}.{methodName}() at {fileName}:{lineNumber}", this);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"[FindEnabler] Error: {e.Message}", this);
        }
    }
}