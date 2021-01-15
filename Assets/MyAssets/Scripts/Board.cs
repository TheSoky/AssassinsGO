using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

	public static float spacing = 2.0f;

	public static readonly Vector2[] directions =
	{
		new Vector2(spacing, 0.0f),
		new Vector2(-spacing, 0.0f),
		new Vector2(0.0f, spacing),
		new Vector2(0.0f, -spacing)
	};

	public GameObject goalPrefab;
	public float drawGoalTime = 2.0f;
	public float drawGoalDelay = 2.0f;
	public iTween.EaseType drawGoalEaseType = iTween.EaseType.easeOutExpo;

	public List<Transform> capturePositions;

	public float capturePositionIconSize = 0.4f;
	public Color capturePositionIconColor = Color.blue;

	private int m_currentCapturePosition = 0;
	public int CurrentCapturePosition { get => m_currentCapturePosition; set => m_currentCapturePosition = value; }

	private List<Node> m_allNodes = new List<Node>();
	public List<Node> AllNodes { get { return m_allNodes; } }

	private Node m_playerNode;
	public Node PlayerNode { get { return m_playerNode; } }

	private Node m_goalNode;
	public Node GoalNode { get { return m_goalNode; } }

	private PlayerMover m_player;

	private void Awake()
	{
		m_player = Object.FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
		GetNodeList();

		m_goalNode = FindGoalNode();
	}

	public void GetNodeList()
	{
		Node[] nList = GameObject.FindObjectsOfType<Node>();
		m_allNodes = new List<Node>(nList);
	}

	public Node FindNodeAt(Vector3 pos)
	{
		Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
		return m_allNodes.Find(n => n.Coordinate == boardCoord);
	}

	public Node FindPlayerNode()
	{
		if(m_player != null && !m_player.isMoving)
		{
			return FindNodeAt(m_player.transform.position);
		}
		return null;
	}

	public void UpdatePlayerNode()
	{
		m_playerNode = FindPlayerNode();
	}

	public void DrawGoal()
	{
		if(goalPrefab != null && m_goalNode != null)
		{
			GameObject goalInstance = Instantiate(goalPrefab, m_goalNode.transform.position, Quaternion.identity);

			iTween.ScaleFrom(goalInstance, iTween.Hash(
					"scale", Vector3.zero,
					"time", drawGoalTime,
					"delay", drawGoalDelay,
					"easetype", drawGoalEaseType
				));
		}
	}

	public void InitBoard()
	{
		if(m_playerNode != null)
		{
			m_playerNode.InitNode();
		}
	}

	public List<EnemyManager> FindEnemiesAt(Node node)
	{
		List<EnemyManager> foundEnemies = new List<EnemyManager>();

		EnemyManager[] enemies = Object.FindObjectsOfType<EnemyManager>() as EnemyManager[];

		foreach(EnemyManager enemy in enemies)
		{
			EnemyMover mover = enemy.GetComponent<EnemyMover>();
			if(mover.CurrentNode == node)
			{
				foundEnemies.Add(enemy);
			}
		}

		return foundEnemies;
	}

	private Node FindGoalNode()
	{
		return m_allNodes.Find(n => n.isLevelGoal);
	}

	private void OnDrawGizmos()
	{
		if(m_playerNode != null)
		{
			Gizmos.color = new Color(0.0f, 1.0f, 1.0f, 0.5f);
			Gizmos.DrawSphere(m_playerNode.transform.position, 0.2f);
		}

		Gizmos.color = capturePositionIconColor;
		foreach(Transform capturePos in capturePositions)
		{
			Gizmos.DrawCube(capturePos.position, Vector3.one * capturePositionIconSize);
		}
	}

}