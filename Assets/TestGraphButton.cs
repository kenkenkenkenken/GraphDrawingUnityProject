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
            // GL�`��p�}�e���A���ݒ�
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
        // �O���t�`��
        GL.PushMatrix();
        {
            // Matrial Apply
            _lineMaterial.SetPass(0);


            // �f�[�^�O���t�̕`��
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

            //float cellHeight = 1; //�⏕���P�̍���
            //var rectHeight = 4; //�O���t�}�̍���
            //var rectWidth = 20; //�O���t�}�̕�
            //var interval = 1; //
            //var maxValue = 4; //���͂��ꂽ�f�[�^�̍ő�l
            //var gridCount = 0; //�⏕���̐�
            //var collectPosY = 0;
            //var collectPosX = 0;
            //float[] datas = { 1, 2, 3, 2, 1, 5 };

            //// �⏕���̕`��
            //GL.Begin(GL.LINES);
            //{
            //    // ���͂��ꂽ�f�[�^�̍ő�l�����Ƃɕ⏕���̐������肷��
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
            //        // ���ۂɐ���`�悷��
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