using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommandSystem : MonoBehaviour
{
    [SerializeField] private Camera eyeballCam;
    [SerializeField] private float timeout;

    private KeywordRecognizer keywordRecognizer;
    private KeywordRecognizer computerKeywordRecognizer;
    private KeywordRecognizer cameraKeywordRecognizer;
    private KeywordRecognizer chatBotKeywordRecognizer;
    private KeywordRecognizer faceExpressionKeywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private Dictionary<string, Action> computerActions = new Dictionary<string, Action>();
    private Dictionary<string, Action> cameraActions = new Dictionary<string, Action>();
    private Dictionary<string, Action> chatBotActions = new Dictionary<string, Action>();
    private Dictionary<string, Action> faceExpressionActions = new Dictionary<string, Action>();

    private Coroutine currentCoroutine = null;

    void Start()
    {
        /* Computer Commands */
        actions.Add("computer", Computer);
        computerActions.Add("its go time", ItsGoTime);
        computerActions.Add("next song", NextSong);
        computerActions.Add("stop the music", StopMusic);

        /* Eyeball Cam Commands */
        actions.Add("eyeball", Camera);
        actions.Add("camera", Camera);
        actions.Add("winker", Camera);

        cameraActions.Add("stay", Stay);
        cameraActions.Add("follow", Follow);

        /* ChatBot Commands */
        actions.Add("chat bot", ChatBot);
        chatBotActions.Add("socials", Socials);
        chatBotActions.Add("nya", Nya);
        chatBotActions.Add("say hello", Hello);

        /* Facial Expression Commands */
        // Laughing Face
        faceExpressionActions.Add("haha", FunnyFace);
        faceExpressionActions.Add("funny", FunnyFace);
        faceExpressionActions.Add("hilarious", FunnyFace);

        // Angry Face
        faceExpressionActions.Add("what the hell", AngryFace);
        faceExpressionActions.Add("what the fuck", AngryFace);
        faceExpressionActions.Add("bullshit", AngryFace);
        faceExpressionActions.Add("angry", AngryFace);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += PhraseRecognized;
        keywordRecognizer.Start();

        computerKeywordRecognizer = new KeywordRecognizer(computerActions.Keys.ToArray());
        computerKeywordRecognizer.OnPhraseRecognized += PhraseRecognized;

        cameraKeywordRecognizer = new KeywordRecognizer(cameraActions.Keys.ToArray());
        cameraKeywordRecognizer.OnPhraseRecognized += PhraseRecognized;

        chatBotKeywordRecognizer = new KeywordRecognizer(chatBotActions.Keys.ToArray());
        chatBotKeywordRecognizer.OnPhraseRecognized += PhraseRecognized;

        faceExpressionKeywordRecognizer = new KeywordRecognizer(faceExpressionActions.Keys.ToArray());
        faceExpressionKeywordRecognizer.OnPhraseRecognized += PhraseRecognized;
        faceExpressionKeywordRecognizer.Start();
    }

    private void PhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    IEnumerator TimeoutRecognizer(KeywordRecognizer recognizer)
    {
        yield return new WaitForSeconds(timeout);

    }

    /* Computer Commands */
    private void Computer()
    {
        computerKeywordRecognizer.Start();
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TimeoutRecognizer(computerKeywordRecognizer));
    }

    private void ItsGoTime()
    {
        computerKeywordRecognizer.Stop();
    }

    private void NextSong()
    {
        computerKeywordRecognizer.Stop();
    }

    private void StopMusic()
    {
        computerKeywordRecognizer.Stop();
    }

    /* Camera Commands */
    private void Camera()
    {
        cameraKeywordRecognizer.Start();
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TimeoutRecognizer(cameraKeywordRecognizer));
    }

    private void Stay()
    {
        cameraKeywordRecognizer.Stop();
    }

    private void Follow()
    {
        cameraKeywordRecognizer.Stop();
    }

    /* Chatbot Commands */
    private void ChatBot()
    {
        //chatBotKeywordRecognizer.Start();
        //StopCoroutine(currentCoroutine);
        //currentCoroutine = StartCoroutine(TimeoutRecognizer(chatBotKeywordRecognizer));
    }

    private void Socials()
    {

    }

    private void Nya()
    {

    }

    private void Hello()
    {

    }

    /* Facial Expression Commands */
    private void FunnyFace()
    {

    }

    private void AngryFace()
    {

    }
}
