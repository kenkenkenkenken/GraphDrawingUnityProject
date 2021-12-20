using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UniRx.Diagnostics;

public class GraphDrawingStartButtonPresenter : MonoBehaviour
{

    [SerializeField] private GraphDrawingStartButtonView graphDrawingStartButtonView;
    [SerializeField] private FileLoadingModel fileLoadingModel;

    void Start()
    {

        // view -> model
        graphDrawingStartButtonView.OnClickDrawingStartButton.Subscribe(_ =>fileLoadingModel.LoadCsv().Forget()).AddTo(this.gameObject);

        //// model -> view 
        //testhpmodel.hp.subscribe(testhpview.displayhp);
        //graphDrawingStartButtonView.OnClickDrawingStartButton.Subscribe(_ => fileLoadingModel.LoadCsv().Forget());
    }
}
