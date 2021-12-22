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

        //DrawingStartButton�����������Ɏ��s����
        _graphDrawingStartButtonView.OnClickDrawingStartButton.Subscribe(async _ =>
        {   
            //CSV�t�@�C����ǂݍ���
            await _fileLoadingModel.LoadCsv();

            //CSV�̊e����e���X�g�ɒǉ�����B
            _dataConversionForGraphModel.SetDataForCsvColumn(_fileLoadingModel.CsvDataList.ToList());
        })
        .AddTo(this.gameObject);
    }
}
