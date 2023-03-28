using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MacthItemUI : MonoBehaviour
{
    int m_id;
    public Sprite BG;
    public Sprite BackBG;
    public Image itemBG;
    public Image itemIcon;
    public Button btnComp;
    bool m_isOpened;
     Animator m_amin;


    public int Id { get => m_id; set => m_id = value; }
    private void Awake()
    {
        m_amin = GetComponent<Animator>();
    }
    public void FistUpdateState(Sprite icon)
    {
        if (itemBG)
            itemBG.sprite = BackBG;
        if (itemIcon)
            itemIcon.sprite = icon;
            itemIcon.gameObject.SetActive(false);
    }
    public void ChangeState()
    {
        m_isOpened = !m_isOpened;
        if (itemBG)
            itemBG.sprite = m_isOpened ? BG : BackBG;
        if (itemIcon)
            itemIcon.gameObject.SetActive(m_isOpened);
    }
    public void OpenAminTrigger()
    {
        if (m_amin)
            m_amin.SetBool(AminState.Flip.ToString(), true);
    }
    public void ExpAminTrigger()
    {
        if (m_amin)
            m_amin.SetBool(AminState.Exp.ToString(), true);
    }
    public void BackToIdle()
    {
        if (m_amin)
            m_amin.SetBool(AminState.Flip.ToString(), false);
        if (btnComp)
            btnComp.enabled = !m_isOpened;
    }
}
