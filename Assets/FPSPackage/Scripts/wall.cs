using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class wall : MonoBehaviour

{    // 物にさわった時に呼ばれる
    void OnTriggerEnter(Collider col)
    {
        // もしPlayerにさわったら
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("Damage"); //ダメージを与えて
        }
    }
}

   

