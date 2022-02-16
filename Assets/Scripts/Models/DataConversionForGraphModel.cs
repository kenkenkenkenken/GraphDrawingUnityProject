using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using static CsvDataListIndex;

public class DataConversionForGraphModel : IDataConversionForGraphModel
{
    #region Field
    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g
    /// </summary>
    private List<float> _applicationTimeList = new List<float>();
    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList => _applicationTimeList.AsReadOnly();

    /// <summary>
    /// ����̎�������x�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirXList = new List<float>();
    /// <summary>
    /// ����̎�������x�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList => _eyeRayLeftDirXList.AsReadOnly();

    /// <summary>
    /// ����̎�������y�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirYList = new List<float>();
    /// <summary>
    /// ����̎�������y�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList => _eyeRayLeftDirYList.AsReadOnly();

    /// <summary>
    /// ����̎�������z�̃��X�g
    /// </summary>
    private List<float> _eyeRayLeftDirZList = new List<float>();
    /// <summary>
    /// ����̎�������z�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList => _eyeRayLeftDirZList.AsReadOnly();

    /// <summary>
    /// SetDataForCsvColumn�I�����ɔ��΂���X�g���[��
    /// </summary>
    private Subject<Unit> _onSetDataForCsvColumn = new Subject<Unit>();
    /// <summary>
    /// SetDataForCsvColumn�I�����ɔ��΂���X�g���[���̃v���p�e�B
    /// </summary>
    public IObservable<Unit> OnSetDataForCsvColumn => _onSetDataForCsvColumn;
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
            _applicationTimeList.Add(float.Parse(csvDataList[i][(int)ApplicationTime]));
            _eyeRayLeftDirXList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirX]));
            _eyeRayLeftDirYList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirY]));
            _eyeRayLeftDirZList.Add(float.Parse(csvDataList[i][(int)EyeRayLeftDirZ]));
        }
        _onSetDataForCsvColumn.OnNext(Unit.Default);
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
