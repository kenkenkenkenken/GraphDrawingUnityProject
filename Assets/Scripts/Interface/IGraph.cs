using System.Collections.Generic;
using UnityEngine;

public interface IGraph
{
    /// <summary>
    /// GL描画用マテリアルのプロパティ
    /// </summary>
    public Material LineMaterial { get; set; }

    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public List<float> ApplicationTimeList{ get; set; }

    /// <summary>
    /// 角度のリストのプロパティ
    /// </summary>
    public List<float> AngleList { get; set; }
}
