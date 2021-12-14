using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GraphDrawingPanelView : MonoBehaviour
{
    private Material _lineMaterial;

    void Start()
    {
        CreateLineMaterial();
    }

    private void OnRenderObject()
    {

        DrawHorizontalScaleLine();
        DrawHorizontalScaleCenterLine();
        DrawVerticalScaleLine();
        DrawVerticalScaleCenterLine();
        DrawSeries(timeList, dotList);
    }

    private List<float> timeList = new List<float>();
    private List<float> dotList = new List<float>();
    //グラフ出力用データを設定する
    public void SetGraphData(List<float> timeList, List<float> dotList)
    {
        this.timeList = timeList;
        this.dotList = dotList;
    }

    void CreateLineMaterial()
    {
        if (!_lineMaterial)
        {
            // GL描画用マテリアル設定
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
            _lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            _lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    private Color _seriesClor = new Color(0f, 163f, 255f, 1f);

    //系列を描く
    void DrawSeries(List<float> timeList, List<float> dotList)
    {
        // グラフ描画
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", _seriesClor);
            _lineMaterial.SetPass(0);

            // データグラフの描画
            GL.Begin(GL.LINE_STRIP);
            {
                for (int i = 0; i < timeList.Count; i++)
                {
                    //最初の時間を目盛りの0に合わせる //垂直の描画位置を20分の1にする
                    GL.Vertex3(timeList[i] - timeList[0], dotList[i] / 20, 0.0f);

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

    //横目盛りを描く
    void DrawHorizontalScaleLine()
    {
        //1ずつ3ライン
        // グラフ描画
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // データグラフの描画
            GL.Begin(GL.LINES);
            {
                for (var i = -1; i < 2; i++)
                {
                    GL.Vertex3(0, i, 0.0f);
                    GL.Vertex3(20, i, 0.0f);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

    //縦目盛りを描く
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
                    GL.Vertex3(i, 2.0f, 0.0f);
                    GL.Vertex3(i, -2.0f, 0.0f);
                }

            }
            GL.End();
        }
        GL.PopMatrix();
    }

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
                GL.Vertex3(verticalCenterPosition - lineWidth, 2, 0);
                GL.Vertex3(verticalCenterPosition + lineWidth, 2, 0);
                GL.Vertex3(verticalCenterPosition + lineWidth, -2, 0);
                GL.Vertex3(verticalCenterPosition - lineWidth, -2, 0);


            }
            GL.End();
        }
        GL.PopMatrix();
    }

    void DrawHorizontalScaleCenterLine()
    {
        GL.PushMatrix();
        {
            _lineMaterial.SetColor("_Color", Color.white);
            _lineMaterial.SetPass(0);

            // データグラフの描画
            GL.Begin(GL.QUADS);
            {
                float lineWidth = 0.02f;
                int verticalCenterPosition = 0;
                GL.Vertex3(0,  verticalCenterPosition + lineWidth, 0);;
                GL.Vertex3(20, verticalCenterPosition + lineWidth, 0);
                GL.Vertex3(20, verticalCenterPosition - lineWidth, 0);
                GL.Vertex3(0,  verticalCenterPosition - lineWidth, 0);
            }
            GL.End();
        }
        GL.PopMatrix();
    }    
}

    ////LineRendererでのグラフ出力用
    //[SerializeField] private LineRenderer lineRenderer;
    //public void DrawGraph(List<float> timeList, List<float> dotList)
    //{


    //    // 点の数を指定する
    //    lineRenderer.positionCount = dotList.Count;
    //    lineRenderer.startWidth = 0.01f;                   // 開始点の太さを0.1にする
    //    lineRenderer.endWidth = 0.01f;                     // 終了点の太さを0.1にする

    //    // 線を引く場所を一気に設定する
    //    //lineRenderer.SetPositions();


    //    for (int i = 0; i < timeList.Count; i++)
    //    {
    //        lineRenderer.SetPosition(i, new Vector3(timeList[i], dotList[i] / 20, 0.0f));
    //    }
    //}
//1メモリはY軸は20度。X軸は１秒毎としてください
//X軸は真ん中が0度の 40度～ マイナス40度としてください