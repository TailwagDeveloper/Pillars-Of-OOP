using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;
public class Menu : MonoBehaviour
{
    private float fadeSpeed = 10f;
    [SerializeField]
    private Image panel;
    [SerializeField]
    private Image button;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    public void CallStartFadeOut()
    {
        if (!IsInvoking("StartFadeOut"))
        {
            StartCoroutine(nameof(StartFadeOut));
        }
    }
    private IEnumerator StartFadeOut()
    {
        while (panel.color.a > 0.05f)
        {
            FadeOutImage(panel);
            FadeOutImage(button);
            FadeOutText(buttonText);
            FadeOutText(text1);
            FadeOutText(text2);
            yield return new WaitForSeconds(0.05f);
        }
        panel.gameObject.SetActive(false);
    }
    private void FadeOutImage(Image image)
    {
        image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), Time.deltaTime * fadeSpeed);
    }
    private void FadeOutText(TextMeshProUGUI text)
    {
        text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 0), Time.deltaTime * fadeSpeed);
    }
}
