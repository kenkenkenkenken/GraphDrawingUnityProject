using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpacePresenter : MonoBehaviour
{
    /// <summary>
    /// ����̐����^���̃O���t�`���View
    /// </summary>
    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftHorizontalGdsv;

    /// <summary>
    /// ����̐����^���̃O���t�`���View�̃v���p�e�B
    /// </summary>
    ///<remarks>
    /// get����IGraph�ɕϊ�����
    ///</remarks>
    public IGraph IgraphEyeMovementLeftHorizontal
    {
        get { return (IGraph)_eyeMovementLeftHorizontalGdsv; }
    }

    /// <summary>
    /// ����̐����^���̃O���t�`���View
    /// </summary>
    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftVerticalGdsv;

    /// <summary>
    /// ����̐����^���̃O���t�`���View�̃v���p�e�B
    /// </summary>
    ///<remarks>
    /// get����IGraph�ɕϊ�����
    ///</remarks>
    public IGraph IgraphEyeMovementLeftVertical
    {
        get { return (IGraph)_eyeMovementLeftVerticalGdsv; }
    }

    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;
    [SerializeField] private GameObject _eyeMovementLeftHorizontalCanvas;
    [SerializeField] private GameObject _eyeMovementLeftVerticalCanvas;



    void Start()
    {
        //model-> view

        //SetCsvDataForEachColumn�I�����Ɏ��s����
        _dataConversionForGraphModel.OnSetCsvDataForEachColumn.Subscribe(_ =>
        {
            //����̐����^���̃O���t�`��
            PreprocessToDrawGraph(IgraphEyeMovementLeftHorizontal, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList(), _eyeMovementLeftHorizontalCanvas);

            //����̐����^���̃O���t�`��
            PreprocessToDrawGraph(IgraphEyeMovementLeftVertical, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList(), _eyeMovementLeftVerticalCanvas);
        })
        .AddTo(this.gameObject);
    }

    /// <summary>
    /// �O���t�`��̑O�������s��
    /// </summary>
    /// <param name="graphView">�O���t�`���View</param>
    /// <param name="coordinateList1">���W</param>
    /// <param name="coordinateList2">���W</param>
    /// <param name="canvas">�O���t�`��̃Q�[���I�u�W�F�N�g</param>
    void PreprocessToDrawGraph(IGraph graphView, List<float> coordinateList1, List<float> coordinateList2, GameObject canvas)
    {
        //�O���tView�ɁA�o�ߎ��Ԃ̃��X�g��ǉ�����B
        graphView.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

        //�O���tView�ɁA�p�x�̃��X�g��ǉ�����        //���W�̃��X�g��2�n���āA�p�x�̃��X�g���擾����
        graphView.AngleList = _dataConversionForGraphModel.GetAngleList(coordinateList1, coordinateList2);

        ////�O���tView�́AGL�`��p�}�e���A����ݒ肷��
        graphView.CreateLineMaterial();

        //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ��鎖�ŁAOnRenderObject()�����s����A�O���t���`�悳���B
        canvas.SetActive(true);
    }
}
