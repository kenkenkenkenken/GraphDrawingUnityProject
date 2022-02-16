using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

public interface IDataConversionForGraphModel
{
    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> ApplicationTimeList { get; }
    /// <summary>
    /// ����̎�������x�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirXList { get; }
    /// <summary>
    /// ����̎�������y�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirYList { get; }
    /// <summary>
    /// ����̎�������z�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<float> EyeRayLeftDirZList { get; }
    /// <summary>
    /// SetDataForCsvColumn�I�����ɔ��΂���X�g���[���̃v���p�e�B
    /// </summary>
    public IObservable<Unit> OnSetDataForCsvColumn { get; }
    /// <summary>
    /// CSV�̗�̃f�[�^��ǉ�����
    /// </summary>
    /// 
    /// <param name="csvDataList">CSV�̃f�[�^�̃��X�g</param>
    public void SetDataForCsvColumn(List<string[]> csvDataList);
    /// <summary>
    /// �p�x�̃��X�g���擾����
    /// </summary>
    /// <param name="coordinateList1">���W�̃��X�g</param>
    /// <param name="coordinateList2">���W�̃��X�g</param>
    /// <returns></returns>
    public List<float> GetAngleList(List<float> coordinateList1, List<float> coordinateList2);
}
