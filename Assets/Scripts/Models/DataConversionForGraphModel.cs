using System;
using System.Collections;
using System.Collections.Generic;
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

    public List<float> applicationTimeList = new List<float>();
    private List<float> _eyeRayLeftDirXList = new List<float>();
    private List<float> _eyeRayLeftDirYList = new List<float>();
    private List<float> _eyeRayLeftDirZList = new List<float>();


    Subject<Unit> subject = new Subject<Unit>();

    public ReactiveCollection<List<float>> eyeMovementLeftHorizontalList = new ReactiveCollection<List<float>>();
    public ReactiveCollection<List<float>> eyeMovementLeftVerticalList = new ReactiveCollection<List<float>>();
    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftHorizontalList => eyeMovementLeftHorizontalList.ObserveAdd();
    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftVerticalList => eyeMovementLeftVerticalList.ObserveAdd();
    
    void Start()
    {
        //左目（左右）zxの、グラフ出力データを取得する。
        subject
        .Subscribe(_ =>
        {
            eyeMovementLeftHorizontalList.Clear();
            eyeMovementLeftHorizontalList.Add(GetGraphData(_eyeRayLeftDirZList, _eyeRayLeftDirXList));
        })
        .AddTo(this.gameObject);


        ////左目（上下）zyの、グラフ出力データを取得する。
        subject
        .Subscribe(_ =>
        {
            eyeMovementLeftVerticalList.Clear();
            eyeMovementLeftVerticalList.Add(GetGraphData(_eyeRayLeftDirZList, _eyeRayLeftDirYList));
        })
        .AddTo(this.gameObject);
    }



    public void SetProcessingData(List<string[]> mDataList)
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
        subject.OnNext(Unit.Default);

    }

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
