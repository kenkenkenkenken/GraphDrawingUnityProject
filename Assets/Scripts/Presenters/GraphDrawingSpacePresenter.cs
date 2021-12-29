using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpacePresenter : MonoBehaviour
{

    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftHorizontalGdsv;
    [SerializeField] private GraphDrawingSpaceView _eyeMovementLeftVerticalGdsv;
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
            DrawGraph(_eyeMovementLeftHorizontalGdsv, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList(), _eyeMovementLeftHorizontalCanvas);
            
            //����̐����^���̃O���t�`��
            DrawGraph(_eyeMovementLeftVerticalGdsv, _dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList(), _eyeMovementLeftVerticalCanvas);
        })
        .AddTo(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphView">�O���t�`���View</param>
    /// <param name="coordinateList1">���W</param>
    /// <param name="coordinateList2">���W</param>
    /// <param name="canvas">�O���t�`��̃Q�[���I�u�W�F�N�g</param>
    void DrawGraph(GraphDrawingSpaceView graphView, List<float> coordinateList1, List<float> coordinateList2, GameObject canvas)
    {
        //�O���tView�ɁA�o�ߎ��Ԃ̃��X�g��ǉ�����B
        graphView.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

        //�O���tView�ɁA�p�x�̃��X�g��ǉ�����        //���W�̃��X�g��2�n���āA�p�x�̃��X�g���擾����
        graphView.AngleList = _dataConversionForGraphModel.GetAngleList(coordinateList1, coordinateList2);

        //�O���tView�́AGL�`��p�}�e���A����ݒ肷��
        graphView.CreateLineMaterial();

        //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ��鎖�ŁAOnRenderObject()�����s����A�O���t���`�悳���B
        canvas.SetActive(true);
    }
}
