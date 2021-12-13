using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GraphDrawingPanelPresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingPanelView _graphDrawingPanelView;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;
    [SerializeField] private TestGraphButton testGraphButton;

void Start()
    {
        // model -> view
        _dataConversionForGraphModel.DrawGraph.Subscribe(_ =>
        {
            _graphDrawingPanelView.DrawGraph(_dataConversionForGraphModel._applicationTimeList, _dataConversionForGraphModel.eyeMovementLeftHorizontalList.SelectMany(c => c).ToList());
            testGraphButton.DrawGraph(_dataConversionForGraphModel._applicationTimeList, _dataConversionForGraphModel.eyeMovementLeftHorizontalList.SelectMany(c => c).ToList());
        });

    }
}

