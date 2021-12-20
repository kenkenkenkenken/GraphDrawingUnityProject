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

    public ReactiveCollection<List<string[]>> csvDataList = new ReactiveCollection<List<string[]>>();
    public IObservable<CollectionAddEvent<List<string[]>>> ObserveAddCsvDataList => csvDataList.ObserveAdd();

    private void Start()
    {
        ObserveAddCsvDataList.Subscribe(_ => dataConversionForGraphModel.SetProcessingData(csvDataList.SelectMany(x => x).ToList())).AddTo(this.gameObject);
    }

    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    /// <returns>ファイルのデータをリストに格納したもの</returns>
    public async UniTask LoadCsv()
    {
        //CSVファイルを読み込む
        var mDataList = new List<string[]>(); 
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data"); 
        //ファイルのデータをリストに格納する
        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null )
            {
                mDataList.Add(line.Split(',')); 
            }
        }

        csvDataList.Add(mDataList); 
    }


}

