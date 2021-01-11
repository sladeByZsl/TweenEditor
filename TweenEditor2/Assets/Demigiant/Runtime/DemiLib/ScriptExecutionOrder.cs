using System;

namespace DG.DemiLib.Attributes
{
	public class ScriptExecutionOrder : Attribute 
	{

		public int order;

		public ScriptExecutionOrder(int order)
		{
			this.order = order;
		}
	}

}
