using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

public class GraphDrawingStartButtonView : MonoBehaviour
{

    [SerializeField] private Button drawingStartButton;

    public IObservable<Unit> StartLoadingTheFile => drawingStartButton.OnClickAsObservable().TakeUntilDestroy(this);

    void Start()
    {
        drawingStartButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => drawingStartButton.gameObject.SetActive(false));
    }
}
