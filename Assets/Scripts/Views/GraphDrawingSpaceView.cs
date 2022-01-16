using System.Collections.Generic;
using UnityEngine;

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
    private IGraph _iGraph;

    /// <summary>
    /// グラフ描画クラスのインターフェイスのプロパティ
    /// </summary>
    public IGraph IGraph
    {
        get { return _iGraph; }
        set { _iGraph = value; }
    }

    private void Awake()
    {
        //明示的に実装したインターフェイスメソッドを、クラス内から呼び出す事ができないので、呼び出せるように変数に代入する
        IGraph = (IGraph)gameObject.GetComponent<GraphDrawingSpaceView>();
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
        IGraph.DrawBackground();
        IGraph.DrawHorizontalScaleLine();
        IGraph.DrawHorizontalScaleCenterLine();
        IGraph.DrawVerticalScaleLine();
        IGraph.DrawVerticalScaleCenterLine();
        IGraph.DrawSeries(_applicationTimeList, _angleList);
        IGraph.DrawFrameBorder();
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

                IGraph.FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                IGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                IGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                IGraph.FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
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
                    IGraph.FollowAndDrawGraph(applicationTimeList[i] - applicationTimeList[0], angleList[i] / 20, 0);

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
                    IGraph.FollowAndDrawGraph(0, i, 0);
                    IGraph.FollowAndDrawGraph(graphWidth, i, 0);
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
                    IGraph.FollowAndDrawGraph(i, 2, 0);
                    IGraph.FollowAndDrawGraph(i, -2, 0);
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
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, 2, 0);
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, 2, 0);
                IGraph.FollowAndDrawGraph(verticalCenterPosition + lineWidth, -2, 0);
                IGraph.FollowAndDrawGraph(verticalCenterPosition - lineWidth, -2, 0);
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
                IGraph.FollowAndDrawGraph(0, verticalCenterPosition + lineWidth, 0);
                IGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition + lineWidth, 0);
                IGraph.FollowAndDrawGraph(graphWidth, verticalCenterPosition - lineWidth, 0);
                IGraph.FollowAndDrawGraph(0, verticalCenterPosition - lineWidth, 0);
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
                IGraph.FollowAndDrawGraph(0, -2, 0);
                IGraph.FollowAndDrawGraph(0, 2, 0);

                float graphWidth = 20;

                //横線
                for (var i = -2; i <= 2; i += 4)
                {
                    IGraph.FollowAndDrawGraph(0, i, 0);
                    IGraph.FollowAndDrawGraph(graphWidth, i, 0);
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
