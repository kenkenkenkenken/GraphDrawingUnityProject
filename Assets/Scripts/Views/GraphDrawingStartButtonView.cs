using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class GraphDrawingStartButtonView: MonoBehaviour
{
    private Button _drawingStartButton;

    [Inject]
    void Construct(Button drawingStartButton)
    {
        _drawingStartButton = drawingStartButton;
    }

    /// <summary>
    /// DrawingStartButtonを押した時に発火するストリーム
    /// </summary>
    public IObservable<Unit> OnClickDrawingStartButton => _drawingStartButton.OnClickAsObservable();

    private void Start()
    {
        //DrawingStartButtonを押した時に実行する //DrawingStartButtonボタンを非表示にする
        OnClickDrawingStartButton.Subscribe(_ => _drawingStartButton.gameObject.SetActive(false)).AddTo(this.gameObject);
    }
}
