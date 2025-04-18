using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public void OnNextStageButton()
    {
        SceneController.Instance.LoadNextStage();
    }

    public void OnTitleButton()
    {
        SceneController.Instance.LoadTitle();
    }

    public void OnRetryButton()
    {
        SceneController.Instance.ReloadCurrentStage();
    }

    /* もしかしたら使用しないかも */
    public void OnResultButton()
    {
        SceneController.Instance.LoadResult();
    }
}
