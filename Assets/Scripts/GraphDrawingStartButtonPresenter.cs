using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class GraphDrawingStartButtonPresenter : MonoBehaviour
{

    [SerializeField] private GraphDrawingStartButtonView graphDrawingStartButtonView;
    [SerializeField] private FileLoadingModel fileLoadingModel;

    void Start()
    {

        // view -> model
        graphDrawingStartButtonView.StartLoadingTheFile.Subscribe(_ => fileLoadingModel.LoadCsv().Forget());

        //// model -> view
        //testhpmodel.hp.subscribe(testhpview.displayhp);


    }
}
