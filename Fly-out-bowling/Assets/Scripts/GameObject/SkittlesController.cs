using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittlesController : MonoBehaviour
{
    [SerializeField] private GameObject targetCamera;
    private List<int> _skittlesFallenNumber;
    private GameManager _gameManager;
    private bool _isStartedCoroutine;
    readonly float _delayAfterFallFirstSkittle = 3f;

    private void Start()
    {
        _skittlesFallenNumber = new List<int>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SkittleFallen(int number)
    {
        if (!_isStartedCoroutine) StartCoroutine(StartCountSkittleFallen());
        if (!_skittlesFallenNumber.Contains(number)) 
            _skittlesFallenNumber.Add(number);
    }

    private IEnumerator StartCountSkittleFallen()
    {
        _isStartedCoroutine = true;
        FindObjectOfType<Camera>().SetTarget(targetCamera);
        yield return new WaitForSecondsRealtime(_delayAfterFallFirstSkittle);
        FindObjectOfType<LeaderBoard>().StartLeaderBoard(_skittlesFallenNumber.Count * 10, _gameManager.RoundNumber > 0);
        _skittlesFallenNumber.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var skittleTransform = transform.GetChild(i);
            skittleTransform.GetComponent<Skittle>()?.StopMove();
            if (skittleTransform.GetComponent<Skittle>() == null && !(skittleTransform.gameObject == targetCamera))
                Destroy(skittleTransform.gameObject);
        }
        _isStartedCoroutine = false;
    }
}

