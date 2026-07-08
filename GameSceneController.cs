using System.Collections;
using UnityEngine;
using unityroom.Api;
using static CommonData;

public class GameSceneController : SceneControllerBase
{
    static int instanceCount = 0;
    public static int InstanceCount { get => instanceCount; private set => instanceCount = value; }

    public static GameSceneController instance;

    int stageNum;
    [SerializeField] GameSceneCanvas canvas;
    [SerializeField] GameObject stageParent;

    private int score = 0;   //使用した手数
    public int Score { get => score; set { score = value; canvas.SetTekazu(score); } }

    enum Mode
    {
        Game,
        Miss,
        Success,
        Stop
    }
    Mode mode = Mode.Game;

    private void Awake()
    {
        instance = this;
        InstanceCount++;
    }

    protected override IEnumerator Start()
    {
        InitStageNum();
        yield return base.Start();
    }

    protected override IEnumerator _Update(string input)
    {
        switch(input)
        {
            case "Retry":
                mode = Mode.Stop;
                retryReservation = true;
                SoundManager.PlaySound(SoundId.ButtonPush);
                break;
            case "StageSelect":
                mode = Mode.Stop;
                stageParent.SetActive(false);
                SoundManager.PlaySound(SoundId.ButtonPush);
                yield return MySceneManager.Instance.LoadSceneAsync(SceneName.StageSelect);
                break;
            case "NextStage":
                SoundManager.PlaySound(SoundId.ButtonPush);
                SceneName currentSceneName = MySceneManager.Instance.CurrentSceneName;
                SceneName nextStageSceneName = (SceneName)((int)currentSceneName+1);
                stageParent.SetActive(false);
                yield return MySceneManager.Instance.LoadSceneAsync((SceneName )(int)nextStageSceneName);
                break;
            }
        if (retryReservation)
        {
            Debug.Log("retryReservation---");
            TimeScaleManager.SetPause(false);
            SceneName currentSceneName = MySceneManager.Instance.CurrentSceneName;
            stageParent.SetActive(false);
            yield return MySceneManager.Instance.LoadSceneAsync(currentSceneName);
        }
        base._Update(input);
        yield return true;
    }

    public void Miss()
    {
        if (mode != Mode.Game) return;
        mode = Mode.Miss;
        canvas.ShowMisWindow();
        SoundManager.PlaySound(SoundId.Miss);
    }

    public void Success()
    {
        if (mode != Mode.Game) return;
        mode = Mode.Success;
        canvas.ShowSuccessWindow(ShouldShowNextStage());
        //Save
        SoundManager.PlaySound(SoundId.Success);

        // スコア判定
        //int lastLowScore = SaveDataManager.Instance.GetLowScore(stageNum);    //スコア更新アニメ表示したい場合に使う
        SaveDataManager.Instance.UpdateScore(stageNum, Score);  //ハイスコアを更新してセーブ
    #if UNITY_WEBGL //UnityRoomへのハイスコア更新通知
        if (SaveDataManager.Instance.IsAllCleared())
        {
            var totalLowScore = SaveDataManager.Instance.GetTotalLowScore();
            UnityroomApiClient.Instance.SendScore(1, totalLowScore, ScoreboardWriteMode.HighScoreAsc);
        }
    #endif
    }

    bool ShouldShowNextStage()
    {
        Debug.Log( stageNum + ",  max : " + CommonData.MAX_STAGE_COUNT);
        return stageNum+1 < CommonData.MAX_STAGE_COUNT;
    }

    private void OnDestroy()
    {
        //Debug.Log("stage" + stageNum + " playTime : " + stageTime  + " --- ");
        InstanceCount--;
        if (instanceCount <= 0) instance = null;
    }

    bool retryReservation = false;
    public void Retry()
    {
        Debug.Log("Retry");
        retryReservation = true;
    }

    void InitStageNum()
    {
        // StageNum を SceneName から導出
        var currentSceneName = MySceneManager.Instance.GetCurrentSceneName();
        var numStrAry = currentSceneName.Split("Stage");
        stageNum = int.Parse(numStrAry[1])-1;
    }
}
