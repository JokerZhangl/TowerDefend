using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject StartUI;
    [SerializeField] GameObject VictoryUI;
    [SerializeField] GameObject GameOverUI;

    [SerializeField] GameObject StartButtonUI;

    [SerializeField] TextMeshProUGUI Health;
    [SerializeField] TextMeshProUGUI Wave;
    [SerializeField] TextMeshProUGUI GameTime;
    [SerializeField] TextMeshProUGUI emenmyCount;

    [SerializeField] List<Image> star;
    GameState state= GameState.unStart;
    int healthCount = 2;

    int AllWave;
    int CurrentWave;
    int Gametime=0;
    float timer;
    int GameOverTime;
    int EmenmyCount=0;

    enum GameState
    {   unStart,
        Gaming,
        AllRoundOver,
        Over,
        win,
    }
    
     void Update()
    {
        if (state == GameState.Gaming||state==GameState.AllRoundOver)
        {
            timer += Time.deltaTime;
            Gametime = (int)timer;
            if (GameTime != null) GameTime.text = Gametime.ToString();
        }
      
    }
    // Start is called before the first frame update
    void Start()
    {
       
        Time.timeScale = 1;
        RefreshHealth();

        if (VictoryUI != null) VictoryUI.SetActive(false);
        if (GameOverUI != null) GameOverUI.SetActive(false);
        if (StartButtonUI != null) StartButtonUI.SetActive(true);
      
    }

   
  
    // 尝试在场景中查找缺失的引用。要求场景中的对象使用与序列化字段相同的名称（或按需调整查找逻辑）。

    // Update is called once per frame
    

    void VictoryGame()
    {
        if (VictoryUI != null) VictoryUI.SetActive(true);
    }
    void OverGame()
    {
        if (GameOverUI != null) GameOverUI.SetActive(true);

    }
   public void NextLevel() {

        SceneManager.LoadScene(3);

    }
    public void RestartGame() {

        SceneManager.LoadScene(2);
        // 使用异步重载以避免主线程长时间阻塞
        // StartCoroutine(ReloadSceneAsync());

    }
    IEnumerator ReloadSceneAsync()
    {
        var active = SceneManager.GetActiveScene();
        Debug.Log($"[GameManager] Async reload scene {active.name}");
        AsyncOperation op = SceneManager.LoadSceneAsync(active.buildIndex);
        // allowSceneActivation 默认为 true，这里可保留
        while (!op.isDone)
        {
            // 记录进度以便调试
            Debug.Log($"[GameManager] reload progress={op.progress:F2}");
            yield return null;
        }
        Debug.Log("[GameManager] reload finished");
    }
    public  void ExitGame()
    {
        SceneManager.LoadScene("MianMenu");
    }
    public void GetHurt()
    {
        healthCount -= 1;
        RefreshHealth();
        if (healthCount <= 0)
        {
            StopRound();
        }
    }
    public void RefreshHealth()
    {
        if (Health != null)
            Health.text = healthCount.ToString();
    }
     public  void RefreshWave(int currntWave, int allWave)
    {
        if (Wave != null)
            Wave.text= currntWave.ToString()+"/"+ allWave.ToString();
    }
     public void Startround()
    {
        state = GameState.Gaming;
        SpawnManager.Instance.StartRound();
        if (StartButtonUI != null) StartButtonUI.SetActive(false);
    }
    void StopRound()
    {
        OverGame();
        healthCount = 0;
        SpawnManager.Instance.StopSpanw();
        state = GameState.Over;
        Debug.Log("gameover");
    }
    public void AddEnemy(int count) {

        EmenmyCount += count;
        if (emenmyCount != null) emenmyCount.text= EmenmyCount.ToString();
       
    }
    public void ReduceEnemy(int count) {
        
        EmenmyCount -= count;
        if (emenmyCount != null) emenmyCount.text= EmenmyCount.ToString();
        
        
    }
    public void OverAllRound()
    {
        state = GameState.AllRoundOver;
        StartCoroutine(CheckIsWon());

    }
    IEnumerator CheckIsWon()
    {
        while (true) {

            if (EmenmyCount == 0)
            {
                GameWon();
                yield break;
            }
            yield return new WaitForSeconds(0.5f);

        }
    }
    void GameWon()
    {
        if(state == GameState.AllRoundOver&& EmenmyCount == 0 && healthCount > 0)
        {
            state=GameState.win;
            GameOverTime = (int)Gametime;
            StarCount(GameOverTime);
            VictoryGame();
        }
    }
    void StarCount(int time) {

        if (time < 20)
        {
            for (int i = 0; i < star.Count; i++)
            {
                star[i].color = Color.yellow;
            }
        }
        else if (time >= 20 && time < 30) {

            for (int i = 0; i < star.Count-1; i++)
            {
                star[i].color = Color.yellow;
            }
        }
        else
        {
            for (int i = 0; i < star.Count - 2; i++)
            {
                star[i].color = Color.yellow;
            }
        }
    }
}
