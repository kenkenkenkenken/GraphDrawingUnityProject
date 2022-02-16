using System.Collections.Generic;
using UnityEngine;

public class GraphDrawingSpaceView : MonoBehaviour, IGraph
{
    /// <summary>
    /// GL�`��p�}�e���A���̃v���p�e�B
    /// </summary>
    Material IGraph.LineMaterial { get; set; }

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();
    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    List<float> IGraph.ApplicationTimeList { get; set; }

    /// <summary>
    /// �p�x�̃��X�g
    /// </summary>
    [SerializeField] private List<float> _angleList = new List<float>();
    /// <summary>
    /// �p�x�̃��X�g�̃v���p�e�B
    /// </summary>
    List<float> IGraph.AngleList { get; set; }

    /// <summary>
    /// �����ڐ���̍ŏ��l�̃v���p�e�B
    /// </summary>
    float IGraph.MinScaleLineX => 0;
    /// <summary>
    /// �����ڐ���̍ő�l
    /// </summary>
    float IGraph.MaxScaleLineX => 20;
    /// <summary>
    /// 1�����ڐ��肠����̕b���̃v���p�e�B
    /// </summary>
    float IGraph.SecondPerScaleLineX => 1;
    /// <summary>
    /// �b���̍ő�l�̃v���p�e�B
    /// </summary>
    float IGraph.MaxSecondX => IGraph.MaxScaleLineX * IGraph.SecondPerScaleLineX;

    /// <summary>
    /// �����ڐ���̍ŏ��l�̃v���p�e�B
    /// </summary>
    float IGraph.MinScaleLineY => -2;
    /// <summary>
    /// �����ڐ���̍ő�l�̃v���p�e�B
    /// </summary>
    float IGraph.MaxScaleLineY => 2;
    /// <summary>
    /// 1�����ڐ��肠����̊p�x�̃v���p�e�B
    /// </summary>
    float IGraph.AnglePerScaleLineY => 20;

    /// <summary>
    /// �O���t�`��N���X�̃C���^�[�t�F�C�X�̃v���p�e�B
    /// </summary>
    public IGraph IGraph { get; set; }


    public void Awake()
    {
        //�����I�Ɏ��������C���^�[�t�F�C�X���\�b�h���A�N���X������Ăяo�������ł��Ȃ��̂ŁA�Ăяo����悤�ɕϐ��ɑ������
        IGraph = this;
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
        if (IGraph is null) return;
        IGraph.DrawBackground();
        IGraph.DrawHorizontalScaleLine();
        IGraph.DrawHorizontalScaleCenterLine();
        IGraph.DrawVerticalScaleLine();
        IGraph.DrawVerticalScaleCenterLine();
        IGraph.DrawSeries(IGraph.ApplicationTimeList, IGraph.AngleList);
        IGraph.DrawFrameBorder();
    }

    /// <summary>
    /// // GL�`��p�}�e���A����ݒ肷��
    /// </summary>
    void IGraph.CreateLineMaterial()
    {
        if (!(IGraph.LineMaterial is null)) return;
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        IGraph.LineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        IGraph.LineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        IGraph.LineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        IGraph.LineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        IGraph.LineMaterial.SetInt("_ZWrite", 0);
    }

    /// <summary>
    /// �w�i��`��
    /// </summary>
    void IGraph.DrawBackground()
    {
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.grey);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                //����
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MaxScaleLineY, 0);
                //�E��
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, IGraph.MaxScaleLineY, 0);
                //�E��
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, IGraph.MinScaleLineY, 0);
                //����
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MinScaleLineY, 0);
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
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.cyan);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < applicationTimeList.Count; i++)
                {
                    //�ŏ��̎��Ԃ�ڐ����0�ɍ��킹��                                          //�����̕`��ʒu��20����1�ɂ���
                    IGraph.FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / IGraph.AnglePerScaleLineY, 0);

                    //�O���t�̖ڐ����20�b�𒴂����烋�[�v�𔲂���
                    if (applicationTimeList[i] >= applicationTimeList[0] + IGraph.MaxSecondX)
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
    /// �����̖ڐ����`�� 
    /// </summary>
    void IGraph.DrawHorizontalScaleLine()
    {
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.white);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                for (var i = (IGraph.MinScaleLineY + 1); i < IGraph.MaxScaleLineY; i++)
                {
                    IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, i, 0);
                    IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, i, 0);
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// �����̖ڐ����`�� 
    /// </summary>
    void IGraph.DrawVerticalScaleLine()
    {
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.white);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                for (var i = IGraph.MinScaleLineX + 1; i < IGraph.MaxScaleLineX; i++)
                {
                    IGraph.FollowAndDrawGraph(i, IGraph.MaxScaleLineY, 0);
                    IGraph.FollowAndDrawGraph(i, IGraph.MinScaleLineY, 0);
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
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.red);
            IGraph.LineMaterial.SetPass(0);

            // �f�[�^�O���t�̕`�� 
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.015f;
                float verticalCenterPosition = IGraph.MinScaleLineX + (IGraph.MaxScaleLineX - IGraph.MinScaleLineX) / 2;
                //����
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, IGraph.MaxScaleLineY, 0);
                //�E��
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, IGraph.MaxScaleLineY, 0);
                //�E��
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, IGraph.MinScaleLineY, 0);
                //����
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, IGraph.MinScaleLineY, 0);
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
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.white);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.02f;
                float horizontalCenterPosition = IGraph.MinScaleLineY + (IGraph.MaxScaleLineY - IGraph.MinScaleLineY) / 2;
                //����
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, horizontalCenterPosition + lineWidth, 0);
                //�E��
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, horizontalCenterPosition + lineWidth, 0);
                //�E��
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, horizontalCenterPosition - lineWidth, 0);
                //����
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, horizontalCenterPosition - lineWidth, 0);
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
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.green);
            IGraph.LineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                //������
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MinScaleLineY, 0);
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MaxScaleLineY, 0);

                //������
                for (var i = IGraph.MinScaleLineY; i <= 2; i += (IGraph.MaxScaleLineY - IGraph.MinScaleLineY))
                {
                    IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, i, 0);
                    IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, i, 0);
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
