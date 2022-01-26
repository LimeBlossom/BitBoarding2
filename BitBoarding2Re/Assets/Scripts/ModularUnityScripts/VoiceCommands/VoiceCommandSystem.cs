using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommandSystem : MonoBehaviour
{
    [SerializeField] private VoiceCommand[] commands;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, GameEvent[]> gameEvents = new Dictionary<string, GameEvent[]>();

    void Start()
    {
        for(int i = 0; i < commands.Length; i++)
        {
            foreach(string phrase in commands[i].phrases)
            {
                gameEvents.Add(phrase, commands[i].eventsToCall);
            }
        }

        keywordRecognizer = new KeywordRecognizer(gameEvents.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += PhraseRecognized;
        keywordRecognizer.Start();
    }

    private void PhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        foreach(GameEvent gameEvent in gameEvents[speech.text])
        {
            gameEvent.Raise();
        }
    }
}
