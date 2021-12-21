using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpacePresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftHorizontalGdsv;
    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftVerticalGdsv;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;
    [SerializeField] private GameObject _eyeMovementLeftHorizontalCanvas;
    [SerializeField] private GameObject _eyeMovementLeftVerticalCanvas;

void Start()
    {
        // model -> view
        _dataConversionForGraphModel.ObserveAddEyeMovementLeftHorizontalList
        .Subscribe(_ =>
        {
            _eyeMovementLeftHorizontalGdsv.SetGraphData
            (
                _dataConversionForGraphModel.ApplicationTimeList.ToList(),
                _dataConversionForGraphModel.EyeMovementLeftHorizontalList.SelectMany(c => c).ToList()
            );
            _eyeMovementLeftHorizontalCanvas.SetActive(true);
        })
        .AddTo(this.gameObject);

        _dataConversionForGraphModel.ObserveAddEyeMovementLeftVerticalList
        .Subscribe(_ =>
        {
            _eyeMovementLeftVerticalGdsv.SetGraphData
            (
                _dataConversionForGraphModel.ApplicationTimeList.ToList(),
                _dataConversionForGraphModel.eyeMovementLeftVerticalList.SelectMany(c => c).ToList()
            );
            _eyeMovementLeftVerticalCanvas.SetActive(true);
        })
        .AddTo(this.gameObject);

    }
}

