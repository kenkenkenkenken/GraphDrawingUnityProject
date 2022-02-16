using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using static CsvDataListIndex;

public class DataConversionForGraphModel : IDataConversionForGraphModel
{
    #region Field
    /// <summary>
    /// 経過時間のリスト
    /// </summary>
    private List<float> _applicationTimeList = new List<float>();
    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => _applicationTimeList.AsReadOnly();

    /// <summary>
    /// 左眼の視線方向xのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirXList = new List<float>();
    /// <summary>
    /// 左眼の視線方向xのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => _eyeRayLeftDirXList.AsReadOnly();

    /// <summary>
    /// 左眼の視線方向yのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirYList = new List<float>();
    /// <summary>
    /// 左眼の視線方向yのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => _eyeRayLeftDirYList.AsReadOnly();

    /// <summary>
    /// 左眼の視線方向zのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirZList = new List<float>();
    /// <summary>
    /// 左眼の視線方向zのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => _eyeRayLeftDirZList.AsReadOnly();

    /// <summary>
    /// SetDataForCsvColumn終了時に発火するストリーム
    /// </summary>
    private Subject<Unit> _onSetDataForCsvColumn = new Subject<Unit>();
    /// <summary>
    /// SetDataForCsvColumn終了時に発火するストリームのプロパティ
    /// </summary>
    public IObservable<Unit> OnSetDataForCsvColumn => _onSetDataForCsvColumn;
    #endregion


    /// <summary>
    /// CSVの列のデータを追加する
    /// </summary>
    /// 
    /// <param name="csvDataList">CSVのデータのリスト</param>
    public void SetDataForCsvColumn(List<string[]> csvDataList)
    {
        _applicationTimeList.Clear();
        _eyeRayLeftDirXList.Clear();
        _eyeRayLeftDirYList.Clear();
        _eyeRayLeftDirZList.Clear();

        for (int i = 1; i < csvDataList.Count; i++)
        {
            _applicationTimeList.Add(float.Parse(csvDataList[i][(int)ApplicationTime]));
            _eyeRayLeftDirXList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirX]));
            _eyeRayLeftDirYList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirY]));
            _eyeRayLeftDirZList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirZ]));
        }
        _onSetDataForCsvColumn.OnNext(Unit.Default);
    }

    /// <summary>
    /// 角度のリストを取得する
    /// </summary>
    /// <param name="coordinateList1">座標のリスト</param>
    /// <param name="coordinateList2">座標のリスト</param>
    /// <returns></returns>
    public List<float> GetAngleList(List<float> coordinateList1, List<float> coordinateList2)
    {
        var angleList = new List<float>();
        for (int i = 0; i < coordinateList1.Count; i++)
        {
            angleList.Add(AngleUtility.GetAngle(coordinateList1[i], coordinateList2[i]));
        }
        return angleList;
    }
}
