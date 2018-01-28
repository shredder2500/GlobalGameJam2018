using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreIndicator : MonoBehaviour
{
    private Text _textComponent;
    private string text { get { return _textComponent.text; } set { _textComponent.text = value; } }

    [SerializeField]
    private Manager _manager;

    private void Start() => _textComponent = GetComponent<Text>();

    public void Update() => text = $"SCORE: { _manager.Score }";
}
