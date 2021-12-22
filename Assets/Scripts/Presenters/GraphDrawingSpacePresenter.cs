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
            //����̐����^���̃O���tView�ɁA�o�ߎ��Ԃ̃��X�g��ǉ�����B
            _eyeMovementLeftHorizontalGdsv.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

            //����̐����^���̃O���tView�ɁA����̐����^���̃��X�g��ǉ�����        //���ځi���E�jzx�̃��X�g��n���āA����̐����^���̃��X�g�擾����
            _eyeMovementLeftHorizontalGdsv.AngleList = _dataConversionForGraphModel.GetAngleList(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList());

            //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ��鎖�ŁAOnRenderObject()�����s����A�O���t���`�悳���B
            _eyeMovementLeftHorizontalCanvas.SetActive(true);


            //����̐����^���̃O���tView�ɁA�o�ߎ��Ԃ̃��X�g��ǉ�����B
            _eyeMovementLeftVerticalGdsv.ApplicationTimeList = _dataConversionForGraphModel.ApplicationTimeList.ToList();

            //����̐����^���̃O���tView�ɁA����̐����^���̃��X�g��ǉ�����      //���ځi�㉺�jzy�̃f�[�^��n���āA����̐����^���̃��X�g���擾����B
            _eyeMovementLeftVerticalGdsv.AngleList = _dataConversionForGraphModel.GetAngleList(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList());

            //�Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ��鎖�ŁAOnRenderObject()�����s����A�O���t���`�悳���B
            _eyeMovementLeftVerticalCanvas.SetActive(true);
        })
        .AddTo(this.gameObject);
    }
}
