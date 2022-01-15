using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawingSpaceView : MonoBehaviour, IGraph
{



    /// <summary>
    /// GL�`��p�}�e���A��
    /// </summary>
    private Material _lineMaterial;

    /// <summary>
    /// GL�`��p�}�e���A���̃v���p�e�B
    /// </summary>
    public Material LineMaterial
    {
        get { return _lineMaterial; }
        set { _lineMaterial = value; }
    }

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

    /// <summary>
    /// �O���t�`��N���X�̃C���^�[�t�F�C�X
    /// </summary>
    private IGraph iGraph;

    private void Awake()
    {
        //�����I�Ɏ��������C���^�[�t�F�C�X���\�b�h���A�N���X������Ăяo�������ł��Ȃ��̂ŁA�Ăяo����悤�ɕϐ��ɑ������
        iGraph = (IGraph)gameObject.GetComponent<GraphDrawingSpaceView>();
    }
    /// <summary>
    /// �J�������V�[���������_�����O���ꂽ��ɌĂяo�����
    /// </summary>
    /// <remarks>
    /// Unity�C�x���g���\�b�h�Ȃ̂ŁA�C���^�[�t�F�C�X���\�b�h�̖����I�������ł��Ȃ��̂�public�̂܂܂ɂ��Ă���
    /// </remarks>
    /// 
    public void OnRenderObject()
    {
        iGraph.DrawBackground();
        iGraph.DrawHorizontalScaleLine();
        iGraph.DrawHorizontalScaleCenterLine();
        iGraph.DrawVerticalScaleLine();
        iGraph.DrawVerticalScaleCenterLine();
        iGraph.DrawSeries(_applicationTimeList, _angleList);
        iGraph.DrawFrameBorder();
    }

    /// <summary>
    /// // GL�`��p�}�e���A����ݒ肷��
    /// </summary>
    void IGraph.CreateLineMaterial()
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
    void IGraph.DrawBackground()
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

                iGraph.FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                iGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                iGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                iGraph.FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
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
    void IGraph.DrawSeries(List<float> applicationTimeList, List<float> angleList)
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
                    iGraph.FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / 20, 0);

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
    void IGraph.DrawHorizontalScaleLine()
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
                    iGraph.FollowAndDrawGraph(0, i, 0);
                    iGraph.FollowAndDrawGraph(graphWidth, i, 0);
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �c�ڐ����`�� 
    /// </summary>
    void IGraph.DrawVerticalScaleLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                for (var i = 1; i < 20; i++)
                {
                    iGraph.FollowAndDrawGraph(i, 2, 0);
                    iGraph.FollowAndDrawGraph(i, -2, 0);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
    void IGraph.DrawVerticalScaleCenterLine()
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
                iGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, 2, 0);
                iGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, 2, 0);
                iGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, -2, 0);
                iGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, -2, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
    void IGraph.DrawHorizontalScaleCenterLine()
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
                iGraph.FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                iGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                iGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                iGraph.FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �g����`��
    /// </summary>
    void IGraph.DrawFrameBorder()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.green);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                //�c��
                iGraph.FollowAndDrawGraph(0, -2, 0);
                iGraph.FollowAndDrawGraph(0, 2, 0);

                float graphWidth = 20;

                //����
                for (var i = -2; i <= 2; i += 4)
                {
                    iGraph.FollowAndDrawGraph(0, i, 0);
                    iGraph.FollowAndDrawGraph(graphWidth, i, 0);
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
    void IGraph.FollowAndDrawGraph(float x, float y, float z)
    {
        GL.Vertex3
        (
            x + transform.position.x,
            y + transform.position.y,
            z + transform.position.z
        );
    }
}
