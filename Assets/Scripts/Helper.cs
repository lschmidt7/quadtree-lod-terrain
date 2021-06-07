using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
	public static Vector3 Mean(Vector3 p1, Vector3 p2)
	{
		return new Vector3((p1.x + p2.x) / 2f, 0, (p1.z + p2.z) / 2f);
	}
}