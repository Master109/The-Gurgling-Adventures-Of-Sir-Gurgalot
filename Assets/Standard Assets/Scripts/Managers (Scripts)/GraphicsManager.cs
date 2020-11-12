﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassExtensions;
using System;

namespace TAoKR
{
	public class GraphicsManager : SingletonMonoBehaviour<GraphicsManager>
	{
		public Material[] filters;
		public Transform spriteMaskTrsPrefab;
		public List<GraphicsOverlap> graphicsOverlaps = new List<GraphicsOverlap>();
		
		public override void Start ()
		{
			if (instance != null || filters == null || filters.Length == 0)
			{
				instance = this;
				return;
			}
			GraphicsManager graphicsManager = CameraScript.instance.camera.gameObject.AddComponent<GraphicsManager>();
			graphicsManager.filters = filters;
			graphicsManager.spriteMaskTrsPrefab = spriteMaskTrsPrefab;
			instance = graphicsManager;
			enabled = false;
		}
		
		void LateUpdate ()
		{
			foreach (GraphicsOverlap graphicsOverlap in graphicsOverlaps)
				graphicsOverlap.Update ();
		}
		
		void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
			foreach (Material filter in filters)
				Graphics.Blit(source, destination, filter);
		}
		
		public class GraphicsOverlap
		{
			public IBlockable blockable1;
			public IBlockable blockable2;
			public Transform maskTrs;
			
			public GraphicsOverlap (IBlockable blockable1, IBlockable blockable2)
			{
				this.blockable1 = blockable1;
				this.blockable2 = blockable2;
				maskTrs = Instantiate(GraphicsManager.instance.spriteMaskTrsPrefab);
			}
			
			public Rect GetOverlap ()
			{
				if (blockable1.Collider != null && blockable1.Collider.isActiveAndEnabled && blockable2.Collider != null && blockable2.Collider.isActiveAndEnabled)
				{
					Rect rect1 = blockable1.Collider.bounds.ToRect();
					Rect rect2 = blockable2.Collider.bounds.ToRect();
					Vector2 overlapRectMin = new Vector2(Mathf.Max(rect1.min.x, rect2.min.x), Mathf.Max(rect1.min.y, rect2.min.y));
					Vector2 overlapRectMax = new Vector2(Mathf.Min(rect1.max.x, rect2.max.x), Mathf.Min(rect1.max.y, rect2.max.y));
					return Rect.MinMaxRect(overlapRectMin.x, overlapRectMin.y, overlapRectMax.x, overlapRectMax.y);
				}
				else
				{
					End ();
					return RectExtensions.NULL;
				}
			}
			
			public void Update ()
			{
				Rect overlapRect = GetOverlap();
				if (maskTrs != null)
				{
					maskTrs.position = overlapRect.center;
					maskTrs.localScale = overlapRect.size.SetZ(1);
				}
			}
			
			public void End ()
			{
				if (maskTrs != null)
					Destroy (maskTrs.gameObject);
			}
		}
	}
}