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
        //model-> view

        //SetCsvDataForEachColumn終了時に実行する
        _dataConversionForGraphModel.OnSetCsvDataForEachColumn.Subscribe(_ =>
        {
            //左眼の水平運動のグラフViewに、経過時間のリストを追加する。
            _eyeMovementLeftHorizontalGdsv.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

            //左眼の水平運動のグラフViewに、左眼の水平運動のリストを追加する        //左目（左右）zxのリストを渡して、左眼の水平運動のリスト取得する
            _eyeMovementLeftHorizontalGdsv.AngleList = _dataConversionForGraphModel.GetAngleList(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList());

            //ゲームオブジェクトをアクティブにする事で、OnRenderObject()が実行され、グラフが描画される。
            _eyeMovementLeftHorizontalCanvas.SetActive(true);


            //左眼の垂直運動のグラフViewに、経過時間のリストを追加する。
            _eyeMovementLeftVerticalGdsv.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

            //左眼の垂直運動のグラフViewに、左眼の垂直運動のリストを追加する      //左目（上下）zyのデータを渡して、左眼の垂直運動のリストを取得する。
            _eyeMovementLeftVerticalGdsv.AngleList = _dataConversionForGraphModel.GetAngleList(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList());

            //ゲームオブジェクトをアクティブにする事で、OnRenderObject()が実行され、グラフが描画される。
            _eyeMovementLeftVerticalCanvas.SetActive(true);
        })
        .AddTo(this.gameObject);
    }
}
