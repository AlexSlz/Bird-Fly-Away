using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private TMP_Text _timerView;
    [SerializeField]
    private TMP_Text _helpText;


    [SerializeField]
    private Player _player;
    public Player Player => _player;

    private BaseGameState _currentState;
    public BaseGameState CurrentState => _currentState;

    public PauseState pauseState;
    public RunState runState;
    public DeadState deadState;
    public WaitToStartState waitToStartState;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        pauseState = new PauseState(_pauseMenu, _timerView, this);
        runState = new RunState(_helpText, this);
        deadState = new DeadState(this);
        waitToStartState = new WaitToStartState(this);
        SetState(waitToStartState);
        InputManager.Instance.onPressPause += onPressPause;

        _timerView.color = GameAssets.Instance.ColorPalette.InterfaceColor;
        _helpText.color = GameAssets.Instance.ColorPalette.InterfaceColor;
    }

    private void onPressPause()
    {
        if (pauseState.isActive)
        {
            SetState(runState);
        }
        else if(runState.isActive)
        {
            SetState(pauseState);
        }
    }

    public void SetState(BaseGameState state)
    {
        if (_currentState != null)
            _currentState.EndState();
        _currentState = state;
        _currentState.EnterState();
    }

    public void EndGame()
    {
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
