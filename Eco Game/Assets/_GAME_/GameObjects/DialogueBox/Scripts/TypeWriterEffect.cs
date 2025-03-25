using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{

    [SerializeField] private float writingSpeed = 30;

    public bool isRunning {  get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.','!','?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',',';',':'}, 0.3f)

    };

private Coroutine typingCoroutine;
    public void Run (string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType,textLabel));
    }


    public void Stop ()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    private IEnumerator TypeText (string textToType, TMP_Text textLabel)
    {
        isRunning = true;
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i + 1);
                

                if (isPunctuation(textToType[i], out float waitTime) && !isLast && !isPunctuation(textToType[i + 1], out float _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }


            yield return null;
        }

        isRunning = false;
    }

    private bool isPunctuation (char character, out float waitTime)
    {
        foreach (Punctuation punctuationsCategory in punctuations)
        {
            if (punctuationsCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationsCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
