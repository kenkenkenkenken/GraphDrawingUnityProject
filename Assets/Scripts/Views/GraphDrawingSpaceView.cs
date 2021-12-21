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
    private Material _lineMaterial;

    private void Start()
    {
        CreateLineMaterial();
    }
    private List<float> timeList = new List<float>();
    private List<float> angleList = new List<float>();

    private void OnRenderObject()
    {
        DrawBackground();
        DrawHorizontalScaleLine();
        DrawHorizontalScaleCenterLine();
        DrawVerticalScaleLine();
        DrawVerticalScaleCenterLine();
        DrawSeries(timeList, angleList);
        DrawFrameBorder();
    }

    /// <summary>
    /// グラフ出力用データを設定する
    /// </summary>
    /// <param name="timeList"></param>
    /// <param name="dotList"></param>
    public void SetGraphData(List<float> timeList, List<float> angleList)
    {
        this.timeList = timeList;
        this.angleList = angleList;
    }

    /// <summary>
    /// // GL描画用マテリアル設定
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
    /// 背景を描く
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
    /// 系列を描く
    /// </summary>
    /// <param name="timeList">x軸のデータ</param>
    /// <param name="angleList">y軸のデータ</param>
    void DrawSeries(List<float> timeList, List<float> angleList)
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.cyan);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < timeList.Count; i++)
                {
                    //最初の時間を目盛りの0に合わせる //垂直の描画位置を20分の1にする
                    FollowAndDrawGraph(timeList[i] - timeList[0], angleList[i] / 20, 0.0f);

                    //グラフの目盛りの20秒を超えたらループを抜ける
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
    /// 横目盛りを描く 
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
    /// 縦目盛りを描く 
    /// </summary>
    void DrawVerticalScaleLine()
    {
        //1ずつ19ライン
        // グラフ描画
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // データグラフの描画
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
    /// 水平の目盛りの中央の線を太く描く
    /// </summary>
    void DrawVerticalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.red);
            _lineMaterial.SetPass(0);

            // データグラフの描画
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
    /// 水平の目盛りの中央の線を太く描く
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
    /// 枠線を描く
    /// </summary>
    void DrawFrameBorder()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.green);
            _lineMaterial.SetPass(0);

            GL.Begin(GL.LINES);
            {
                //縦線
                FollowAndDrawGraph(0, -2, 0);
                FollowAndDrawGraph(0, 2, 0);

                //横線
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
    /// ゲームオブジェクトを追従してグラフを描く
    /// </summary>
    /// <param name="x">追従前の描画位置のx座標</param>
    /// <param name="y">追従前の描画位置のy座標</param>
    /// <param name="z">追従前の描画位置のz座標</param>
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