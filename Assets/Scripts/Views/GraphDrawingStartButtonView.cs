using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

public class GraphDrawingStartButtonView : MonoBehaviour
{
    [SerializeField] private Button drawingStartButton;

    public IObservable<Unit> OnClickDrawingStartButton => drawingStartButton.OnClickAsObservable();

    void Start()
    {
        OnClickDrawingStartButton.Subscribe(_ => drawingStartButton.gameObject.SetActive(false)).AddTo(this.gameObject);
    }
}
