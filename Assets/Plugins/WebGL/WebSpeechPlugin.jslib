mergeInto(LibraryManager.library, {
  WebSpeech_Init: function() {
    window._webSpeechPlugin = window._webSpeechPlugin || {};
    window._webSpeechPlugin.recognition = null;
    window._webSpeechPlugin.listening = false;
    window._webSpeechPlugin.targetObject = "WebSpeechWrapper";
  },

  WebSpeech_Start: function() {
    if (!('webkitSpeechRecognition' in window) && !('SpeechRecognition' in window)) {
      alert('Web Speech API не поддерживается этим браузером');
      return;
    }
    var SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
    var plugin = window._webSpeechPlugin;
    if (!plugin) return;
    var recognition = new SpeechRecognition();
    recognition.lang = 'ru-RU';
    recognition.continuous = true;
    recognition.interimResults = true;
    plugin.recognition = recognition;
    plugin.listening = true;

    recognition.onresult = function(event) {
      var results = event.results;
      if (!results || results.length === 0) return;
      var result = results[results.length - 1];
      if (!result || !result[0]) return;
      var transcript = result[0].transcript;
      var isFinal = result.isFinal;
      if (isFinal)
        SendMessage(plugin.targetObject, "OnFinalResult", transcript);
      else
        SendMessage(plugin.targetObject, "OnPartialResult", transcript);
    };

    recognition.onend = function() {
      if (plugin.listening) {
        recognition.start();
      }
    };

    recognition.start();
  },

  WebSpeech_Stop: function() {
    var plugin = window._webSpeechPlugin;
    if (plugin && plugin.recognition) {
      plugin.recognition.stop();
      plugin.listening = false;
    }
  }
});
