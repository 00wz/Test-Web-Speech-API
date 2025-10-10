using UnityEngine;

public class WebSpeechExample : MonoBehaviour
{
    void Start()
    {
        // Подписка на потоковые и финальные результаты
        WebSpeechWrapper.Instance.OnPartialResultReceived += OnPartial;
        WebSpeechWrapper.Instance.OnFinalResultReceived += OnFinal;

        // Запуск распознавания речи
        WebSpeechWrapper.Instance.StartRecognition();
    }

    void OnPartial(string text)
    {
        Debug.Log("[Потоковый результат] " + text);
        // Можно выводить в UI для live-отображения
    }

    void OnFinal(string text)
    {
        Debug.Log("[Финальный результат] " + text);
        // Здесь можно отправить текст AI-ассистенту
    }

    void OnDestroy()
    {
        if (WebSpeechWrapper.Instance != null)
        {
            WebSpeechWrapper.Instance.OnPartialResultReceived -= OnPartial;
            WebSpeechWrapper.Instance.OnFinalResultReceived -= OnFinal;
        }
    }
}
