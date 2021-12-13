using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GraphDrawingPanelView : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        
    }

    public void DrawGraph(List<float> timeList, List<float> dotList)
    {


        // 点の数を指定する
        lineRenderer.positionCount = dotList.Count;
        lineRenderer.startWidth = 0.01f;                   // 開始点の太さを0.1にする
        lineRenderer.endWidth = 0.01f;                     // 終了点の太さを0.1にする
        //lineRenderer.numCapVertices = 1;
        //lineRenderer.numCornerVertices = 1;

        // 線を引く場所を一気に設定する
        //lineRenderer.SetPositions();


        for (int i = 0; i < timeList.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(timeList[i], dotList[i] / 20, 0.0f));
        }
    }

}
//1メモリはY軸は20度。X軸は１秒毎としてください
//X軸は真ん中が0度の 40度〜 マイナス40度としてください