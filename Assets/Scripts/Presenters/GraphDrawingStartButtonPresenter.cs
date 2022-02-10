using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class GraphDrawingStartButtonPresenter: IInitializable
{
    [SerializeField] private GraphDrawingStartButtonView _graphDrawingStartButtonView;
    [SerializeField] private IFileLoadingModel _fileLoadingModel;
    [SerializeField] private IDataConversionForGraphModel _dataConversionForGraphModel;

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
        });
    }
}
