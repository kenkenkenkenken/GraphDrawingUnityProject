using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GraphDrawingStartButtonView : MonoBehaviour
{
    [SerializeField] private Button _drawingStartButton;

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
