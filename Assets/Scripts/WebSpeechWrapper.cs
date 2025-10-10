using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebSpeechWrapper : MonoBehaviour
{
    public static WebSpeechWrapper Instance { get; private set; }

    [Header("Web Speech Settings")]
    public string WakeWord = "ассистент";
    public int PauseMs = 1200;

    /// <summary>
    /// Промежуточный результат (interim, потоковый)
    /// </summary>
    public event Action<string> OnPartialResultReceived;
    /// <summary>
    /// Финальный результат (isFinal)
    /// </summary>
    public event Action<string> OnFinalResultReceived;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void WebSpeech_Init();

    [DllImport("__Internal")]
    private static extern void WebSpeech_Start();

    [DllImport("__Internal")]
    private static extern void WebSpeech_Stop();
#else
    private static void WebSpeech_Init() { }
    private static void WebSpeech_Start() { }
    private static void WebSpeech_Stop() { }
#endif

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.name = "WebSpeechWrapper";
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        WebSpeech_Init();
    }

    public void StartRecognition()
    {
        WebSpeech_Start();
    }

    public void StopRecognition()
    {
        WebSpeech_Stop();
    }

    // Вызывается из JS для промежуточных результатов
    // Вызывается из JS для промежуточных результатов
    public void OnPartialResult(string text)
    {
        OnPartialResultReceived?.Invoke(text);
    }

    // Вызывается из JS для финальных результатов
    public void OnFinalResult(string text)
    {
        OnFinalResultReceived?.Invoke(text);
    }
}
