using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class DataConversionForGraphModel : MonoBehaviour
{
    #region Field
    /// <summary>
    /// CSVのデータ名と対応する列数目
    /// </summary>
    private enum CsvDataListIndex
    {
        ApplicationTime = 1, //経過時間
        EyeRayLeftDirX = 24, //左眼の視線方向 x
        EyeRayLeftDirY = 25, //左眼の視線方向 y
        EyeRayLeftDirZ = 26, //左眼の視線方向 z
    }

    /// <summary>
    /// 経過時間のリスト
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();

    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => new ReadOnlyCollection<float>(_applicationTimeList);

    /// <summary>
    /// 左眼の視線方向xのリスト
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirXList = new List<float>();

    /// <summary>
    /// 左眼の視線方向xのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => new ReadOnlyCollection<float>(_eyeRayLeftDirXList);

    /// <summary>
    /// 左眼の視線方向yのリスト
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirYList = new List<float>();

    /// <summary>
    /// 左眼の視線方向yのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => new ReadOnlyCollection<float>(_eyeRayLeftDirYList);

    /// <summary>
    /// 左眼の視線方向zのリスト
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirZList = new List<float>();

    /// <summary>
    /// 左眼の視線方向zのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => new ReadOnlyCollection<float>(_eyeRayLeftDirZList);

    /// <summary>
    /// SetCsvDataForEachColumn終了時に発火するストリーム
    /// </summary>
    private Subject<Unit> _onSetCsvDataForEachColumn = new Subject<Unit>();

    /// <summary>
    /// SetCsvDataForEachColumn終了時に発火するストリームのプロパティ
    /// </summary>
    public IObservable<Unit> OnSetCsvDataForEachColumn => _onSetCsvDataForEachColumn;
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
            _applicationTimeList.Add(float.Parse(csvDataList[i][(int)CsvDataListIndex.ApplicationTime]));
            _eyeRayLeftDirXList.Add(float.Parse(csvDataList[i][(int)CsvDataListIndex.EyeRayLeftDirX]));
            _eyeRayLeftDirYList.Add(float.Parse(csvDataList[i][(int)CsvDataListIndex.EyeRayLeftDirY]));
            _eyeRayLeftDirZList.Add(float.Parse(csvDataList[i][(int)CsvDataListIndex.EyeRayLeftDirZ]));
        }
        _onSetCsvDataForEachColumn.OnNext(Unit.Default);
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
