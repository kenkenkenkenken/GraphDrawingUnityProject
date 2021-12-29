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
            //左眼の水平運動のグラフ描画
            DrawGraph(_eyeMovementLeftHorizontalGdsv, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList(), _eyeMovementLeftHorizontalCanvas);
            
            //左眼の垂直運動のグラフ描画
            DrawGraph(_eyeMovementLeftVerticalGdsv, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList(), _eyeMovementLeftVerticalCanvas);
        })
        .AddTo(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphView">グラフ描画のView</param>
    /// <param name="coordinateList1">座標</param>
    /// <param name="coordinateList2">座標</param>
    /// <param name="canvas">グラフ描画のゲームオブジェクト</param>
    void DrawGraph(GraphDrawingSpaceView graphView, List<float> coordinateList1, List<float> coordinateList2, GameObject canvas)
    {
        //グラフViewに、経過時間のリストを追加する。
        graphView.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

        //グラフViewに、角度のリストを追加する        //座標のリストを2つ渡して、角度のリストを取得する
        graphView.AngleList = _dataConversionForGraphModel.GetAngleList(coordinateList1, coordinateList2);

        //グラフViewの、GL描画用マテリアルを設定する
        graphView.CreateLineMaterial();

        //ゲームオブジェクトをアクティブにする事で、OnRenderObject()が実行され、グラフが描画される。
        canvas.SetActive(true);
    }
}
