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
    /// CSVファイルを読みリストに格納する。
    /// </summary>
    public async UniTask LoadCsv()
    {
        //CSVファイルを読み込む
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data");
        
        //ファイルのデータをリストに格納する
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
