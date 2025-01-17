using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNumberManager : MonoBehaviour {

    [SerializeField] private int[] l_iNumber;            // 桁毎の数値を入れておく
    [SerializeField] private int iDigitMillisecond = 3;  // 表示する桁数ミリ秒
    [SerializeField] private int iDigitSeconds = 2;      // 表示する桁数秒
    [SerializeField] private int iDigitMinutes = 2;      // 表示する桁数分
    [SerializeField] private int i1Minutes = 60;         // 何秒で一分か

    //[SerializeField] private int iDispNumber = 0;
    [SerializeField] private float fTime = 0.0f;         // deltaTimeを加算していく
    private int iTimeMillisecond;                        // ミリ秒
    private int iTimeMinutes;                            // 分


    void Start() {
        l_iNumber = new int[iDigitMillisecond + iDigitSeconds + iDigitSeconds];
        fTime = 0.0f;
        iTimeMillisecond = 0;
        iTimeMinutes = 0;
    }

    void Update() {
        // ポーズ画面の時にオブジェクトが生成しないような処理
        if (Mathf.Approximately(Time.timeScale, 0f)) { return; }

        // 時間を計測
        fTime += Time.deltaTime;

        // 整数に繰り上げ
        iTimeMillisecond = (int)(fTime * 1000);

        // 1分足ったら分数を足して秒を引く
        if (iTimeMillisecond >= i1Minutes * 1000) {
            fTime -= i1Minutes;
            iTimeMinutes++;
        }

        {
            int minutes = iTimeMinutes;
            // ミリ秒
            for (int i = 0; i < iDigitMillisecond; i++) {
                l_iNumber[i] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // 秒
            for (int i = 0; i < iDigitSeconds; i++) {
                l_iNumber[i + iDigitMillisecond] = iTimeMillisecond % 10;
                iTimeMillisecond /= 10;
            }
            // 分
            for (int i = 0; i < iDigitMinutes; i++) {
                l_iNumber[i + iDigitMillisecond + iDigitSeconds] = minutes % 10;
                minutes /= 10;
            }
        }

        //for (int i = 0; i < 7; i++) {
        //    Debug.Log(l_iNumber[i]);
        //}

    }

    public int Get_iNumber(int iDigit) {
        return l_iNumber[iDigit];
    }

    public float Get_fTime() {
        return fTime;
    }
}
