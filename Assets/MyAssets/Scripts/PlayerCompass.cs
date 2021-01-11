using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour
{

	public GameObject arrowPrefab;
	public float scale = 1.0f;
	public float startDistance = 0.25f;
	public float endDistance = 0.5f;

	public float moveTime = 1.0f;
	public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
	public float delay = 0.0f;

	private List<GameObject> m_arrows = new List<GameObject>();
    private Board m_board;

	private void Awake()
	{
		m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
		SetupArrows();
	}

	public void ShowArrows(bool state)
	{
		if(m_board == null)
		{
			Debug.LogError("PLAYERCOMPASS ShowArrows ERROR: No board found");
			return;
		}

		if(m_arrows == null || m_arrows.Count != Board.directions.Length)
		{
			Debug.LogError("PLAYERCOMPASS ShowArrows ERROR: No arrows found");
			return;
		}

		if(m_board.PlayerNode != null)
		{
			for(int i = 0; i < Board.directions.Length; i++)
			{
				Node neighbour = m_board.PlayerNode.FindNeighbourAt(Board.directions[i]);

				if(neighbour == null || !state)
				{
					m_arrows[i].SetActive(false);
				}
				else
				{
					bool activeState = m_board.PlayerNode.LinkedNodes.Contains(neighbour);
					m_arrows[i].SetActive(activeState);
				}
			}
		}

		ResetArrows();
		MoveArrows();
	}

	private void SetupArrows()
	{
		if(arrowPrefab == null)
		{
			Debug.LogError("PLAYERCOMPASS SetupArrows ERROR: Missing arrow prefab!");
			return;
		}
		foreach(Vector2 dir in Board.directions)
		{
			Vector3 dirVector = new Vector3(dir.normalized.x, 0.0f, dir.normalized.y);
			Quaternion rotation = Quaternion.LookRotation(dirVector);
			GameObject arrowInstance = Instantiate(arrowPrefab, transform.position + dirVector * startDistance, rotation);
			arrowInstance.transform.localScale = new Vector3(scale, scale, scale);
			arrowInstance.transform.parent = transform;

			m_arrows.Add(arrowInstance);
		}
	}

	private void MoveArrow(GameObject arrowInstance)
	{
		iTween.MoveBy(arrowInstance, iTween.Hash(
				"z", endDistance - startDistance,
				"looptype", iTween.LoopType.loop,
				"time", moveTime,
				"easetype", easeType
			));
	}

	private void MoveArrows()
	{
		foreach(GameObject arrow in m_arrows)
		{
			MoveArrow(arrow);
		}
	}

	private void ResetArrows()
	{
		for(int i = 0; i < Board.directions.Length; i++)
		{
			iTween.Stop(m_arrows[i]);
			Vector3 dirVector = new Vector3(Board.directions[i].normalized.x, 0.0f, Board.directions[i].normalized.y);
			m_arrows[i].transform.position = transform.position + dirVector * startDistance;
		}
	}

}
