using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyChara : MonoBehaviour {

	//キャラクターコントローラー
	private CharacterController cCon;
	//　キャラクターの速度
	private Vector3 velocity;
	//　Animator
	private Animator animator;
	//　歩くスピード
	[SerializeField]
	private float walkSpeed = 1.5f;
	//　走るスピード
	[SerializeField]
	private float runSpeed = 2.5f;
	//　走っているかどうか
	private bool runFlag = false;
	//　キャラクター視点のカメラ
	private Transform myCamera;
	//　キャラクター視点のカメラで回転出来る限度
	[SerializeField]
	private float cameraRotateLimit = 30f;
	//　カメラの上下の移動方法。マウスを上で上を向く場合はtrue、マウスを上で下を向く場合はfalseを設定
	[SerializeField]
	private bool cameraRotForward = true;
	//　カメラの角度の初期値
	private Quaternion initCameraRot;
	//　キャラクター、カメラ（視点）の回転スピード
	[SerializeField]
	private float rotateSpeed = 2f;
	//　カメラのX軸の角度変化値
	private float xRotate;
	//　キャラクターのY軸の角度変化値
	private float yRotate;
	//　マウス移動のスピード
	[SerializeField]
	private float mouseSpeed = 2f;
	//　キャラクターのY軸の角度
	private Quaternion charaRotate;
	//　カメラのX軸の角度
	private Quaternion cameraRotate;

	void Start () {
		//キャラクターコントローラの取得
		cCon = GetComponent<CharacterController>();
		animator = GetComponent <Animator> ();
		myCamera = GetComponentInChildren<Camera>().transform;	//　キャラクター視点のカメラの取得
		initCameraRot = myCamera.localRotation;
		charaRotate = transform.localRotation;
		cameraRotate = myCamera.localRotation;
	}

	void Update () {

		//　キャラクターの向きを変更する
		RotateChara();
		//　視点の向きを変える
		RotateCamera();

		//　キャラクターコントローラのコライダが地面と接触してるかどうか
		if(cCon.isGrounded) {

			velocity = Vector3.zero;

			velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));

			//　走るか歩くかでスピードを変更する
			float speed = 0f;

			if(Input.GetButton("Run")) {
				runFlag = true;
				speed = runSpeed;
			} else {
				runFlag = false;
				speed = walkSpeed;
			}
			velocity *= speed;

			if(velocity.magnitude > 0f) {
				if (runFlag) {
					animator.SetFloat ("Speed", 2.1f);
				} else {
					animator.SetFloat ("Speed", 1f);
				}
			} else {
				animator.SetFloat("Speed", 0f);
			}

		}
		velocity.y += Physics.gravity.y * Time.deltaTime; //　重力値を計算
		cCon.Move(velocity * Time.deltaTime); //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
	}
	//　キャラクターの角度を変更
	void RotateChara() {
		//　横の回転値を計算
		float yRotate = Input.GetAxis ("Mouse X") * mouseSpeed;

		charaRotate *= Quaternion.Euler(0f, yRotate, 0f);

		//　キャラクターの回転を実行
		transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRotate, rotateSpeed * Time.deltaTime);
	}
	//　カメラの角度を変更
	void RotateCamera() {

		float xRotate = Input.GetAxis("Mouse Y") * mouseSpeed;

		//　マウスを上に移動した時に上を向かせたいなら反対方向に角度を反転させる
		if(cameraRotForward) {
			xRotate *= -1;
		}
		//　一旦角度を計算する	
		cameraRotate *= Quaternion.Euler(xRotate, 0f, 0f);
		//　カメラのX軸の角度が限界角度を超えたら限界角度に設定
		var resultYRot = Mathf.Clamp (Mathf.DeltaAngle (initCameraRot.eulerAngles.x, cameraRotate.eulerAngles.x), -cameraRotateLimit, cameraRotateLimit);
		//　角度を再構築
		cameraRotate = Quaternion.Euler (resultYRot, cameraRotate.eulerAngles.y, cameraRotate.eulerAngles.z);
		//　カメラの視点変更を実行
		myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRotate, rotateSpeed * Time.deltaTime);
	}//　キャラが回転中かどうか？
	private bool charaRotFlag = false;

	void Update () {

		//　キャラクターの向きを変更する
		RotateChara();
		//　視点の向きを変える
		RotateCamera();

		//　キャラクターコントローラのコライダが地面と接触してるかどうか
		if(cCon.isGrounded) {

			velocity = Vector3.zero;

			velocity = (transform.forward * Input.GetAxis ("Vertical") + transform.right * Input.GetAxis ("Horizontal")).normalized;

			//　走るか歩くかでスピードを変更する
			float speed = 0f;

			if(Input.GetButton("Run")) {
				runFlag = true;
				speed = runSpeed;
			} else {
				runFlag = false;
				speed = walkSpeed;
			}
			velocity *= speed;

			if(velocity.magnitude > 0f || charaRotFlag) {
				if (runFlag && !charaRotFlag) {
					animator.SetFloat ("Speed", 2.1f);
				} else {
					animator.SetFloat ("Speed", 1f);
				}
			} else {
				animator.SetFloat("Speed", 0f);
			}
		}
		velocity.y += Physics.gravity.y * Time.deltaTime; //　重力値を計算
		cCon.Move(velocity * Time.deltaTime); //　キャラクターコントローラのMoveを使ってキャラクターを移動させる
	}

	//　キャラクターの角度を変更
	void RotateChara ()
	{
		//　横の回転値を計算
		float yRotate = Input.GetAxis ("Mouse X") * mouseSpeed;

		charaRotate *= Quaternion.Euler (0f, yRotate, 0f);

		//　キャラクターが回転しているかどうか？

		if (yRotate != 0f) {
			charaRotFlag = true;
		} else {
			charaRotFlag = false;
		}

		//　キャラクターの回転を実行
		transform.localRotation = Quaternion.Slerp (transform.localRotation, charaRotate, rotateSpeed * Time.deltaTime);
	}
}

