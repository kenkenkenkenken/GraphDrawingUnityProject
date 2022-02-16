using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

public interface IDataConversionForGraphModel
{
    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList { get; }
    /// <summary>
    /// 左眼の視線方向xのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList { get; }
    /// <summary>
    /// 左眼の視線方向yのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList { get; }
    /// <summary>
    /// 左眼の視線方向zのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList { get; }
    /// <summary>
    /// SetDataForCsvColumn終了時に発火するストリームのプロパティ
    /// </summary>
    public IObservable<Unit> OnSetDataForCsvColumn { get; }
    /// <summary>
    /// CSVの列のデータを追加する
    /// </summary>
    /// 
    /// <param name="csvDataList">CSVのデータのリスト</param>
    public void SetDataForCsvColumn(List<string[]> csvDataList);
    /// <summary>
    /// 角度のリストを取得する
    /// </summary>
    /// <param name="coordinateList1">座標のリスト</param>
    /// <param name="coordinateList2">座標のリスト</param>
    /// <returns></returns>
    public List<float> GetAngleList(List<float> coordinateList1, List<float> coordinateList2);
}
