using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private int _coinsCount;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _defeatPanel;

    private int _score = 0;

    private void OnEnable()
    {
        Circle.CollisionEvent += CollisionEvent;
    }

    private void OnDisable()
    {
        Circle.CollisionEvent -= CollisionEvent;
    }

    private void CollisionEvent(bool isWin)
    {
        if(isWin)
        {
            _scoreText.text = (++_score).ToString();
            if(_score == _coinsCount)
            {
                EndGame(isWin);
            }
        }
        else
        {
            EndGame(isWin);
        }
    }

    private void EndGame(bool isWin)
    {
        Circle._canMove = false;
        if(isWin)
        {
            _winPanel.SetActive(true);
        }
        else
        {
            _defeatPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        Circle._canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
