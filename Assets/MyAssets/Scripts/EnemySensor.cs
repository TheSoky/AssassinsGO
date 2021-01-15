using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    
    public Vector3 directionToSearch = new Vector3(0.0f, 0.0f, 2.0f);

    private Node m_nodeToSearch;
    private Board m_board;

    private bool m_foundPlayer = false;
    public bool FoundPlayer { get { return m_foundPlayer; } }

	private void Awake()
	{
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
	}

    public void UpdateSensor(Node enemyNode)
	{
        Vector3 worldSpacePositionToSearch = transform.TransformVector(directionToSearch) + transform.position;

        if(m_board != null)
		{
            m_nodeToSearch = m_board.FindNodeAt(worldSpacePositionToSearch);

            if(!enemyNode.LinkedNodes.Contains(m_nodeToSearch))
			{
                m_foundPlayer = false;
                return;
			}

            if(m_nodeToSearch == m_board.PlayerNode)
			{
                m_foundPlayer = true;
			}
		}
	}

}
