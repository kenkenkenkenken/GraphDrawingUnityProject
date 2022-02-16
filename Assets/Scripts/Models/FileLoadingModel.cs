using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;
using UniRx;

public class FileLoadingModel : IFileLoadingModel
{
    /// <summary>
    /// CSVのデータのリスト
    /// </summary>
    private List<string[]> _csvDataList = new List<string[]>();
    /// <summary>
    /// CSVのデータのリストのプロパティ
    /// </summary>
    public ReadOnlyCollection<string[]> CsvDataList => _csvDataList.AsReadOnly();


    /// <summary>
    /// CSVファイルを読んでリストに追加する
    /// </summary>
    public async UniTask LoadCsv()
    {   
        //リストを空にする。
        _csvDataList.Clear();

        //CSVファイルを読み込む
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data");
        
        //CSVファイルのデータをリストに追加する
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
