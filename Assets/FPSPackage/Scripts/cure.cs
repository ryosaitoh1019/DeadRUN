using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cure : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		// もしPlayerにさわったら
		if (col.gameObject.tag == "Player")
		{
			col.SendMessage("cure"); // 回復を与えて
		}
		// 自分は消える
		Destroy(this.gameObject);
	}
}
