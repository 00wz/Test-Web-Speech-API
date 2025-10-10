# UILogger — Logging to UI in Unity

## Как корректно отобразить кириллицу в WebGL

### Для стандартного UI Text

1. **Создайте Font Asset с поддержкой кириллицы:**
   - В Project, кликните правой кнопкой мыши → Create → Legacy → Font.
   - В Inspector выберите Arial.ttf (или другой шрифт с поддержкой кириллицы).
   - В поле Character выберите "Unicode" или "Custom Set".
   - В поле Custom Characters вставьте все буквы кириллицы:  
     `АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя`
   - Сохраните Font Asset.

2. **Используйте этот Font Asset в вашем UI Text:**
   - В компоненте Text укажите созданный Font Asset в поле Font.

3. **Проверьте, что в Canvas/Text компоненте выбран именно этот Font Asset.**

---

## How to Use UILogger

1. **Add a UI Text element to your Canvas:**
   - In the Unity Editor, right-click your Canvas in the Hierarchy.
   - Select `UI > Text`.
   - Position and resize the Text as desired for your log display.

2. **Attach the UILogger script:**
   - Drag `UILogger.cs` onto any GameObject in your scene (e.g., an empty GameObject named `UILogger`).

3. **Assign the UI Text:**
   - In the Inspector for your UILogger GameObject, assign the `uiText` field to your Text component.

4. **(Optional) Set max log lines:**
   - Adjust the `maxLines` field to control how many log lines are shown in the UI.

5. **Log messages from code:**
   - Use the static methods:
     ```csharp
     UILogger.Log("Это лог-сообщение");
     UILogger.LogWarning("Это предупреждение");
     UILogger.LogError("Это ошибка");
     ```
   - All standard `Debug.Log`, `Debug.LogWarning`, and `Debug.LogError` calls will also appear in the UI.

6. **Play the scene:**
   - All logs will appear in your UI Text in real time.

---

**Note:**  
- Логгер поддерживает rich text для цветных уровней логов.
- Одновременно активен только один экземпляр UILogger (singleton pattern).
- Для WebGL используйте Font Asset с поддержкой кириллицы!
