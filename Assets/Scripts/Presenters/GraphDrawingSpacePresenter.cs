using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using static GraphDrawingSpaceViewType;
using static RectTransformType;

public class GraphDrawingSpacePresenter : IInitializable
{
    #region Field
    /// <summary>
    /// 左眼の水平運動のグラフ描画のView
    /// </summary>
    private IGraph _eyeMovementLeftHorizontal;

    /// <summary>
    /// 左眼の水平運動のグラフ描画のViewのプロパティ
    /// </summary>
    ///<remarks>
    /// get時にIGraphに変換する
    ///</remarks>
    public IGraph IgraphEyeMovementLeftHorizontal
    {
        get { return _eyeMovementLeftHorizontal; }
    }

    /// <summary>
    /// 左眼の垂直運動のグラフ描画のView
    /// </summary>
    private IGraph _eyeMovementLeftVertical;

    /// <summary>
    /// 左眼の垂直運動のグラフ描画のViewのプロパティ
    /// </summary>
    ///<remarks>
    /// get時にIGraphに変換する
    ///</remarks>
    public IGraph IgraphEyeMovementLeftVertical
    {
        get { return _eyeMovementLeftVertical; }
    }

    private IDataConversionForGraphModel _dataConversionForGraphModel;
    private RectTransform _eyeMovementLeftHorizontalCanvas;
    private RectTransform _eyeMovementLeftVerticalCanvas;
    #endregion

    public GraphDrawingSpacePresenter(
        [Inject(Id = EyeMovementLeftHorizontalCanvas)]
        RectTransform eyeMovementLeftHorizontalCanvas,
        [Inject(Id = EyeMovementLeftVerticalCanvas)]
        RectTransform eyeMovementLeftVerticalCanvas,

        [Inject(Id = EyeMovementLeftHorizontal)]
        IGraph eyeMovementLeftHorizontal,
        [Inject(Id = EyeMovementLeftVertical)]
        IGraph eyeMovementLeftVertical,

        IDataConversionForGraphModel dataConversionForGraphModel
    )
    {
        _eyeMovementLeftHorizontal = eyeMovementLeftHorizontal;
        _eyeMovementLeftVertical = eyeMovementLeftVertical;
        _dataConversionForGraphModel = dataConversionForGraphModel;
        _eyeMovementLeftHorizontalCanvas = eyeMovementLeftHorizontalCanvas;
        _eyeMovementLeftVerticalCanvas = eyeMovementLeftVerticalCanvas;
    }

    void IInitializable.Initialize()
    {
        //model-> view

        //SetDataForCsvColumn終了時に実行する
        _dataConversionForGraphModel.OnSetDataForCsvColumn.Subscribe(_ =>
        {
            //左眼の水平運動のグラフ描画
            PreprocessToDrawGraph(IgraphEyeMovementLeftHorizontal, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList(), _eyeMovementLeftHorizontalCanvas);

            //左眼の垂直運動のグラフ描画
            PreprocessToDrawGraph(IgraphEyeMovementLeftVertical, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList(), _eyeMovementLeftVerticalCanvas);
        });
    }

    /// <summary>
    /// グラフ描画の前処理を行う
    /// </summary>
    /// <param name="graphView">グラフ描画のView</param>
    /// <param name="coordinateList1">座標</param>
    /// <param name="coordinateList2">座標</param>
    /// <param name="canvas">グラフ描画のゲームオブジェクト</param>
    void PreprocessToDrawGraph(IGraph graphView, List<float> coordinateList1, List<float> coordinateList2, RectTransform canvas)
    {
        //グラフViewに、経過時間のリストを追加する。
        graphView.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

        //グラフViewに、角度のリストを追加する        //座標のリストを2つ渡して、角度のリストを取得する
        graphView.AngleList = _dataConversionForGraphModel.GetAngleList(coordinateList1, coordinateList2);

        //ゲームオブジェクトをアクティブにする事で、OnRenderObject()が実行され、グラフが描画される。
        canvas.gameObject.SetActive(true);

        //GL描画用マテリアルを設定する
        graphView.CreateLineMaterial();
    }
}
