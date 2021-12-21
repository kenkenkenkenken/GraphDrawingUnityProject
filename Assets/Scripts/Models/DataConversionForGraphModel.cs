using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class DataConversionForGraphModel : MonoBehaviour
{
    /// <summary>
    /// CSV�̃f�[�^���ƑΉ�����񐔖�
    /// </summary>
    private enum MDataListIndex
    {
        ApplicationTime = 1, //�o�ߎ���
        EyeRayLeftDirX = 24, //����̎������� x
        EyeRayLeftDirY = 25, //����̎������� y
        EyeRayLeftDirZ = 26, //����̎������� z
    }

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g
    /// </summary>
    private List<float> applicationTimeList = new List<float>();

    /// <summary>
    /// �o�ߎ��Ԃ�ReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => new ReadOnlyCollection<float>(applicationTimeList);

    /// <summary>
    /// ����̎�������x�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirXList = new List<float>();

    /// <summary>
    /// ����̎�������x��ReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => new ReadOnlyCollection<float>(_eyeRayLeftDirXList);

    /// <summary>
    /// ����̎�������y�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirYList = new List<float>();

    /// <summary>
    /// ����̎�������y��ReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => new ReadOnlyCollection<float>(_eyeRayLeftDirYList);

    /// <summary>
    /// ����̎�������z�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirZList = new List<float>();

    /// <summary>
    /// ����̎�������z��ReadOnlyCollection
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => new ReadOnlyCollection<float>(_eyeRayLeftDirZList);


    /// <summary>
    /// ����̐����^����ReactiveCollection
    /// </summary>
    private ReactiveCollection<List<float>> _eyeMovementLeftHorizontalList = new ReactiveCollection<List<float>>();

    public ReactiveCollection<List<float>> EyeMovementLeftHorizontalList
    {
        get { return _eyeMovementLeftHorizontalList; }
    }

    /// <summary>
    /// ����̐����^����ReactiveCollection
    /// </summary>
    public ReactiveCollection<List<float>> eyeMovementLeftVerticalList = new ReactiveCollection<List<float>>();

    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftHorizontalList => EyeMovementLeftHorizontalList.ObserveAdd();
    public IObservable<CollectionAddEvent<List<float>>> ObserveAddEyeMovementLeftVerticalList => eyeMovementLeftVerticalList.ObserveAdd();
    
    void Start()
    {

    }


    /// <summary>
    /// CSV�f�[�^��񂲂Ƃɕ�������
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
    /// �O���t�Ƃ��ďo�͂���f�[�^���擾����
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
