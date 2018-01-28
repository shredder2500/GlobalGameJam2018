using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextEffects : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onOutComplete;
    [SerializeField]
    private UnityEvent _onInComplete;
    private Text Renderer => GetComponent<Text>();

    public void In()
    {
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 0);
        transform.localScale = Vector3.zero;

        Renderer.DOFade(.75f, 1);
        transform.DOScale(.75f, 1)
            .onComplete += _onInComplete.Invoke;
    }

    public void Out()
    {
        Renderer.DOFade(0, 1);
        transform.DOScale(0, 1)
            .onComplete += _onOutComplete.Invoke;
    }

}
