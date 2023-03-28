using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int timeLimit;
    public MacthItem[] matchItems;
    public MacthItemUI itemUIPb;
    public Transform gridRoot;
    public GameState state;
    List<MacthItem> m_matchItemsCopy;
    List<MacthItemUI> m_matchItemUIs;
    List<MacthItemUI> m_answers;
    float m_timecouting;
    int m_totalMatchItem;
    int m_totalMoving;
    int m_rightMoving;
    bool m_isAnswerChecking;

    public int TotalMoving { get => m_totalMoving; }
    public int RightMoving { get => m_rightMoving; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_matchItemsCopy = new List<MacthItem>();
        m_matchItemUIs = new List<MacthItemUI>();
        m_answers = new List<MacthItemUI>();
        m_timecouting = timeLimit;
        state = GameState.Starting;
    }
    public override void Start()
    {
        base.Start();
        // GenerateMatchItemss();
        if (AudioController.Ins)
            AudioController.Ins.PlayBackgroundMusic();
    }
    private void Update()
    {
        if (state != GameState.Playing) return;
        m_timecouting -= Time.deltaTime;
        if (m_timecouting <= 0 && state != GameState.Timeout)
        {
            state = GameState.Timeout;
            m_timecouting = 0;
            if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.timeOut);
            Debug.Log("tiemout");
            if (GUIManager.Ins)
                GUIManager.Ins.timeoutDialog.Show(true);
            
        }
        if (GUIManager.Ins)
            GUIManager.Ins.UpdateTimeBar((float)m_timecouting, (float)timeLimit);
    }
    public void PlayGame()
    {
        state = GameState.Playing;
        GenerateMatchItemss();
        if (GUIManager.Ins)
            GUIManager.Ins.ShowGamePlay(true);
    }
    private void SuffleMatchItems()
    {
        if (m_matchItemsCopy == null || m_matchItemsCopy.Count <= 0) return;
        for (int i = 0; i < m_matchItemsCopy.Count; i++)
        {
            var temp = m_matchItemsCopy[i];
            if (temp != null)
            {
                int ranIdx = Random.Range(0, m_matchItemsCopy.Count);
                m_matchItemsCopy[i] = m_matchItemsCopy[ranIdx];
                m_matchItemsCopy[ranIdx] = temp;
            }
        }
    }
    private void GenerateMatchItemss()
    {
        if (matchItems == null || matchItems.Length <= 0 || itemUIPb == null || gridRoot == null) return;
        int totalItems = matchItems.Length;
        int divItems = totalItems % 2;
        m_totalMatchItem = totalItems - divItems;
        for (int i = 0; i < m_totalMatchItem; i++)
        {
            var matchItem = matchItems[i];
            if (matchItem != null)
                matchItem.Id =i;
        }
        m_matchItemsCopy.AddRange(matchItems);
        m_matchItemsCopy.AddRange(matchItems);
        SuffleMatchItems();
        ClearGrid();
        for (int i = 0; i < m_matchItemsCopy.Count; i++)
        {
            var matchItem = m_matchItemsCopy[i];
            var matchIemUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);
            matchIemUIClone.transform.SetParent(gridRoot);
            matchIemUIClone.transform.localPosition = Vector3.zero;
            matchIemUIClone.transform.localScale = Vector3.one;
            matchIemUIClone.FistUpdateState(matchItem.icon);
            matchIemUIClone.Id = matchItem.Id;
            m_matchItemUIs.Add(matchIemUIClone);

            if (matchIemUIClone.btnComp)
            {
                matchIemUIClone.btnComp.onClick.RemoveAllListeners();
                matchIemUIClone.btnComp.onClick.AddListener(() =>
                {
                    if (m_isAnswerChecking) return;
                    m_answers.Add(matchIemUIClone);
                    matchIemUIClone.OpenAminTrigger();
                    if(m_answers.Count == 2)
                    {
                        m_isAnswerChecking = true;
                        m_totalMoving++;
                        StartCoroutine(CheckAnswerCo());
                    }
                    matchIemUIClone.btnComp.enabled = false;
                });
            }
        }
    }
    private IEnumerator CheckAnswerCo()
    {
        bool isRight = m_answers[0] != null && m_answers[1] != null && m_answers[0].Id == m_answers[1].Id;
        yield return new WaitForSeconds(1f);
        if(m_answers != null && m_answers.Count == 2)
        {
            if (isRight)
            {
                m_rightMoving++;

                for (int i = 0; i < m_answers.Count; i++)
                {
                    var answer = m_answers[i];
                    if (answer)
                        answer.ExpAminTrigger();
                    if (AudioController.Ins)
                        AudioController.Ins.PlaySound(AudioController.Ins.right);

                }
            }
            else
            {
                for (int i = 0; i < m_answers.Count; i++)
                {
                    var answer = m_answers[i];
                    if (answer)
                        answer.OpenAminTrigger();
                    if (AudioController.Ins)
                        AudioController.Ins.PlaySound(AudioController.Ins.wrong);

                }
            }
        }
        m_answers.Clear();
        m_isAnswerChecking = false;
        if(m_rightMoving == m_totalMatchItem)
        {
            Prefs.bestMove = TotalMoving;
            Debug.Log("gameover");
            if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.gameover);

            if (GUIManager.Ins)
                GUIManager.Ins.gameoverDialog.Show(true);
        }
    }
    public void ClearGrid()
    {
        if (gridRoot == null) return;
        for (int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);
            if (child)
                Destroy(child.gameObject);
        }
    }
}
