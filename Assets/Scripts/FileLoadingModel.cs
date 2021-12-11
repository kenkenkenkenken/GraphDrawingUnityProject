using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Cysharp.Threading.Tasks;

public class FileLoadingModel : MonoBehaviour
{
    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    /// <returns>ファイルのデータをリストに格納したもの</returns>
    public async UniTask<List<string[]>> LoadCsv()
    {　　
        //CSVファイルを読み込む
        var mDataList = new List<string[]>(); 
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data"); 
        //ファイルのデータをリストに格納する
        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null )
            {
                mDataList.Add(line.Split(',')); 
            }
        }

        return mDataList;
    }
    //for (var i = 1; i < mDataList.Count; i++ )
    //{
    //   var lx = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][24]));
    //}

    //参考：
    // 左目（左右）zx
    //lx = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][24]));
    // 左目（上下）zy
    //ly = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][25]));

}
//index
// 1   Application Time(経過時間 )
// 24  Eye Ray left dir  x（ 左眼の視線方向 x ）
// 25  Eye Ray left dir  y（ 左眼の視線方向 y ）
// 26  Eye Ray left dir  z（ 左眼の視線方向 z ）
