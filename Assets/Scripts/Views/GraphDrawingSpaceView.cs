using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpaceView : MonoBehaviour
{
    private Material _lineMaterial;

    void Start()
    {
        CreateLineMaterial();
    }

    private void OnRenderObject()
    {
        DrawBackground();
        DrawHorizontalScaleLine();
        DrawHorizontalScaleCenterLine();
        DrawVerticalScaleLine();
        DrawVerticalScaleCenterLine();
        DrawSeries(timeList, dotList);
        DrawFrameBorder();
    }

    private List<float> timeList = new List<float>();
    private List<float> dotList = new List<float>();
    //�O���t�o�͗p�f�[�^��ݒ肷��
    public void SetGraphData(List<float> timeList, List<float> dotList)
    {
        this.timeList = timeList;
        this.dotList = dotList;
    }
    /// <summary>
    /// // GL�`��p�}�e���A���ݒ�
    /// </summary>
    void CreateLineMaterial()
    {
        if (!_lineMaterial)
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
            _lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            _lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    /// <summary>
    /// �w�i��`��
    /// </summary>
    void DrawBackground()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.grey);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                float lineWidth = 2f;
                int verticalCenterPosition = 0;
                FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(20, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(20, verticalCenterPosition - lineWidth, 0);
                FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }
 
    /// <summary>
    /// �n���`��
    /// </summary>
    /// <param name="timeList">x���̃f�[�^</param>
    /// <param name="dotList">y���̃f�[�^</param>
    void DrawSeries(List<float> timeList, List<float> dotList)
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.cyan);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < timeList.Count; i++)
                {
                    //�ŏ��̎��Ԃ�ڐ����0�ɍ��킹�� //�����̕`��ʒu��20����1�ɂ���
                    FollowAndDrawGraph(timeList[i] - timeList[0], dotList[i] / 20, 0.0f);

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

    /// <summary>
    /// ���ڐ����`�� 
    /// </summary>
    void DrawHorizontalScaleLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                for (var i = -1; i < 2; i++)
                {
                    FollowAndDrawGraph(0, i, 0);
                    FollowAndDrawGraph(20, i, 0);
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �c�ڐ����`�� 
    /// </summary>
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
                    FollowAndDrawGraph(i, 2, 0);
                    FollowAndDrawGraph(i, -2, 0);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
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
                FollowAndDrawGraph(verticalCenterPosition - lineWidth, 2, 0);
                FollowAndDrawGraph(verticalCenterPosition + lineWidth, 2, 0);
                FollowAndDrawGraph(verticalCenterPosition + lineWidth, -2, 0);
                FollowAndDrawGraph(verticalCenterPosition - lineWidth, -2, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
    void DrawHorizontalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.02f;
                int verticalCenterPosition = 0;
                FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(20, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(20, verticalCenterPosition - lineWidth, 0);
                FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }
    
    /// <summary>
    /// �g����`��
    /// </summary>
    void DrawFrameBorder()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.green);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                //�c��
                FollowAndDrawGraph(0, -2, 0);
                FollowAndDrawGraph(0, 2, 0);

                //����
                for (var i = -2; i <= 2; i += 4)
                {
                    FollowAndDrawGraph(0, i, 0);
                    FollowAndDrawGraph(20, i, 0);
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �Q�[���I�u�W�F�N�g��Ǐ]���ăO���t��`��
    /// </summary>
    /// <param name="x">�Ǐ]�O�̕`��ʒu��x���W</param>
    /// <param name="y">�Ǐ]�O�̕`��ʒu��y���W</param>
    /// <param name="z">�Ǐ]�O�̕`��ʒu��z���W</param>
    void FollowAndDrawGraph(float x, float y, float z)
    {
        GL.Vertex3
        (
            x + transform.position.x,
            y + transform.position.y,
            z + transform.position.z
        );
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