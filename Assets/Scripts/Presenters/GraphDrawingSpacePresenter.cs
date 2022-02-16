using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using static GraphDrawingSpaceViewType;
using static RectTransformType;

public class GraphDrawingSpacePresenter : IInitializable
{
    #region Field
    /// <summary>
    /// ����̐����^���̃O���t�`���View
    /// </summary>
    private IGraph _eyeMovementLeftHorizontal;

    /// <summary>
    /// ����̐����^���̃O���t�`���View�̃v���p�e�B
    /// </summary>
    ///<remarks>
    /// get����IGraph�ɕϊ�����
    ///</remarks>
    public IGraph IgraphEyeMovementLeftHorizontal
    {
        get { return _eyeMovementLeftHorizontal; }
    }

    /// <summary>
    /// ����̐����^���̃O���t�`���View
    /// </summary>
    private IGraph _eyeMovementLeftVertical;

    /// <summary>
    /// ����̐����^���̃O���t�`���View�̃v���p�e�B
    /// </summary>
    ///<remarks>
    /// get����IGraph�ɕϊ�����
    ///</remarks>
    public IGraph IgraphEyeMovementLeftVertical
    {
        get { return _eyeMovementLeftVertical; }
    }

    private IDataConversionForGraphModel _dataConversionForGraphModel;
    private RectTransform _eyeMovementLeftHorizontalCanvas;
    private RectTransform _eyeMovementLeftVerticalCanvas;
    #endregion

    public GraphDrawingSpacePresenter(
        [Inject(Id = EyeMovementLeftHorizontalCanvas)]
        RectTransform eyeMovementLeftHorizontalCanvas,
        [Inject(Id = EyeMovementLeftVerticalCanvas)]
        RectTransform eyeMovementLeftVerticalCanvas,

        [Inject(Id = EyeMovementLeftHorizontal)]
        IGraph eyeMovementLeftHorizontal,
        [Inject(Id = EyeMovementLeftVertical)]
        IGraph eyeMovementLeftVertical,

        IDataConversionForGraphModel dataConversionForGraphModel
    )
    {
        _eyeMovementLeftHorizontal = eyeMovementLeftHorizontal;
        _eyeMovementLeftVertical = eyeMovementLeftVertical;
        _dataConversionForGraphModel = dataConversionForGraphModel;
        _eyeMovementLeftHorizontalCanvas = eyeMovementLeftHorizontalCanvas;
        _eyeMovementLeftVerticalCanvas = eyeMovementLeftVerticalCanvas;
    }

    void IInitializable.Initialize()
    {
        //model-> view

        //SetDataForCsvColumn�I�����Ɏ��s����
        _dataConversionForGraphModel.OnSetDataForCsvColumn.Subscribe(_ =>
        {
            //����̐����^���̃O���t�`��
            PreprocessToDrawGraph(IgraphEyeMovementLeftHorizontal, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList(), _eyeMovementLeftHorizontalCanvas);

            //����̐����^���̃O���t�`��
            PreprocessToDrawGraph(IgraphEyeMovementLeftVertical, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList(), _eyeMovementLeftVerticalCanvas);
        });
    }

    /// <summary>
    /// �O���t�`��̑O�������s��
    /// </summary>
    /// <param name="graphView">�O���t�`���View</param>
    /// <param name="coordinateList1">���W</param>
    /// <param name="coordinateList2">���W</param>
    /// <param name="canvas">�O���t�`��̃Q�[���I�u�W�F�N�g</param>
    void PreprocessToDrawGraph(IGraph graphView, List<float> coordinateList1, List<float> coordinateList2, RectTransform canvas)
    {
        //�O���tView�ɁA�o�ߎ��Ԃ̃��X�g��ǉ�����B
        graphView.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

        //�O���tView�ɁA�p�x�̃��X�g��ǉ�����        //���W�̃��X�g��2�n���āA�p�x�̃��X�g���擾����
        graphView.AngleList = _dataConversionForGraphModel.GetAngleList(coordinateList1, coordinateList2);

        //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ��鎖�ŁAOnRenderObject()�����s����A�O���t���`�悳���B
        canvas.gameObject.SetActive(true);

        //GL�`��p�}�e���A����ݒ肷��
        graphView.CreateLineMaterial();
    }
}
