// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
	public class Dpad : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		public DpadAxis[] DpadAxis;

		public Camera CurrentEventCamera
		{
			get;
			set;
		}

		private void Awake()
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.CurrentEventCamera = (eventData.pressEventCamera ?? this.CurrentEventCamera);
			DpadAxis[] dpadAxis = this.DpadAxis;
			for (int i = 0; i < dpadAxis.Length; i++)
			{
				DpadAxis dpadAxis2 = dpadAxis[i];
				if (RectTransformUtility.RectangleContainsScreenPoint(dpadAxis2.RectTransform, eventData.position, this.CurrentEventCamera))
				{
					dpadAxis2.Press(eventData.position, this.CurrentEventCamera, eventData.pointerId);
				}
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			DpadAxis[] dpadAxis = this.DpadAxis;
			for (int i = 0; i < dpadAxis.Length; i++)
			{
				DpadAxis dpadAxis2 = dpadAxis[i];
				dpadAxis2.TryRelease(eventData.pointerId);
			}
		}
	}
}
