using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GraphDrawingStartButtonView : MonoBehaviour
{
    [SerializeField] private Button _drawingStartButton;

    /// <summary>
    /// DrawingStartButton�����������ɔ��΂���X�g���[��
    /// </summary>
    public IObservable<Unit> OnClickDrawingStartButton => _drawingStartButton.OnClickAsObservable();

    private void Start()
    {

        //DrawingStartButton�����������Ɏ��s���� //DrawingStartButton�{�^�����\���ɂ���
        OnClickDrawingStartButton.Subscribe(_ => _drawingStartButton.gameObject.SetActive(false)).AddTo(this.gameObject);
    }
}
