using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Board m_board;
    PlayerManager m_player;

	private bool m_hasLevelStarted = false;	
	private bool m_isGamePlaying = false;	
	private bool m_isGameOver = false;	
	private bool m_hasLevelFinished = false;

	public bool HasLevelStarted { get => m_hasLevelStarted; set => m_hasLevelStarted = value; }
	public bool IsGamePlaying { get => m_isGamePlaying; set => m_isGamePlaying = value; }
	public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
	public bool HasLevelFinished { get => m_hasLevelFinished; set => m_hasLevelFinished = value; }

	public float delay = 1.0f;

	public UnityEvent setUpEvent;
	public UnityEvent startLevelEvent;
	public UnityEvent playLevelEvent;
	public UnityEvent endLevelEvent;

	private void Awake()
	{
		m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
		m_player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
	}

	private void Start()
	{
		if(m_player != null && m_board != null)
		{
			StartCoroutine(RunGameLoop());
		}
		else
		{
			Debug.LogError("GAMEMANAGER Error: no player or board found!");
		}
	}

	public void PlayLevel()
	{
		m_hasLevelStarted = true;
	}

	private IEnumerator RunGameLoop()
	{
		yield return StartCoroutine(StartLevelRoutine());
		yield return StartCoroutine(PlayLevelRoutine());
		yield return StartCoroutine(EndLevelRoutine());
	}

	private IEnumerator StartLevelRoutine()
	{
		if(setUpEvent != null)
		{
			setUpEvent.Invoke();
		}

		m_player.playerInput.InputEnabled = false;
		
		while(!m_hasLevelStarted)
		{

			yield return null;

		}

		if(startLevelEvent != null)
		{
			startLevelEvent.Invoke();
		}
	}

	private IEnumerator PlayLevelRoutine()
	{
		m_isGamePlaying = true;
		yield return new WaitForSeconds(delay);
		m_player.playerInput.InputEnabled = true;

		if(playLevelEvent != null)
		{
			playLevelEvent.Invoke();
		}

		while(!m_isGameOver)
		{
			yield return null;

			m_isGameOver = IsWinner();
		}

	}

	private IEnumerator EndLevelRoutine()
	{
		m_player.playerInput.InputEnabled = false;

		if(endLevelEvent != null)
		{
			endLevelEvent.Invoke();
		}
		
		while(!m_hasLevelFinished)
		{
			
			yield return null;

		}

		RestartLevel();
	}

	private void RestartLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	private bool IsWinner()
	{
		if(m_board.PlayerNode != null)
		{
			return (m_board.PlayerNode == m_board.GoalNode);
		}
		return false;
	}

}
