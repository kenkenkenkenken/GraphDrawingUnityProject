using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UniRx;

public class GraphDrawingStartButtonView : MonoBehaviour
{

    [SerializeField] private Button button;

    public IObservable<Unit> StartLoadingTheFile => button.OnClickAsObservable().TakeUntilDestroy(this);


    void Start()
    {

    }
}
