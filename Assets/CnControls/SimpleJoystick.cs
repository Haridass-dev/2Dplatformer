// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CnControls
{
	public class SimpleJoystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		public float MovementRange = 50f;

		public string HorizontalAxisName = "Horizontal";

		public string VerticalAxisName = "Vertical";

		[Space(15f), Tooltip("Should the joystick be hidden on release?")]
		public bool HideOnRelease;

		[Tooltip("Should the Base image move along with the finger without any constraints?")]
		public bool MoveBase = true;

		[Tooltip("Should the joystick snap to finger? If it's FALSE, the MoveBase checkbox logic will be ommited")]
		public bool SnapsToFinger = true;

		[Tooltip("Constraints on the joystick movement axis")]
		public ControlMovementDirection JoystickMoveAxis = ControlMovementDirection.Both;

		[Tooltip("Image of the joystick base")]
		public Image JoystickBase;

		[Tooltip("Image of the stick itself")]
		public Image Stick;

		[Tooltip("Touch Zone transform")]
		public RectTransform TouchZone;

		private Vector2 _initialStickPosition;

		private Vector2 _intermediateStickPosition;

		private Vector2 _initialBasePosition;

		private RectTransform _baseTransform;

		private RectTransform _stickTransform;

		[HideInInspector]public bool released;

		private float x;

		private float y;

		private float _oneOverMovementRange;

		protected VirtualAxis HorizintalAxis;

		protected VirtualAxis VerticalAxis;

        public Vector2 magnitude_;

		public Camera CurrentEventCamera
		{
			get;
			set;
		}

		private void Update()
		{
			if (this.released)
			{
				this.x = 0f;//Mathf.MoveTowards(this.x, 0f, 0.03f);
				this.y = 0f;//Mathf.MoveTowards(this.y, 0f, 0.03f);
				this.HorizintalAxis.Value = 0f;
				this.VerticalAxis.Value = 0f;
			}
		}

		private void Awake()
		{
			this._stickTransform = this.Stick.GetComponent<RectTransform>();
			this._baseTransform = this.JoystickBase.GetComponent<RectTransform>();
			this._initialStickPosition = this._stickTransform.anchoredPosition;
			this._intermediateStickPosition = this._initialStickPosition;
			this._initialBasePosition = this._baseTransform.anchoredPosition;
			this._stickTransform.anchoredPosition = this._initialStickPosition;
			this._baseTransform.anchoredPosition = this._initialBasePosition;
			this._oneOverMovementRange = 1f / this.MovementRange;
			if (this.HideOnRelease)
			{
				this.Hide(true);
			}
		}

		private void OnEnable()
		{
			this.HorizintalAxis = (this.HorizintalAxis ?? new VirtualAxis(this.HorizontalAxisName));
			this.VerticalAxis = (this.VerticalAxis ?? new VirtualAxis(this.VerticalAxisName));
			CnInputManager.RegisterVirtualAxis(this.HorizintalAxis);
			CnInputManager.RegisterVirtualAxis(this.VerticalAxis);
		}

		private void OnDisable()
		{
            if (!released) {
                x = 0f;
                y = 0f;
                this.HorizintalAxis.Value = 0f;
                this.VerticalAxis.Value = 0f;
            }
			CnInputManager.UnregisterVirtualAxis(this.HorizintalAxis);
			CnInputManager.UnregisterVirtualAxis(this.VerticalAxis);
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			this.x = CnInputManager.GetAxis("Horizontal");
			this.y = CnInputManager.GetAxis("Vertical");
			this.CurrentEventCamera = (eventData.pressEventCamera ?? this.CurrentEventCamera);
			Vector3 position;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(this._stickTransform, eventData.position, this.CurrentEventCamera, out position);
			this._stickTransform.position = position;
			Vector2 anchoredPosition = this._stickTransform.anchoredPosition;
			if ((this.JoystickMoveAxis & ControlMovementDirection.Horizontal) == (ControlMovementDirection)0)
			{
				anchoredPosition.x = this._intermediateStickPosition.x;
			}
			if ((this.JoystickMoveAxis & ControlMovementDirection.Vertical) == (ControlMovementDirection)0)
			{
				anchoredPosition.y = this._intermediateStickPosition.y;
			}
			this._stickTransform.anchoredPosition = anchoredPosition;
			Vector2 a = new Vector2(anchoredPosition.x, anchoredPosition.y) - this._intermediateStickPosition;
			float magnitude = a.magnitude;
            magnitude_ = a;
			Vector2 a2 = a / magnitude;
			if (magnitude > this.MovementRange)
			{
				if (this.MoveBase && this.SnapsToFinger)
				{
					float d = a.magnitude - this.MovementRange;
					Vector2 b = a2 * d;
					this._baseTransform.anchoredPosition += b;
					this._intermediateStickPosition += b;
				}
				else
				{
					this._stickTransform.anchoredPosition = this._intermediateStickPosition + a2 * this.MovementRange;
				}
			}
			float value = Mathf.Clamp(a.x * this._oneOverMovementRange, -1f, 1f);
			float value2 = Mathf.Clamp(a.y * this._oneOverMovementRange, -1f, 1f);
			this.HorizintalAxis.Value = value;
			this.VerticalAxis.Value = value2;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			this._baseTransform.anchoredPosition = this._initialBasePosition;
			this._stickTransform.anchoredPosition = this._initialStickPosition;
			this._intermediateStickPosition = this._initialStickPosition;
			this.released = true;
			if (this.HideOnRelease)
			{
				this.Hide(true);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.released = false;
			if (this.SnapsToFinger)
			{
				this.CurrentEventCamera = (eventData.pressEventCamera ?? this.CurrentEventCamera);
				Vector3 position;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(this._stickTransform, eventData.position, this.CurrentEventCamera, out position);
				Vector3 position2;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(this._baseTransform, eventData.position, this.CurrentEventCamera, out position2);
				this._baseTransform.position = position2;
				this._stickTransform.position = position;
				this._intermediateStickPosition = this._stickTransform.anchoredPosition;
			}
			else
			{
				this.OnDrag(eventData);
			}
			if (this.HideOnRelease)
			{
				this.Hide(false);
			}
		}

		private void Hide(bool isHidden)
		{
			this.JoystickBase.gameObject.SetActive(!isHidden);
			this.Stick.gameObject.SetActive(!isHidden);
		}
	}
}
