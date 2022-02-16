using System.Collections.Generic;
using UnityEngine;

public class GraphDrawingSpaceView : MonoBehaviour, IGraph
{
    /// <summary>
    /// GL描画用マテリアルのプロパティ
    /// </summary>
    Material IGraph.LineMaterial { get; set; }

    /// <summary>
    /// 経過時間のリスト
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();
    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    List<float> IGraph.ApplicationTimeList { get; set; }

    /// <summary>
    /// 角度のリスト
    /// </summary>
    [SerializeField] private List<float> _angleList = new List<float>();
    /// <summary>
    /// 角度のリストのプロパティ
    /// </summary>
    List<float> IGraph.AngleList { get; set; }

    /// <summary>
    /// 水平目盛りの最小値のプロパティ
    /// </summary>
    float IGraph.MinScaleLineX => 0;
    /// <summary>
    /// 水平目盛りの最大値
    /// </summary>
    float IGraph.MaxScaleLineX => 20;
    /// <summary>
    /// 1水平目盛りあたりの秒数のプロパティ
    /// </summary>
    float IGraph.SecondPerScaleLineX => 1;
    /// <summary>
    /// 秒数の最大値のプロパティ
    /// </summary>
    float IGraph.MaxSecondX => IGraph.MaxScaleLineX * IGraph.SecondPerScaleLineX;

    /// <summary>
    /// 垂直目盛りの最小値のプロパティ
    /// </summary>
    float IGraph.MinScaleLineY => -2;
    /// <summary>
    /// 垂直目盛りの最大値のプロパティ
    /// </summary>
    float IGraph.MaxScaleLineY => 2;
    /// <summary>
    /// 1垂直目盛りあたりの角度のプロパティ
    /// </summary>
    float IGraph.AnglePerScaleLineY => 20;

    /// <summary>
    /// グラフ描画クラスのインターフェイスのプロパティ
    /// </summary>
    public IGraph IGraph { get; set; }


    public void Awake()
    {
        //明示的に実装したインターフェイスメソッドを、クラス内から呼び出す事ができないので、呼び出せるように変数に代入する
        IGraph = this;
    }

    /// <summary>
    /// カメラがシーンをレンダリングされた後に呼び出される
    /// </summary>
    /// <remarks>
    /// Unityイベントメソッドなので、インターフェイスメソッドの明示的実装ができないのでpublicのままにしている
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
    /// // GL描画用マテリアルを設定する
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
    /// 背景を描く
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
                //左上
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MaxScaleLineY, 0);
                //右上
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, IGraph.MaxScaleLineY, 0);
                //右下
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, IGraph.MinScaleLineY, 0);
                //左下
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MinScaleLineY, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// 系列を描く
    /// </summary>
    /// <param name="applicationTimeList">経過時間のリスト</param>
    /// <param name="angleList">角度のリスト</param>
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
                    //最初の時間を目盛りの0に合わせる                                          //垂直の描画位置を20分の1にする
                    IGraph.FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / IGraph.AnglePerScaleLineY, 0);

                    //グラフの目盛りの20秒を超えたらループを抜ける
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
    /// 水平の目盛りを描く 
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
    /// 垂直の目盛りを描く 
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
    /// 垂直の目盛りの中央の線を太く描く
    /// </summary>
    void IGraph.DrawVerticalScaleCenterLine()
    {
        GL.Flush();
        GL.PushMatrix();
        {
            IGraph.LineMaterial.SetColor("_Color", Color.red);
            IGraph.LineMaterial.SetPass(0);

            // データグラフの描画 
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.015f;
                float verticalCenterPosition = IGraph.MinScaleLineX + (IGraph.MaxScaleLineX - IGraph.MinScaleLineX) / 2;
                //左上
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, IGraph.MaxScaleLineY, 0);
                //右上
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, IGraph.MaxScaleLineY, 0);
                //右下
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, IGraph.MinScaleLineY, 0);
                //左下
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, IGraph.MinScaleLineY, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// 水平の目盛りの中央の線を太く描く
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
                //左上
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, horizontalCenterPosition + lineWidth, 0);
                //右上
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, horizontalCenterPosition + lineWidth, 0);
                //右下
                IGraph.FollowAndDrawGraph(IGraph.MaxScaleLineX, horizontalCenterPosition - lineWidth, 0);
                //左下
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, horizontalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    /// <summary>
    /// 枠線を描く
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
                //垂直線
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MinScaleLineY, 0);
                IGraph.FollowAndDrawGraph(IGraph.MinScaleLineX, IGraph.MaxScaleLineY, 0);

                //水平線
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
    /// ゲームオブジェクトを追従してグラフを描く
    /// </summary>
    /// <param name="x">追従前の描画位置のx座標</param>
    /// <param name="y">追従前の描画位置のy座標</param>
    /// <param name="z">追従前の描画位置のz座標</param>
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
