using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wall : MonoBehaviour
{
	// 物にさわった時に呼ばれる
	private float timer;

	void OnTriggerStay (Collider col)
	{
		print (col);
		// もしPlayerにさわったら
		if (col.gameObject.tag == "Player") {
			timer += Time.deltaTime;
			if (timer > 0.5f) {
				timer = 0;
				col.SendMessage ("Damage"); //ダメージを与えて
			}
		}
	}
}

   

