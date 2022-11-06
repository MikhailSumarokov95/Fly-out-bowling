using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittlesController : MonoBehaviour
{
    private List<int> _skittlesFallenNumber;
    private bool _isFallSkittle;
    private float _timerDelayAfterFallFirstSkittle;
    private GameManager _gameManager;
    readonly float _delayAfterFallFirstSkittle = 3f;


    private void Start()
    {
        _skittlesFallenNumber = new List<int>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (_isFallSkittle) _timerDelayAfterFallFirstSkittle += Time.deltaTime;
        if (_timerDelayAfterFallFirstSkittle > _delayAfterFallFirstSkittle) StopCountSkittleFallen();
    }

    public void onSkittleFallen(int number)
    {
        _isFallSkittle = true;
        if (!_skittlesFallenNumber.Contains(number)) 
            _skittlesFallenNumber.Add(number);
    }

    private void StopCountSkittleFallen()
    {
        FindObjectOfType<LeaderBoard>().StartLeaderBoard(_skittlesFallenNumber.Count * 10, _gameManager.RoundNumber > 0);
        _timerDelayAfterFallFirstSkittle = 0f;
        _isFallSkittle = false;
        _skittlesFallenNumber.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var skittleTransform = transform.GetChild(i);
            skittleTransform.GetComponent<Skittle>()?.StopMove();
            if (skittleTransform.GetComponent<Skittle>() == null) Destroy(skittleTransform.gameObject);
        }
    }
}

