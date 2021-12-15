using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GraphDrawingSpacePresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingSpaceView _graphDrawingSpaceView;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;
    [SerializeField] private GameObject _graphDrawingSpace;

void Start()
    {
        // model -> view
        _dataConversionForGraphModel.DrawGraph.Subscribe(_ =>
        {
            //LineRenderer�ł̃O���t�o�͗p
            //_graphDrawingPanelView.DrawGraph(_dataConversionForGraphModel._applicationTimeList, _dataConversionForGraphModel.eyeMovementLeftHorizontalList.SelectMany(c => c).ToList());
            //GL�ł̃O���t�o�͗p
            _graphDrawingSpaceView.SetGraphData(_dataConversionForGraphModel._applicationTimeList, _dataConversionForGraphModel.eyeMovementLeftHorizontalList.SelectMany(c => c).ToList());
            _graphDrawingSpace.SetActive(true);

        });

    }
}

