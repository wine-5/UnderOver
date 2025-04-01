using UnityEngine;

public class AlignSprites : MonoBehaviour
{
    [SerializeField] private float currentX = 0.0f; /* 現在のX座標 */

    public GameObject[] images; /* 並べたい画像のオブジェクトを入れるよう */
    public float spacing = 0.0f; /* 画像の間隔 */

    void Start()
    {
        AlignImages();
    }

    private void AlignImages()
    {
        foreach (GameObject image in images)
        {
            if(image.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            {
                float width = sr.bounds.size.x; /* 画像の幅を取得 */
                image.transform.position = new Vector3(currentX, 0, 0); /* 横位置を設定 */
                currentX += width + spacing; /* 次の画像のX座標を計算 */
            }
        }
    }
}
