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

	private List<Node> m_allNodes = new List<Node>();
	public List<Node> AllNodes { get { return m_allNodes; } }

	private Node m_playerNode;
	public Node PlayerNode { get { return m_playerNode; } }

	private PlayerMover m_player;

	private void Awake()
	{
		m_player = Object.FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
		GetNodeList();
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

	private void OnDrawGizmos()
	{
		if(m_playerNode != null)
		{
			Gizmos.color = new Color(0.0f, 1.0f, 1.0f, 0.5f);
			Gizmos.DrawSphere(m_playerNode.transform.position, 0.2f);
		}
	}

}