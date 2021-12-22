using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using UnityEngine;

public class GraphDrawingStartButtonPresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingStartButtonView _graphDrawingStartButtonView;
    [SerializeField] private FileLoadingModel _fileLoadingModel;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;

    private void Start()
    {
        // view -> model

        //DrawingStartButtonを押した時に実行する
        _graphDrawingStartButtonView.OnClickDrawingStartButton.Subscribe(async _ =>
        {   
            //CSVファイルを読み込む
            await _fileLoadingModel.LoadCsv();

            //CSVの各列を各リストに追加する。
            _dataConversionForGraphModel.SetDataForCsvColumn(_fileLoadingModel.CsvDataList.ToList());
        })
        .AddTo(this.gameObject);
    }
}
