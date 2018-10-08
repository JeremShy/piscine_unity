using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

	public List<Node> path = null;

	public Vector2 startVec, destVec;
	private Node startNode, destNode;

	public bool computed = false;
	PriorityQueue<Node>.fcomparison h;

	private PriorityQueue<Node> openList;
	private Dictionary<int, bool> closedList;

	public Path(Vector2 start, Vector2 dest)
	{
		startVec = start;
		destVec = dest;

		startNode = Map.map.getClosestNode(startVec);
		destNode = Map.map.getClosestNode(destVec);

		h = delegate(Node n1, Node n2) {
			float h1 = Vector2.Distance(n1.pos, destVec) + n1.n * Map.map.distanceNodes.x;
			float h2 = Vector2.Distance(n2.pos, destVec) + n2.n * Map.map.distanceNodes.x;
			if (h1 < h2)
				return (1);
			else if (h1 == h2)
				return (0);
			else
				return (-1);
		};

		openList = new PriorityQueue<Node>(h);
		closedList = new Dictionary<int, bool>();
		computed = false;
	}

	public void changePath(Vector2 start, Vector2 dest)
	{
		startVec = start;
		destVec = dest;

		startNode = Map.map.getClosestNode(startVec);
		destNode = Map.map.getClosestNode(destVec);
		openList = new PriorityQueue<Node>(h);
		closedList = new Dictionary<int, bool>();
		computed = false;
	}

	public void	ComputePath()
	{
		// Debug.Log("Adding " + startNode.id + " to openList");
		startNode.n = 0;
		openList.Push(startNode);
		while (openList.Count() > 0)
		{
			Node n = openList.Pop();
			// Debug.Log("Removing " + n.id + " from openList");
			if (n.id == destNode.id)
			{
				// Debug.Log("Found the end in " + n.n + " operations");
				path = new List<Node>();
				if (n.id == startNode.id)
				{
					path.Add(n);
					computed = true;
					return ;
				}
				do {
					path.Add(n);
					n = n.prec;
				} while (n.id != startNode.id);
				path.Reverse();
				computed = true;
				return ;
			}
			// Debug.Log("Examining node " + n.id + ", it has " + n.links.Count + " links");
			foreach (Node voisin in n.links)
			{
				if (closedList.ContainsKey(voisin.id) || openList.lst.Contains(voisin))
				{
					// Debug.Log(voisin.id + " is already in the closedList");
				}
				else
				{
					// Debug.Log(voisin.id + " isn't in the closedList");
					// if (voisin.id == n.id)
						// Debug.LogError("wtf");
					voisin.n = n.n + 1;
					voisin.prec = n;
					// Debug.Log("Adding " + voisin.id + " to the openList");
					openList.Push(voisin);
				}
			}
			// Debug.Log("adding " + n.id + " to the closedList");
			if (!closedList.ContainsKey(n.id))
				closedList.Add(n.id, true);
		}
	}
}
