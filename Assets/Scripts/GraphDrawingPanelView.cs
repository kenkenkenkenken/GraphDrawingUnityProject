using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GraphDrawingPanelView : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;


    void Start()
    {
        
    }

    public void DrawGraph(List<float> timeList, List<float> dotList)
    {


        // �_�̐����w�肷��
        lineRenderer.positionCount = dotList.Count;


        // ���������ꏊ����C�ɐݒ肷��
        //lineRenderer.SetPositions();


        for (int i = 0; i < timeList.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(timeList[i], dotList[i], 0.0f));
        }
    }
}
//1��������Y����20�x�BX���͂P�b���Ƃ��Ă�������
//X���͐^�񒆂�0�x�� 40�x�` �}�C�i�X40�x�Ƃ��Ă�������