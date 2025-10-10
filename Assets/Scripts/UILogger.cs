using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

/// <summary>
/// UILogger allows you to log messages to a UI Text element in your Unity scene.
/// Attach this script to any GameObject and assign a Text or TMP_Text component.
/// </summary>
public class UILogger : MonoBehaviour
{
    [Header("Assign a UI Text component (e.g., Text or TMP_Text)")]
    public TMP_Text uiText;

    [Header("Maximum number of log lines to keep in UI")]
    public int maxLines = 30;

    private StringBuilder logBuilder = new StringBuilder();

    private static UILogger _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Application.logMessageReceived += HandleLog;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            Application.logMessageReceived -= HandleLog;
            _instance = null;
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        string color = type == LogType.Error || type == LogType.Exception ? "#FF5555" :
                       type == LogType.Warning ? "#FFCC00" : "#FFFFFF";
        string formatted = $"<color={color}>{logString}</color>";

        logBuilder.AppendLine(formatted);

        // Limit number of lines
        string[] lines = logBuilder.ToString().Split('\n');
        if (lines.Length > maxLines)
        {
            logBuilder = new StringBuilder();
            for (int i = lines.Length - maxLines; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                    logBuilder.AppendLine(lines[i]);
            }
        }

        if (uiText != null)
        {
            // For TMP_Text, rich text is always supported and enabled by default.
            uiText.text = logBuilder.ToString();
        }
    }

    /// <summary>
    /// Manually log a message to the UI.
    /// </summary>
    public static void Log(string message)
    {
        Debug.Log(message);
    }

    /// <summary>
    /// Manually log a warning to the UI.
    /// </summary>
    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    /// <summary>
    /// Manually log an error to the UI.
    /// </summary>
    public static void LogError(string message)
    {
        Debug.LogError(message);
    }
}