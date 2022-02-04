using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Collections.ObjectModel;

public class FileLoadingModel : /*MonoBehaviour,*/ IFileLoadingModel
{
    /// <summary>
    /// CSV�̃f�[�^�̃��X�g
    /// </summary>
    private List<string[]> _csvDataList = new List<string[]>();

    /// <summary>
    /// CSV�̃f�[�^�̃��X�g�̃v���p�e�B
    /// </summary>
    public ReadOnlyCollection<string[]> CsvDataList => new ReadOnlyCollection<string[]>(_csvDataList);

    /// <summary>
    /// CSV�t�@�C����ǂ�Ń��X�g�ɒǉ�����
    /// </summary>
    public async UniTask LoadCsv()
    {   
        //���X�g����ɂ���B
        _csvDataList.Clear();

        //CSV�t�@�C����ǂݍ���
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data");
        
        //CSV�t�@�C���̃f�[�^�����X�g�ɒǉ�����
        using (var reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null )
            {
                _csvDataList.Add(line.Split(',')); 
            }
        }
    }
}
