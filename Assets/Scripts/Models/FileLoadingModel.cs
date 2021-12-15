using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Linq;

public class FileLoadingModel : MonoBehaviour
{
    [SerializeField] private DataConversionForGraphModel dataConversionForGraphModel;
    Subject<List<string[]>> subject = new Subject<List<string[]>>();

    private void Start()
    {
        subject.Subscribe(x => dataConversionForGraphModel.SetProcessingData(x));
    }

    /// <summary>
    /// CSV�t�@�C����ǂݍ���
    /// </summary>
    /// <returns>�t�@�C���̃f�[�^�����X�g�Ɋi�[��������</returns>
    public async UniTask LoadCsv()
    {
        Debug.Log("LoadCsv()");
        //CSV�t�@�C����ǂݍ���
        var mDataList = new List<string[]>(); 
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data"); 
        //�t�@�C���̃f�[�^�����X�g�Ɋi�[����
        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null )
            {
                mDataList.Add(line.Split(',')); 
            }
        }

        subject.OnNext(mDataList);
    }


}

