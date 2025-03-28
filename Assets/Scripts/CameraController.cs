using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float zPosition = -10f;
    public Transform target; /* 追従するオブジェクトを入れる */
    public float smoothing = 5f; /* カメラの追従速度 */
    public Vector2 minBounds; /* カメラの左下の制限 */
    public Vector2 maxBounds; /* カメラの右上の制限 */

    private float cameraHalfWidth; /* カメラの横幅の半分 */
    private float cameraHalfHeight; /* カメラの縦幅の半分 */

    private const float CAMERA_FIX_POSITION = 3.0f; /* カメラの固定位置 */
    void Start()
    {
        Camera camera = Camera.main;
        cameraHalfHeight = camera.orthographicSize; /* 2D（正投影）カメラの「上方向の距離」を取得 */
        cameraHalfWidth = cameraHalfWidth * camera.aspect; /* アスペクト比を考慮して */
    }


    void Update()
    {
        if (target == null) {
            Debug.LogError("ターゲットが設定されていない");
            return;
        }

        /* プレイヤーの位置を取得する */
        Vector2 targetPosition = target.position;

        /* カメラの位置を設定する */
        transform.position = new Vector3(targetPosition.x, targetPosition.y + CAMERA_FIX_POSITION, zPosition);

    }

}
