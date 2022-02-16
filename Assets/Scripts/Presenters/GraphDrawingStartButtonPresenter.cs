using System;
using System.Linq;
using UniRx;
using Zenject;

public class GraphDrawingStartButtonPresenter: IInitializable, IDisposable
{
    CompositeDisposable _compositeDisposable = new CompositeDisposable();

    private GraphDrawingStartButtonView _graphDrawingStartButtonView;
    private IFileLoadingModel _fileLoadingModel;
    private IDataConversionForGraphModel _dataConversionForGraphModel;

    public GraphDrawingStartButtonPresenter(
        GraphDrawingStartButtonView graphDrawingStartButtonView,
        IFileLoadingModel fileLoadingModel,
        IDataConversionForGraphModel dataConversionForGraphModel
    )
    {
        _graphDrawingStartButtonView = graphDrawingStartButtonView;
        _fileLoadingModel = fileLoadingModel;
        _dataConversionForGraphModel = dataConversionForGraphModel;
    }
                                                                                                 
    void IInitializable.Initialize()
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
        .AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
