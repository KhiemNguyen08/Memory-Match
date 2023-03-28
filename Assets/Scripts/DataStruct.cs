using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MacthItem
{
    public Sprite icon;
    int m_id;

    public int Id { get => m_id; set => m_id = value; }
   
}
public enum AminState
{
    Flip,
    Exp
}
public enum GameState
{
    Starting,
    Playing,
    Timeout,
    Gameover
}
public enum PrefKey
{
    BestScore
}
