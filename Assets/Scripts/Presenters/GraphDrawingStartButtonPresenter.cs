using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class GraphDrawingStartButtonPresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingStartButtonView _graphDrawingStartButtonView;
    [SerializeField] private IFileLoadingModel _fileLoadingModel;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;

    [Inject]
    private void Construct( 
        //GraphDrawingStartButtonView graphDrawingStartButtonView,
        IFileLoadingModel fileLoadingModel
        //DataConversionForGraphModel dataConversionForGraphModel
    )
    {
        //_graphDrawingStartButtonView = graphDrawingStartButtonView;
        _fileLoadingModel = fileLoadingModel;
        //_dataConversionForGraphModel = dataConversionForGraphModel;
    }

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
