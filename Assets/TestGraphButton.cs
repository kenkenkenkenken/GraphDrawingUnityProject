using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class TestGraphButton : MonoBehaviour
{
    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 100;
    public float radius = 3.0f;

    static Material _lineMaterial;
    static void CreateLineMaterial()
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

    private List<float> timeList = new List<float>();
    private List<float> dotList = new List<float>();
    public void DrawGraph(List<float> timeList, List<float> dotList)
    {
        this.timeList = timeList;
        this.dotList = dotList;
    }


        // Will be called after all regular rendering is done
        private void OnRenderObject()
        {

        Debug.Log("TestGraphButtonDrawGraph");

        CreateLineMaterial();
        // グラフ描画
        GL.PushMatrix();
        {
            // Matrial Apply
            _lineMaterial.SetPass(0);


            // データグラフの描画
            GL.Begin(GL.LINE_STRIP);
            {
                GL.Color(Color.red);
                if (timeList.Count > 0)
                {
                    for (int i = 0; i < timeList.Count; i++)
                    {
                        GL.Vertex3(timeList[i], dotList[i] / 20 + 1, 0.0f);
                    }
                }

                //GL.Vertex3(0,0,0);
                //GL.Vertex3(1,1,0);
                //GL.Vertex3(2,0,0);
                //GL.Vertex3(3,2,0);

            }
            GL.End();

            //float cellHeight = 1; //補助線１つの高さ
            //var rectHeight = 4; //グラフ図の高さ
            //var rectWidth = 20; //グラフ図の幅
            //var interval = 1; //
            //var maxValue = 4; //入力されたデータの最大値
            //var gridCount = 0; //補助線の数
            //var collectPosY = 0;
            //var collectPosX = 0;
            //float[] datas = { 1, 2, 3, 2, 1, 5 };

            //// 補助線の描画
            //GL.Begin(GL.LINES);
            //{
            //    // 入力されたデータの最大値をもとに補助線の数を決定する
            //    cellHeight = rectHeight * interval / maxValue;
            //    gridCount = (int)(maxValue / interval);
            //    var ans = maxValue % interval;
            //    if (!(ans < 0 || 0 < ans))
            //    {
            //        gridCount -= 1;
            //    }

            //    if (gridCount >= 10)
            //    {
            //        cellHeight = rectHeight / 10f;
            //        gridCount = 10;
            //    }


            //    for (int i = 1; i <= gridCount; i++)
            //    {
            //        var line = collectPosY + rectHeight - i * cellHeight;
            //        // 実際に線を描画する
            //        GL.Vertex3(collectPosX, line, 0);
            //        GL.Vertex3(collectPosX + rectWidth, line, 0);
            //    }
            //}
            //GL.End();
        }
        GL.PopMatrix();

    }

    public void TestDrawGraph()
    {

    }
}