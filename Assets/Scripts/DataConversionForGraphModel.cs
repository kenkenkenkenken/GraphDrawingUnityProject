using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DataConversionForGraphModel : MonoBehaviour
{
    private enum Graph
    {
        ApplicationTime = 1,
        EyeRayLeftDirX = 24,
        EyeRayLeftDirY = 25,
        EyeRayLeftDirZ = 26,
    }
    // 1   Application Time(経過時間 )
    // 24  Eye Ray left dir  x（ 左眼の視線方向 x ）
    // 25  Eye Ray left dir  y（ 左眼の視線方向 y ）
    // 26  Eye Ray left dir  z（ 左眼の視線方向 z ）

    public List<float> _applicationTimeList = new List<float>();
    private List<float> _eyeRayLeftDirXList = new List<float>();
    private List<float> _eyeRayLeftDirYList = new List<float>();
    private List<float> _eyeRayLeftDirZList = new List<float>();


    Subject<Unit> subject = new Subject<Unit>();

    public ReactiveCollection<List<float>> eyeMovementLeftHorizontalList = new ReactiveCollection<List<float>>();
    public ReactiveCollection<List<float>> eyeMovementLeftVerticalList = new ReactiveCollection<List<float>>();
    public IObservable<CollectionAddEvent<List<float>>> DrawGraph => eyeMovementLeftHorizontalList.ObserveAdd();

    void Start()
    {
        //左目（左右）zxの、グラフ出力データを取得する。
        subject.Subscribe(_ =>
        {
            eyeMovementLeftHorizontalList.Clear();
            eyeMovementLeftHorizontalList.Add(GetGraphData(_eyeRayLeftDirZList, _eyeRayLeftDirXList));
        });

        

        Debug.Log("start");
        ////左目（左右）zxの、グラフ出力データを取得する。
        //subject.Subscribe(_ => eyeMovementLeftHorizontal = GetGraphData(_eyeRayLeftDirZList, _eyeRayLeftDirXList));

        ////左目（上下）zyの、グラフ出力データを取得する。
        //subject.Subscribe(_ => eyeMovementLeftVertical = GetGraphData(_eyeRayLeftDirZList, _eyeRayLeftDirYList));
    }

    
    //public ReactiveCollection<float> GetGraphData(List<float> dataList1, List<float> dataList2)
    //{
    //    var graphDataList = new ReactiveCollection<float>();
    //    for (int i = 1; i < dataList1.Count; i++)
    //    {
    //        graphDataList.Add(AngleUtility.GetAngle(dataList1[i], dataList2[i]));
    //    }

    //    return graphDataList;
    //}

    public void SetProcessingData(List<string[]> mDataList)
    {
        _applicationTimeList.Clear();
        _eyeRayLeftDirXList.Clear();
        _eyeRayLeftDirYList.Clear();
        _eyeRayLeftDirZList.Clear();

        for (int i = 1; i < mDataList.Count; i++)
        {
            _applicationTimeList.Add(float.Parse(mDataList[i][(int)Graph.ApplicationTime]));
            _eyeRayLeftDirXList.Add(float.Parse(mDataList[i][(int)Graph.EyeRayLeftDirX]));
            _eyeRayLeftDirYList.Add(float.Parse(mDataList[i][(int)Graph.EyeRayLeftDirY]));
            _eyeRayLeftDirZList.Add(float.Parse(mDataList[i][(int)Graph.EyeRayLeftDirZ]));
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
        Debug.Log("GetGraphDat");
        return graphDataList;
    }

    public void Test()
    {

    }
}
//for (var i = 1; i < mDataList.Count; i++ )
//{
//   var lx = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][24]));
//}

//参考：
// 左目（左右）zx
//lx = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][24]));
// 左目（上下）zy
//ly = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][25]));

//index
// 1   Application Time(経過時間 )
// 24  Eye Ray left dir  x（ 左眼の視線方向 x ）
// 25  Eye Ray left dir  y（ 左眼の視線方向 y ）
// 26  Eye Ray left dir  z（ 左眼の視線方向 z ）