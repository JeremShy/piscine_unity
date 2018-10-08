using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
	public delegate void iterNode(Node n);
	public List<Node> links;
	public bool occupied;

	public Vector2 pos;
	
	private static int curId = 0;

	public Node prec;

	public int id;

	public int n;

	public Node(List<Node> lst, bool occ)
	{
		links = lst;
		occupied = occ;
		id = curId;
		Node.curId++;
		n = 0;
	}

	public Node(bool occ, Vector2 mpos)
	{
		links = null;
		occupied = occ;
		id = curId;
		Node.curId++;
		pos = mpos;
		n = 0;
	}

	public void Iter(iterNode n)
	{
		Dictionary<int, bool> visited = new Dictionary<int, bool>();
		Queue<Node> worklist = new Queue<Node>();

		visited.Add(this.id, true);
		worklist.Enqueue(this);
		while (worklist.Count != 0)
		{
			Node cur = worklist.Dequeue();
			n(cur);
			foreach (Node neigbor in cur.links)
			{
				if (!visited.ContainsKey(neigbor.id))
				{
					visited.Add(neigbor.id, true);
					worklist.Enqueue(neigbor);
				}
			}
		}
	}
}
