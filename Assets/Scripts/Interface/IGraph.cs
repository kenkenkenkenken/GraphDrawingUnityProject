using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グラフ描画クラスに継承させる
/// </summary>
public interface IGraph
{
    /// <summary>
    /// GL描画用マテリアルのプロパティ
    /// </summary>
    public Material LineMaterial { get; set; }

    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public List<float> ApplicationTimeList{ get; set; }

    /// <summary>
    /// 角度のリストのプロパティ
    /// </summary>
    public List<float> AngleList { get; set; }

    /// <summary>
    /// カメラがシーンをレンダリングされた後に呼び出される
    /// </summary>
    public void OnRenderObject();

    /// <summary>
    /// // GL描画用マテリアルを設定する
    /// </summary>
    public void CreateLineMaterial();

    /// <summary>
    /// 背景を描く
    /// </summary>
    public void DrawBackground();

    /// <summary>
    /// 系列を描く
    /// </summary>
    /// <param name="applicationTimeList">経過時間のリスト</param>
    /// <param name="angleList">角度のリスト</param>
    public void DrawSeries(List<float> applicationTimeList, List<float> angleList);

    /// <summary>
    /// 横目盛りを描く 
    /// </summary>
    public void DrawHorizontalScaleLine();

    /// <summary>
    /// 縦目盛りを描く 
    /// </summary>
    public void DrawVerticalScaleLine();

    /// <summary>
    /// 垂直の目盛りの中央の線を太く描く
    /// </summary>
    public void DrawVerticalScaleCenterLine();

    /// <summary>
    /// 水平の目盛りの中央の線を太く描く
    /// </summary>
    public void DrawHorizontalScaleCenterLine();

    /// <summary>
    /// 枠線を描く
    /// </summary>
    public void DrawFrameBorder();

    /// <summary>
    /// ゲームオブジェクトを追従してグラフを描く
    /// </summary>
    /// <param name="x">追従前の描画位置のx座標</param>
    /// <param name="y">追従前の描画位置のy座標</param>
    /// <param name="z">追従前の描画位置のz座標</param>
    public void FollowAndDrawGraph(float x, float y, float z);
}
