using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Effects : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onOutComplete;
    [SerializeField]
    private UnityEvent _onInComplete;
    private Image Renderer => GetComponent<Image>();

    public void In()
    {
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 0);
        transform.localScale = Vector3.zero;

        Renderer.DOFade(1, 1);
        transform.DOScale(1, 1)
            .onComplete += _onInComplete.Invoke;
    }

    public void Out()
    {
        Renderer.DOFade(0, 1);
        transform.DOScale(0, 1)
            .onComplete += _onOutComplete.Invoke;
    }

}
