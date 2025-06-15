using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// カメラの追従と制限を管理するクラス。
/// </summary>
public class CameraController : MonoBehaviour
{

    [SerializeField] private float zPosition = -100f;
    [SerializeField] private Transform target; /* 追従するオブジェクトを入れる */
    private float smoothing = 5f; /* カメラの追従速度 */
    private Vector2 minBounds; /* カメラの左下の制限 */
    private Vector2 maxBounds; /* カメラの右上の制限 */

    private float cameraHalfWidth; /* カメラの横幅の半分 */
    private float cameraHalfHeight; /* カメラの縦幅の半分 */


    /// <summary>
    /// カメラの初期設定を行う
    /// 主にカメラのサイズとアスペクト比を考慮して、カメラの横幅を計算
    /// </summary>
    void Start()
    {
        Camera camera = Camera.main;
        cameraHalfHeight = camera.orthographicSize; /* 2D（正投影）カメラの「上方向の距離」を取得 */
        cameraHalfWidth = cameraHalfWidth * camera.aspect; /* アスペクト比を考慮して */
    }


    /// <summary>
    /// 毎フレーム呼び出され、カメラをターゲットの位置に追従
    /// ターゲットの位置に基づき、カメラの位置を調整
    /// </summary>
    void Update()
    {
        if (target == null)
        {
            Debug.LogError("ターゲットが設定されていない");
            return;
        }

        /* プレイヤーの位置を取得する */
        Vector2 targetPosition = target.position;

        /* カメラの位置を設定する */
        transform.position = new Vector3(targetPosition.x, transform.position.y, zPosition);  // y座標（高さ）は固定、z座標だけ変更

    }

}
