using System;
using System.Globalization;

public static class AngleUtility
{

    // （参考）https://jprogramer.com/catealgo/1033
    public static float GetAngle(float x, float y) {
        //座標の読み込み
        double angle = Math.Acos(x / Math.Sqrt(x*x + y*y));
        angle = angle * 180.0 / Math.PI;
        if ( y < 0 ) {
            angle = 360.0 - angle;
        }
        // -180 ～ 180に変換して返却
        return AngleConversion((float)angle);
    }

    /// <summary>
    /// 角度を、0～360から-180～180に変換
    /// </summary>
    public static float AngleConversion(float angle) {
        // -180 ～ 180に変換
        return (angle > 180.0f) ? angle - 360.0f : angle;
    }
}