using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class DataConversionForGraphModel : MonoBehaviour
{
    #region Field
    /// <summary>
    /// CSV�̃f�[�^���ƑΉ�����񐔖�
    /// </summary>
    private enum CsvDataListIndex
    {
        ApplicationTime = 1, //�o�ߎ���
        EyeRayLeftDirX = 24, //����̎������� x
        EyeRayLeftDirY = 25, //����̎������� y
        EyeRayLeftDirZ = 26, //����̎������� z
    }

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => new ReadOnlyCollection<float>(_applicationTimeList);

    /// <summary>
    /// ����̎�������x�̃��X�g
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirXList = new List<float>();

    /// <summary>
    /// ����̎�������x�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => new ReadOnlyCollection<float>(_eyeRayLeftDirXList);

    /// <summary>
    /// ����̎�������y�̃��X�g
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirYList = new List<float>();

    /// <summary>
    /// ����̎�������y�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => new ReadOnlyCollection<float>(_eyeRayLeftDirYList);

    /// <summary>
    /// ����̎�������z�̃��X�g
    /// </summary>
    [SerializeField]  private List<float> _eyeRayLeftDirZList = new List<float>();

    /// <summary>
    /// ����̎�������z�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => new ReadOnlyCollection<float>(_eyeRayLeftDirZList);

    /// <summary>
    /// SetCsvDataForEachColumn�I�����ɔ��΂���X�g���[��
    /// </summary>
    private Subject<Unit> _onSetCsvDataForEachColumn = new Subject<Unit>();

    /// <summary>
    /// SetCsvDataForEachColumn�I�����ɔ��΂���X�g���[���̃v���p�e�B
    /// </summary>
    public IObservable<Unit> OnSetCsvDataForEachColumn => _onSetCsvDataForEachColumn;
    #endregion

    /// <summary>
    /// CSV�̗�̃f�[�^��ǉ�����
    /// </summary>
    /// 
    /// <param name="csvDataList">CSV�̃f�[�^�̃��X�g</param>
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
    /// �p�x�̃��X�g���擾����
    /// </summary>
    /// <param name="coordinateList1">���W�̃��X�g</param>
    /// <param name="coordinateList2">���W�̃��X�g</param>
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
