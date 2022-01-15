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
    /// GL描画用マテリアル
    /// </summary>
    private Material _lineMaterial;

    /// <summary>
    /// GL描画用マテリアルのプロパティ
    /// </summary>
    public Material LineMaterial
    {
        get { return _lineMaterial; }
        set { _lineMaterial = value; }
    }

    /// <summary>
    /// 経過時間のリスト
    /// </summary>
    [SerializeField] private List<float> _applicationTimeList = new List<float>();

    /// <summary>
    /// 経過時間のリストのプロパティ
    /// </summary>
    public List<float> ApplicationTimeList
    {
        get { return _applicationTimeList; }
        set { _applicationTimeList = value; }
    }

    /// <summary>
    /// 角度のリスト
    /// </summary>
    [SerializeField] private List<float> _angleList = new List<float>();

    /// <summary>
    /// 角度のリストのプロパティ
    /// </summary>
    public List<float> AngleList
    {
        get { return _angleList; }
        set { _angleList = value; }
    }

    /// <summary>
    /// グラフ描画クラスのインターフェイス
    /// </summary>
    private IGraph iGraph;

    private void Awake()
    {
        //明示的に実装したインターフェイスメソッドを、クラス内から呼び出す事ができないので、呼び出せるように変数に代入する
        iGraph = (IGraph)gameObject.GetComponent<GraphDrawingSpaceView>();
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
        iGraph.DrawBackground();
        iGraph.DrawHorizontalScaleLine();
        iGraph.DrawHorizontalScaleCenterLine();
        iGraph.DrawVerticalScaleLine();
        iGraph.DrawVerticalScaleCenterLine();
        iGraph.DrawSeries(_applicationTimeList, _angleList);
        iGraph.DrawFrameBorder();
    }

    /// <summary>
    /// // GL描画用マテリアルを設定する
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
    /// 背景を描く
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
    /// 系列を描く
    /// </summary>
    /// <param name="applicationTimeList">経過時間のリスト</param>
    /// <param name="angleList">角度のリスト</param>
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
                    //最初の時間を目盛りの0に合わせる                 //垂直の描画位置を20分の1にする
                    iGraph.FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / 20, 0);

                    //グラフの目盛りの20秒を超えたらループを抜ける
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
    /// 横目盛りを描く 
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
    /// 縦目盛りを描く 
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
    /// 垂直の目盛りの中央の線を太く描く
    /// </summary>
    void IGraph.DrawVerticalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.red);
            _lineMaterial.SetPass(0);

            // データグラフの描画
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
    /// 水平の目盛りの中央の線を太く描く
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
    /// 枠線を描く
    /// </summary>
    void IGraph.DrawFrameBorder()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.green);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                //縦線
                iGraph.FollowAndDrawGraph(0, -2, 0);
                iGraph.FollowAndDrawGraph(0, 2, 0);

                float graphWidth = 20;

                //横線
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
