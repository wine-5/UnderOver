using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面の各種ボタンを管理
/// </summary>
public class ResultManager : MonoBehaviour
{
    /// <summary>
    /// 次のステージに進む
    /// </summary>
    public void OnNextStageButton()
    {
        SceneController.Instance.LoadNextStage();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */
    }

    /// <summary>
    /// タイトルに進む
    /// </summary>
    public void OnTitleButton()
    {
        SceneController.Instance.LoadTitle();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */
    }

    /// <summary>
    /// もう一度同じステージをする
    /// </summary>
    public void OnRetryButton()
    {
        SceneController.Instance.ReloadCurrentStage();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

    }

    /// <summary>
    /// リザルト画面をロード
    /// </summary>
    public void OnResultButton()
    {
        SceneController.Instance.LoadResult();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

    }
}
