﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	private float m_h;
	public float H
	{ get { return m_h; } }

	private float m_v;
	public float V
	{ get { return m_v; } }

	private bool m_inputEnabled = false;
	public bool InputEnabled { get { return m_inputEnabled; } set { m_inputEnabled = value; } }

    public void GetKeyInput()
	{
		if (m_inputEnabled)
		{
			m_h = Input.GetAxisRaw("Horizontal");
			m_v = Input.GetAxisRaw("Vertical");
		}
		else
		{
			m_h = 0.0f;
			m_v = 0.0f;
		}

	}

}
