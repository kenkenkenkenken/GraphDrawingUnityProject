using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���t�`��N���X�Ɍp��������
/// </summary>
public interface IGraph
{
    /// <summary>
    /// GL�`��p�}�e���A���̃v���p�e�B
    /// </summary>
    public Material LineMaterial { get; set; }

    /// <summary>
    /// �o�ߎ��Ԃ̃��X�g�̃v���p�e�B
    /// </summary>
    public List<float> ApplicationTimeList{ get; set; }

    /// <summary>
    /// �p�x�̃��X�g�̃v���p�e�B
    /// </summary>
    public List<float> AngleList { get; set; }

    /// <summary>
    /// �����ڐ���̍ŏ��l�̃v���p�e�B
    /// </summary>
    public float MinScaleLineX { get; }
    /// <summary>
    /// �����ڐ���̍ő�l
    /// </summary>
    public float MaxScaleLineX { get; }
    /// <summary>
    /// 1�����ڐ��肠����̕b���̃v���p�e�B
    /// </summary>
    public float SecondPerScaleLineX { get; }
    /// <summary>
    /// �b���̍ő�l�̃v���p�e�B
    /// </summary>
    public float MaxSecondX { get; }

    /// <summary>
    /// �����ڐ���̍ŏ��l�̃v���p�e�B
    /// </summary>
    public float MinScaleLineY { get; }
    /// <summary>
    /// �����ڐ���̍ő�l�̃v���p�e�B
    /// </summary>
    public float MaxScaleLineY { get; }
    /// <summary>
    /// 1�����ڐ��肠����̊p�x�̃v���p�e�B
    /// </summary>
    public float AnglePerScaleLineY { get; }

    /// <summary>
    /// �O���t�`��N���X�̃C���^�[�t�F�C�X
    /// </summary>
    public IGraph IGraph { get; set; }

    /// <summary>
    /// �J�������V�[���������_�����O���ꂽ��ɌĂяo�����
    /// </summary>
    public void OnRenderObject();

    /// <summary>
    /// // GL�`��p�}�e���A����ݒ肷��
    /// </summary>
    public void CreateLineMaterial();

    /// <summary>
    /// �w�i��`��
    /// </summary>
    public void DrawBackground();

    /// <summary>
    /// �n���`��
    /// </summary>
    /// <param name="applicationTimeList">�o�ߎ��Ԃ̃��X�g</param>
    /// <param name="angleList">�p�x�̃��X�g</param>
    public void DrawSeries(List<float> applicationTimeList, List<float> angleList);

    /// <summary>
    /// ���ڐ����`�� 
    /// </summary>
    public void DrawHorizontalScaleLine();

    /// <summary>
    /// �c�ڐ����`�� 
    /// </summary>
    public void DrawVerticalScaleLine();

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
    public void DrawVerticalScaleCenterLine();

    /// <summary>
    /// �����̖ڐ���̒����̐��𑾂��`��
    /// </summary>
    public void DrawHorizontalScaleCenterLine();

    /// <summary>
    /// �g����`��
    /// </summary>
    public void DrawFrameBorder();

    /// <summary>
    /// �Q�[���I�u�W�F�N�g��Ǐ]���ăO���t��`��
    /// </summary>
    /// <param name="x">�Ǐ]�O�̕`��ʒu��x���W</param>
    /// <param name="y">�Ǐ]�O�̕`��ʒu��y���W</param>
    /// <param name="z">�Ǐ]�O�̕`��ʒu��z���W</param>
    public void FollowAndDrawGraph(float x, float y, float z);
}
