using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGAOSG
{
	public class DebugManager : SingletonMonoBehaviour<DebugManager>
	{
		public static void DrawPoint (Vector3 point, float radius, Color color, float duration)
		{
			Debug.DrawLine(point + (Vector3.right * radius), point + (Vector3.left * radius), color, duration);
			Debug.DrawLine(point + (Vector3.up * radius), point + (Vector3.down * radius), color, duration);
			Debug.DrawLine(point + (Vector3.forward * radius), point + (Vector3.back * radius), color, duration);
		}

		public static void DrawRect (Rect rect, Color color, float duration)
		{
			Debug.DrawLine(rect.min, new Vector2(rect.xMin, rect.yMax), color, duration);
			Debug.DrawLine(new Vector2(rect.xMin, rect.yMax), rect.max, color, duration);
			Debug.DrawLine(rect.max, new Vector2(rect.xMax, rect.yMin), color, duration);
			Debug.DrawLine(new Vector2(rect.xMax, rect.yMin), rect.min, color, duration);
		}
	}
}