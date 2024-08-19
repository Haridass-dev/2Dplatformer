// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CnControls
{
	public class TouchpadInputHelper : BaseInputHelper
	{
		private Touchpad _linkedTouchpad;

		private RectTransform _touchpadTouchRect;

		protected override void Awake()
		{
			base.Awake();
			this._linkedTouchpad = base.GetComponent<Touchpad>();
			this._linkedTouchpad.CurrentEventCamera = this.UiRootCamera;
			this._touchpadTouchRect = this._linkedTouchpad.GetComponent<RectTransform>();
		}

		public void Update()
		{
			for (int i = 0; i < CnInputManager.TouchCount; i++)
			{
				Touch touch = CnInputManager.GetTouch(i);
				this.PointerEventDataCache.position = touch.position;
				this.PointerEventDataCache.delta = touch.deltaPosition;
				if (RectTransformUtility.RectangleContainsScreenPoint(this._touchpadTouchRect, touch.position, this.UiRootCamera) && touch.phase == TouchPhase.Began && this.LastFingerId == -1)
				{
					this._linkedTouchpad.OnPointerDown(this.PointerEventDataCache);
					this.LastFingerId = touch.fingerId;
					return;
				}
				if (touch.phase == TouchPhase.Ended && touch.fingerId == this.LastFingerId)
				{
					this._linkedTouchpad.OnPointerUp(this.PointerEventDataCache);
					this.LastFingerId = -1;
					return;
				}
				if (touch.phase == TouchPhase.Moved && touch.fingerId == this.LastFingerId)
				{
					this._linkedTouchpad.OnDrag(this.PointerEventDataCache);
					return;
				}
			}
			this.PointerEventDataCache.position = Input.mousePosition;
			this.PointerEventDataCache.delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			this.PointerEventDataCache.delta *= 10f;
			if (RectTransformUtility.RectangleContainsScreenPoint(this._touchpadTouchRect, this.PointerEventDataCache.position, this.UiRootCamera) && Input.GetMouseButtonDown(0))
			{
				this._linkedTouchpad.OnPointerDown(this.PointerEventDataCache);
				this.LastFingerId = 255;
				return;
			}
			if (Input.GetMouseButtonUp(0) && this.LastFingerId == 255)
			{
				this._linkedTouchpad.OnPointerUp(this.PointerEventDataCache);
				this.LastFingerId = -1;
				return;
			}
			if (Input.GetMouseButton(0) && this.LastFingerId == 255 && this.PointerEventDataCache.delta.sqrMagnitude > 1E-09f)
			{
				this._linkedTouchpad.OnDrag(this.PointerEventDataCache);
			}
		}
	}
}
