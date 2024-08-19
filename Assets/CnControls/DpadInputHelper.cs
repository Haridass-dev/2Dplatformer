// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CnControls
{
	public class DpadInputHelper : BaseInputHelper
	{
		private Dpad _linkedDpad;

		protected override void Awake()
		{
			base.Awake();
			this._linkedDpad = base.GetComponent<Dpad>();
			this._linkedDpad.CurrentEventCamera = this.UiRootCamera;
		}

		private void Update()
		{
			for (int i = 0; i < CnInputManager.TouchCount; i++)
			{
				Touch touch = CnInputManager.GetTouch(i);
				this.PointerEventDataCache.position = touch.position;
				this.PointerEventDataCache.pointerId = touch.fingerId;
				if (touch.phase == TouchPhase.Began)
				{
					this._linkedDpad.OnPointerDown(this.PointerEventDataCache);
					return;
				}
				if (touch.phase == TouchPhase.Ended)
				{
					this._linkedDpad.OnPointerUp(this.PointerEventDataCache);
					return;
				}
			}
			this.PointerEventDataCache.position = Input.mousePosition;
			this.PointerEventDataCache.pointerId = 255;
			if (Input.GetMouseButtonDown(0))
			{
				this._linkedDpad.OnPointerDown(this.PointerEventDataCache);
				return;
			}
			if (Input.GetMouseButtonUp(0))
			{
				this._linkedDpad.OnPointerUp(this.PointerEventDataCache);
				return;
			}
		}
	}
}
