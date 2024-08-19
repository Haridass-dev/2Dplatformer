// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
	public class BaseInputHelper : MonoBehaviour
	{
		protected PointerEventData PointerEventDataCache;

		protected int LastFingerId = -1;

		protected Camera UiRootCamera;

		protected virtual void Awake()
		{
			this.PointerEventDataCache = new PointerEventData(UnityEngine.Object.FindObjectOfType<EventSystem>());
			this.UiRootCamera = base.GetComponentInParent<Canvas>().worldCamera;
		}
	}
}
