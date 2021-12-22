using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpaceView : MonoBehaviour
{
    /// <summary>
    /// GL�`��p�}�e���A��
    /// </summary>
    [SerializeField] private Material _lineMaterial;

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    public List<float> ApplicationTimeList
    {
        get { return _applicationTimeList; }
        set { _applicationTimeList = value; }
    }

    /// <summary>
    /// �p�x�̃��X�g
    /// </summary>
    [SerializeField] private List<float> _angleList = new List<float>();

    /// <summary>
    /// �p�x�̃��X�g�̃v���p�e�B
    /// </summary>
    public List<float> AngleList
    {
        get { return _angleList; }
        set { _angleList = value; }
    }

    private void Start()
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
        DrawSeries(_applicationTimeList, _angleList);
        DrawFrameBorder();
    }

    /// <summary>
    /// // GL�`��p�}�e���A����ݒ肷��
    /// </summary>
    private void CreateLineMaterial()
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
    private void DrawBackground()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.grey);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                float lineWidth = 2;
                float verticalCenterPosition = 0;
                float graphWidth = 20;

                FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �n���`��
    /// </summary>
    /// <param name="applicationTimeList">�o�ߎ��Ԃ̃��X�g</param>
    /// <param name="angleList">�p�x�̃��X�g</param>
    private void DrawSeries(List<float> applicationTimeList, List<float> angleList)
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.cyan);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < applicationTimeList.Count; i++)
                {
                                    //�ŏ��̎��Ԃ�ڐ����0�ɍ��킹��                 //�����̕`��ʒu��20����1�ɂ���
                    FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / 20, 0);

                    //�O���t�̖ڐ����20�b�𒴂����烋�[�v�𔲂���
                    if (applicationTimeList[i] >= applicationTimeList[0] + 20)
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
    private void DrawHorizontalScaleLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                float graphWidth = 20;
                for (var i = -1; i < 2; i++)
                {
                    FollowAndDrawGraph(0, i, 0);
                    FollowAndDrawGraph(graphWidth, i, 0);
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �c�ڐ����`�� 
    /// </summary>
    private void DrawVerticalScaleLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

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
    private void DrawVerticalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.red);
            _lineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`��
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.015f;
                float verticalCenterPosition = 10;
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
    private void DrawHorizontalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.02f;
                float verticalCenterPosition = 0;
                float graphWidth = 20;
                FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �g����`��
    /// </summary>
    private void DrawFrameBorder()
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

                float graphWidth = 20;

                //����
                for (var i = -2; i <= 2; i += 4)
                {
                    FollowAndDrawGraph(0, i, 0);
                    FollowAndDrawGraph(graphWidth, i, 0);
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
    private void FollowAndDrawGraph(float x, float y, float z)
    {
        GL.Vertex3
        (
            x + transform.position.x,
            y + transform.position.y,
            z + transform.position.z
        );
    }
}
