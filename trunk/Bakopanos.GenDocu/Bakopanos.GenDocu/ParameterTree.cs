using System;
using System.Collections;
using System.Collections.Specialized;

namespace Ciba.Utils.GenDocu
{
	/// <summary>
	/// ParameterNode.
	/// </summary>
	public class ParameterNode
	{
		public int    ParentIndex;
		public int    Index;
		public int    Level;
		public int    ChildCount;
		public string Name;
		public string Value;
		public bool   Complex;
		public bool   ComplexEnd;

		public ParameterNode()
		{
			ParentIndex = -1;
			Index       =  0;
			Level       =  0;
			ChildCount  =  0;
			Name        = "";
			Value       = "";
			Complex     =  false;
			ComplexEnd  =  false;
		}

		public ParameterNode(ParameterNode n)
		{
			ParentIndex = -1;
			Index       =  0;
			Level       =  0;
			ChildCount  =  0;
			Name        =  n.Name;
			Value       =  n.Value;
			Complex     =  n.Complex;
			ComplexEnd  =  n.ComplexEnd;
		}
	}

	/// <summary>
	/// ParameterTree.
	/// </summary>
	public class ParameterTree
	{
		protected ArrayList mNodes;

		public ParameterTree()
		{
			mNodes = new ArrayList();
		}

		public int Count
		{
			get { return mNodes.Count; }
		}

		public ParameterNode this[int i]
		{
			get { return (ParameterNode)mNodes[i]; }
		}

		public int add(int i, ParameterNode n)
		{
			int p = -1;

			if (i < 0) 
			{
				p = mNodes.Count;
				n.ParentIndex = -1;
				n.Index       =  p;
				n.Level       =  0;
				n.ChildCount  =  0;

				mNodes.Add(n);
			}
			else if (i < mNodes.Count)
			{
				p = i + ((ParameterNode)mNodes[i]).ChildCount + 1;
				n.ParentIndex =  i;
				n.Index       =  p;
				n.Level       =  ((ParameterNode)mNodes[i]).Level + 1;
				n.ChildCount  =  0;

				if (p >= mNodes.Count) mNodes.Add(n); else
				{ mNodes.Insert(p, n); updateNodes(p+1); }

				updateCount(i, 1);
			}

			return p;
		}

		public void updateNodes(int iStart)
		{
			for (int i = iStart; i < mNodes.Count; i++)
				((ParameterNode)mNodes[i]).Index = i;
		}

		public void updateCount(int iStart, int n)
		{
			for (int i = iStart; i >= 0; i = ((ParameterNode)mNodes[i]).ParentIndex)
				((ParameterNode)mNodes[i]).ChildCount += n;
		}
	}
}
