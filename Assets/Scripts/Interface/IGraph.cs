using System.Collections.Generic;
using UnityEngine;

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
}
