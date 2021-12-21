using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Linq;
using System.Collections.ObjectModel;

public class FileLoadingModel : MonoBehaviour
{
    private List<string[]> csvDataList = new List<string[]>();

    public ReadOnlyCollection<string[]> CsvDataList => new ReadOnlyCollection<string[]>(csvDataList);

    /// <summary>
    /// CSV�t�@�C����ǂ݃��X�g�Ɋi�[����B
    /// </summary>
    public async UniTask LoadCsv()
    {
        //CSV�t�@�C����ǂݍ���
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data");
        
        //�t�@�C���̃f�[�^�����X�g�Ɋi�[����
        using (var reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null )
            {
                csvDataList.Add(line.Split(',')); 
            }
        }
    }
}
