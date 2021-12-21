using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class DataConversionForGraphModel : MonoBehaviour
{
    /// <summary>
    /// CSVのデータ名と対応する列数目
    /// </summary>
    private enum MDataListIndex
    {
        ApplicationTime = 1, //経過時間
        EyeRayLeftDirX = 24, //左眼の視線方向 x
        EyeRayLeftDirY = 25, //左眼の視線方向 y
        EyeRayLeftDirZ = 26, //左眼の視線方向 z
    }

    /// <summary>
    /// 経過時間のリスト
    /// </summary>
    private List<float> applicationTimeList = new List<float>();

    /// <summary>
    /// 経過時間のReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => new ReadOnlyCollection<float>(applicationTimeList);

    /// <summary>
    /// 左眼の視線方向xのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirXList = new List<float>();

    /// <summary>
    /// 左眼の視線方向xのReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => new ReadOnlyCollection<float>(_eyeRayLeftDirXList);

    /// <summary>
    /// 左眼の視線方向yのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirYList = new List<float>();

    /// <summary>
    /// 左眼の視線方向yのReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => new ReadOnlyCollection<float>(_eyeRayLeftDirYList);

    /// <summary>
    /// 左眼の視線方向zのリスト
    /// </summary>
    private List<float> _eyeRayLeftDirZList = new List<float>();

    /// <summary>
    /// 左眼の視線方向zのReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => new ReadOnlyCollection<float>(_eyeRayLeftDirZList);


    /// <summary>
    /// 左眼の水平運動のReactiveCollection
    /// </summary>
    private ReactiveCollection<List<float>> _eyeMovementLeftHorizontalList = new ReactiveCollection<List<float>>();

    public ReactiveCollection<List<float>> EyeMovementLeftHorizontalList
    {
        get { return _eyeMovementLeftHorizontalList; }
    }

    /// <summary>
    /// 左眼の垂直運動のReactiveCollection
    /// </summary>
    public ReactiveCollection<List<float>> eyeMovementLeftVerticalList = new ReactiveCollection<List<float>>();

    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftHorizontalList => EyeMovementLeftHorizontalList.ObserveAdd();
    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftVerticalList => eyeMovementLeftVerticalList.ObserveAdd();
    
    void Start()
    {

    }


    /// <summary>
    /// CSVデータを列ごとに分割する
    /// </summary>
    /// <param name="mDataList"></param>
    public void DivideCsvDataByColumn(ReadOnlyCollection<string[]> mDataList)
    {
        applicationTimeList.Clear();
        _eyeRayLeftDirXList.Clear();
        _eyeRayLeftDirYList.Clear();
        _eyeRayLeftDirZList.Clear();

        for (int i = 1; i < mDataList.Count; i++)
        {
            applicationTimeList.Add(float.Parse(mDataList[i][(int)MDataListIndex.ApplicationTime]));
            _eyeRayLeftDirXList.Add(float.Parse(mDataList[i][(int)MDataListIndex.EyeRayLeftDirX]));
            _eyeRayLeftDirYList.Add(float.Parse(mDataList[i][(int)MDataListIndex.EyeRayLeftDirY]));
            _eyeRayLeftDirZList.Add(float.Parse(mDataList[i][(int)MDataListIndex.EyeRayLeftDirZ]));
        }
    }

    /// <summary>
    /// グラフとして出力するデータを取得する
    /// </summary>
    /// <param name="dataList1"></param>
    /// <param name="dataList2"></param>
    /// <returns></returns>
    public List<float> GetGraphData(List<float> dataList1, List<float> dataList2)
    {
        var graphDataList = new List<float>();
        for (int i = 0; i < dataList1.Count; i++)
        {
            graphDataList.Add(AngleUtility.GetAngle(dataList1[i], dataList2[i]));
        }
        return graphDataList;
    }
}
