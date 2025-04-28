using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public void OnNextStageButton()
    {
        SceneController.Instance.LoadNextStage();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */
    }

    public void OnTitleButton()
    {
        SceneController.Instance.LoadTitle();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */
    }

    public void OnRetryButton()
    {
        SceneController.Instance.ReloadCurrentStage();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

    }

    /* もしかしたら使用しないかも */
    public void OnResultButton()
    {
        SceneController.Instance.LoadResult();
        AudioManager.Instance.PlaySE("seClickButton"); /*SEを再生 */

    }
}
