using UnityEngine;
using System.Collections;

namespace DG.DemiLib
{
	internal class GUIDragData
	{
		public int currDragIndex;
		public bool currDragSet;
		public IList draggableList;
		public readonly object draggedItem;
		public readonly int draggedItemIndex;
		public object optionalData;

		public GUIDragData(IList draggableList, object draggedItem, int draggedItemIndex, object optionalData)
		{
			this.draggedItem = draggedItem;
			this.draggedItemIndex = draggedItemIndex;
			this.draggableList = draggableList;
			this.optionalData = optionalData;
		}
	}
}

