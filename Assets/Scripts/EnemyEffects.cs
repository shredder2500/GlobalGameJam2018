using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyEffects : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onOutComplete;
    [SerializeField]
    private GameObject _explosion;
    private SpriteRenderer Renderer => GetComponent<SpriteRenderer>();

    public void In()
    {
        Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 0);
        transform.localScale = Vector3.zero;

        Renderer.DOFade(1, 1);
        transform.DOScale(1, 1);
    }

    public void Out()
    {
        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        Renderer.DOFade(0, 1)
            .onComplete += () => Destroy(explosion);
        transform.DOScale(0, 1)
            .onComplete += _onOutComplete.Invoke;
    }

}
