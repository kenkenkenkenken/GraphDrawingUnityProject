using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GraphDrawingPanelView : MonoBehaviour
{
    private Material _lineMaterial;

    void Start()
    {
        CreateLineMaterial();
    }

    private void OnRenderObject()
    {

        DrawHorizontalScaleLine();
        DrawHorizontalScaleCenterLine();
        DrawVerticalScaleLine();
        DrawVerticalScaleCenterLine();
        DrawSeries(timeList, dotList);
    }

    private List<float> timeList = new List<float>();
    private List<float> dotList = new List<float>();
    //�O���t�o�͗p�f�[�^��ݒ肷��
    public void SetGraphData(List<float> timeList, List<float> dotList)
    {
        this.timeList = timeList;
        this.dotList = dotList;
    }

    void CreateLineMaterial()
    {
        if (!_lineMaterial)
        {
            // GL�`��p�}�e���A���ݒ�
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
            _lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            _lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    private Color _seriesClor = new Color(0f, 163f, 255f, 1f);

    //�n���`��
    void DrawSeries(List<float> timeList, List<float> dotList)
    {
        // �O���t�`��
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", _seriesClor);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < timeList.Count; i++)
                {
                    //�ŏ��̎��Ԃ�ڐ����0�ɍ��킹�� //�����̕`��ʒu��20����1�ɂ���
                    GL.Vertex3(timeList[i] - timeList[0], dotList[i] / 20, 0.0f);

                    //�O���t�̖ڐ����20�b�𒴂����烋�[�v�𔲂���
                    if (timeList[i] >= timeList[0] + 20)
                    {
                        break;
                    }
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    //���ڐ����`��
    void DrawHorizontalScaleLine()
    {
        //1����3���C��
        // �O���t�`��
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.LINES);
            {
                for (var i = -1; i < 2; i++)
                {
                    GL.Vertex3(0, i, 0.0f);
                    GL.Vertex3(20, i, 0.0f);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    //�c�ڐ����`��
    void DrawVerticalScaleLine()
    {
        //1����19���C��
        // �O���t�`��
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.LINES);
            {
                for (var i = 1; i < 20; i++)
                {
                    GL.Vertex3(i, 2.0f, 0.0f);
                    GL.Vertex3(i, -2.0f, 0.0f);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    void DrawVerticalScaleCenterLine()
    {

        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.red);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.015f;
                int verticalCenterPosition = 10;
                GL.Vertex3(verticalCenterPosition - lineWidth, 2, 0);
                GL.Vertex3(verticalCenterPosition + lineWidth, 2, 0);
                GL.Vertex3(verticalCenterPosition + lineWidth, -2, 0);
                GL.Vertex3(verticalCenterPosition - lineWidth, -2, 0);


            }
            GL.End();
        }
        GL.PopMatrix();
    }

    void DrawHorizontalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.02f;
                int verticalCenterPosition = 0;
                GL.Vertex3(0,  verticalCenterPosition + lineWidth, 0);;
                GL.Vertex3(20, verticalCenterPosition + lineWidth, 0);
                GL.Vertex3(20, verticalCenterPosition - lineWidth, 0);
                GL.Vertex3(0,  verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }    
}

    ////LineRenderer�ł̃O���t�o�͗p
    //[SerializeField] private LineRenderer lineRenderer;
    //public void DrawGraph(List<float> timeList, List<float> dotList)
    //{


    //    // �_�̐����w�肷��
    //    lineRenderer.positionCount = dotList.Count;
    //    lineRenderer.startWidth = 0.01f;                   // �J�n�_�̑�����0.1�ɂ���
    //    lineRenderer.endWidth = 0.01f;                     // �I���_�̑�����0.1�ɂ���

    //    // ���������ꏊ����C�ɐݒ肷��
    //    //lineRenderer.SetPositions();


    //    for (int i = 0; i < timeList.Count; i++)
    //    {
    //        lineRenderer.SetPosition(i, new Vector3(timeList[i], dotList[i] / 20, 0.0f));
    //    }
    //}
//1��������Y����20�x�BX���͂P�b���Ƃ��Ă�������
//X���͐^�񒆂�0�x�� 40�x�` �}�C�i�X40�x�Ƃ��Ă�������