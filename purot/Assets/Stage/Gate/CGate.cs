/*==============================================================================
    Project_
    [CGate.cs]
    ・ゲート制御
--------------------------------------------------------------------------------
    2021.04.19 @Author Suzuki Hayase
================================================================================
    History
        
            
/*============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGate : MonoBehaviour {
    private int iPassNum;                             // 通過数
    private int iMatchNum;                            // 一致数
    private int iClearNum;                            // クリア数
    private COrderManager csOrderManager;             // OrderManagerスクリプト

    [SerializeField] private GameObject gClear;       // クリアスタンプ

    // Start is called before the first frame update
    void Start() {
        iMatchNum = 0;
        iClearNum = 0;
        csOrderManager = GameObject.Find("PFB_OrderManager").GetComponent<COrderManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider col) {
        // 通過オブジェクト判定
        if (col.gameObject.tag == "RotateObject") {
            GameObject lamp = csOrderManager.Get_gClearLamp(iPassNum);
            OBJECT_SHAPE order = csOrderManager.Get_Order(iPassNum);

            // 指令と一致
            if(order == col.gameObject.GetComponent<CRotateObject>().Get_Shape()) {
                iMatchNum++;
                lamp.GetComponent<Renderer>().material.color = Color.green;
            }
            else {
                lamp.GetComponent<Renderer>().material.color = Color.red;
            }
            iPassNum++;

            int ordernum = csOrderManager.Get_iOrderNum();

            // 指令数のオブジェクトが通過したらリセット
            if (iPassNum == ordernum) {
                if(iMatchNum == ordernum) {
                    // クリアスタンプ生成
                    Instantiate(gClear, new Vector3(20, 0, -10 + iClearNum * 5),
                        Quaternion.Euler(0, 180, 0));
                    
                    // 指令生成
                    csOrderManager.CreateOrder(3);
                    iClearNum++;
                }
                else {
                    for (int i = 0; i < ordernum; i++) {
                        GameObject l = csOrderManager.Get_gClearLamp(i);
                        l.GetComponent<Renderer>().material.color = Color.white;
                    }
                }

                iMatchNum = 0;
                iPassNum = 0;      
            }
        }
    }
}
