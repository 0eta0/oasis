using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Condition
{
	public string type;

	public Condition(string type) {
		this.type = type;
	}
}
