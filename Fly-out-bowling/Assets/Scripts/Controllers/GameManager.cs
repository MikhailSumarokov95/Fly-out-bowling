using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int numberOfRounds;
    public UnityEvent onStartMenuGame;
    public UnityEvent onStartGame;
    public UnityEvent onPauseGame;
    public UnityEvent onResumeGame;
    public UnityEvent onRoundOver;
    public UnityEvent onNextRound;
    public UnityEvent onGameOver;
    public UnityEvent onRestartGame;
    public int RoundNumber { get; private set; }

    private void Start()
    {
        StartMenuGame();
        Time.timeScale = 0;
    }

    public void StartMenuGame()
    {
        onStartMenuGame?.Invoke();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        onStartGame?.Invoke();
        Time.timeScale = 1;
        RoundNumber = 0;
    }

    public void PauseGame()
    {
        onPauseGame?.Invoke();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        onResumeGame?.Invoke();
        Time.timeScale = 1;
    }

    public void RoundOver()
    {
        RoundNumber++;
        if (RoundNumber < numberOfRounds) onRoundOver?.Invoke();
        else GameOver();
    }

    public void NextRound() => onNextRound.Invoke();

    public void GameOver() => onGameOver?.Invoke();

    public void RestartGame()
    {
        onRestartGame?.Invoke();
        RoundNumber = 0;
        Time.timeScale = 1;
    }

    public void AddRounds(int count) => RoundNumber += count;
}
