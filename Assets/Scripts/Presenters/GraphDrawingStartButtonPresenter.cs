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
        _graphDrawingStartButtonView.OnClickDrawingStartButton
            .Subscribe(async _ =>
            {
                await _fileLoadingModel.LoadCsv();
                _dataConversionForGraphModel.DivideCsvDataByColumn(_fileLoadingModel.CsvDataList);

                //左眼の水平運動のリストを空にする。
                _dataConversionForGraphModel.EyeMovementLeftHorizontalList.Clear();

                //左眼の水平運動のリストに追加する。
                _dataConversionForGraphModel.EyeMovementLeftHorizontalList.Add(
                    //左目（左右）zxの、グラフ出力データを取得する。
                    _dataConversionForGraphModel.GetGraphData(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList()));

                //左眼の垂直運動のリストを空にする。
                _dataConversionForGraphModel.eyeMovementLeftVerticalList.Clear();

                //左眼の垂直運動のリストに追加する。
                _dataConversionForGraphModel.eyeMovementLeftVerticalList.Add(
                    //左目（上下）zyの、グラフ出力データを取得する。
                    _dataConversionForGraphModel.GetGraphData(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList()));
            })
            .AddTo(this.gameObject);

        // model -> view 
    }
}
