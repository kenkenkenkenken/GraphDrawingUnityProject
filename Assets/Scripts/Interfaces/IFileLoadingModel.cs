using Cysharp.Threading.Tasks;
using System.Collections.ObjectModel;

public interface IFileLoadingModel
{
    /// <summary>
    /// CSV�̃f�[�^�̃��X�g�̃v���p�e�B
    /// </summary>
    /// 
    public ReadOnlyCollection<string[]> CsvDataList { get; }

    /// <summary>
    /// CSV�t�@�C����ǂ�Ń��X�g�ɒǉ�����
    /// </summary>
    public UniTask LoadCsv();
}
