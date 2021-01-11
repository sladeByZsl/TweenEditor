using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

namespace DG.DemiLib
{
	public static class DeGUIDrag
	{
		private static readonly Color _DefDragColor;
		private static GUIDragData _dragData;
		private static int _dragId;
		private static Editor _editor;
		private static EditorWindow _editorWindow;
		private static bool _waitingToApplyDrag;

		public static object draggedItem
		{
			get
			{ 
				if (_dragData == null) 
				{
					return null;
				}
				return _dragData.draggedItem;
			}
		}

		public static int draggedItemOriginalIndex
		{
			get
			{
				if (_dragData == null) 
				{
					return -1;
				}
				return _dragData.draggedItemIndex;
			}
		}

		public static Type draggedItemType
		{
			get
			{
				if (_dragData == null) 
				{
					return null;
				}
				return _dragData.draggedItem.GetType ();
			}
		}

		public static bool isDragging
		{
			get
			{
				return (_dragData != null);
			}
		}

		public static object optionalDragData
		{
			get
			{
				if (_dragData == null) 
				{
					return null;
				}
				return _dragData.optionalData;
			}
		}

		static DeGUIDrag()
		{
			_DefDragColor = new Color(0.1720873f, 0.4236527f, 0.35f);
		}


		private static void ApplyDrag()
		{
			if (_dragData != null) 
			{
				int draggedItemIndex = _dragData.draggedItemIndex;
				int num2 = (_dragData.currDragIndex > _dragData.draggedItemIndex) ? (_dragData.currDragIndex - 1) : _dragData.currDragIndex;
				if (num2 != draggedItemIndex) 
				{
					int num4;
					int num3 = draggedItemIndex;
					while (num3 > num2) 
					{
						num4 = num3;
						num3 = num4 - 1;
						_dragData.draggableList [num3 + 1] = _dragData.draggableList [num3];
						_dragData.draggableList [num3] = _dragData.draggedItem;
					}
					while (num3 < num2) 
					{
						num4 = num3;
						num3 = num4 + 1;
						_dragData.draggableList [num3 - 1] = _dragData.draggableList [num3];
						_dragData.draggableList [num3] = _dragData.draggedItem;
					}
				}
				Reset ();
				Repaint ();
			}
		}

		public static void StartDrag(int dragId, Editor editor, IList draggableList, int draggedItemIndex, object optionalData=null)
		{
			if (_dragData == null) 
			{
				Reset ();
				_editorWindow = null;
				_editor = editor;
				_dragId = dragId;
				_dragData = new GUIDragData (draggableList, draggableList [draggedItemIndex], draggedItemIndex, optionalData);
			}
		}

		public static void StartDrag(int dragId, EditorWindow editorWindow, IList draggableList, int draggedItemIndex, object optionalData=null)
		{
			if (_dragData == null) 
			{
				Reset ();
				_editorWindow = editorWindow;
				_editor = null;
				_dragId = dragId;
				_dragData = new GUIDragData (draggableList, draggableList [draggedItemIndex], draggedItemIndex, optionalData);
			}
		}

		public static bool Drag(int dragId, IList draggableList, int currDraggableItemIndex)
		{
			return Drag (dragId, draggableList, currDraggableItemIndex, _DefDragColor);
		}

		public static bool Drag(int dragId, IList draggableList, int currDraggableItemIndex, Color dragEvidenceColor)
		{
			if ((_dragData == null) || (_dragId != dragId)) 
			{
				return false;
			}
			if (_waitingToApplyDrag) 
			{
				if (Event.current.type == EventType.Repaint) 
				{
					Event.current.Use ();
				}
				if (Event.current.type == EventType.Used) 
				{
					ApplyDrag ();
				}
				return false;
			}
			_dragData.draggableList = draggableList;
			int count = _dragData.draggableList.Count;
			if ((currDraggableItemIndex == 0) && (Event.current.type == EventType.Repaint)) 
			{
				_dragData.currDragSet = false;
			}
			if (!_dragData.currDragSet) 
			{
				Rect lastRect = GUILayoutUtility.GetLastRect ();
				float num2 = lastRect.yMin + (lastRect.height * 0.5f);
				float y = Event.current.mousePosition.y;
				if ((currDraggableItemIndex <= (count - 1)) && (y <= num2)) {
					DeGUI.FlatDivider (new Rect (lastRect.xMin, lastRect.yMin - 1f, lastRect.width, 2f), new Color? (dragEvidenceColor));
					_dragData.currDragIndex = currDraggableItemIndex;
					_dragData.currDragSet = true;
				} 
				else if ((currDraggableItemIndex >= (count - 1)) && (y > num2)) 
				{
					DeGUI.FlatDivider (new Rect (lastRect.xMin, lastRect.yMax - 1f, lastRect.width, 2f), new Color? (dragEvidenceColor));
					_dragData.currDragIndex = count;
					_dragData.currDragSet = true;
				}
			}
			if (_dragData.draggedItemIndex == currDraggableItemIndex) 
			{
				Color color = dragEvidenceColor;
				color.a = 0.35f;
				DeGUI.FlatDivider (GUILayoutUtility.GetLastRect (), new Color? (color));
			}
			return ((GUIUtility.hotControl < 1) && EndDrag (true));
		}

		public static bool EndDrag(bool applyDrag)
		{
			if (_dragData == null) 
			{
				return false;
			}
			if(applyDrag)
			{
				bool flag4 = (_dragData.currDragIndex < _dragData.draggedItemIndex) || (_dragData.currDragIndex > (_dragData.draggedItemIndex + 1));
				if (Event.current.type == EventType.Repaint) 
				{
					Event.current.Use ();
					return flag4;
				}
				if (Event.current.type == EventType.Used) 
				{
					ApplyDrag ();
					return flag4;
				}
				_waitingToApplyDrag = true;
				return flag4;
			}
			Reset ();
			return true;
		}

		private static void Repaint()
		{
			if (_editor != null) {
				_editor.Repaint ();
			} 
			else if (_editorWindow != null) 
			{
				_editorWindow.Repaint ();
			}
		}

		private static void Reset()
		{
			_dragData = null;
			_dragId = -1;
			_waitingToApplyDrag = false;
		}
	}
}
