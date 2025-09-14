using System.Collections;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType,textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0f;
        int charIndex = 0;

        while (charIndex < textToType.Length) 
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex,0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;
        }

    }
}
