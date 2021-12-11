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
    /// CSV�t�@�C����ǂݍ���
    /// </summary>
    /// <returns>�t�@�C���̃f�[�^�����X�g�Ɋi�[��������</returns>
    public async UniTask<List<string[]>> LoadCsv()
    {�@�@
        //CSV�t�@�C����ǂݍ���
        var mDataList = new List<string[]>(); 
        TextAsset csvFile = (TextAsset)await Resources.LoadAsync<TextAsset>("okn_data"); 
        //�t�@�C���̃f�[�^�����X�g�Ɋi�[����
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

    //�Q�l�F
    // ���ځi���E�jzx
    //lx = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][24]));
    // ���ځi�㉺�jzy
    //ly = AngleUtility.GetAngle(float.Parse(mDataList[i][26]), float.Parse(mDataList[i][25]));

}
//index
// 1   Application Time(�o�ߎ��� )
// 24  Eye Ray left dir  x�i ����̎������� x �j
// 25  Eye Ray left dir  y�i ����̎������� y �j
// 26  Eye Ray left dir  z�i ����̎������� z �j
