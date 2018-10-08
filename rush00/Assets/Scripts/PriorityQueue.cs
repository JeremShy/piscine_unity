using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> {

	public delegate float fcomparison(T n1, T n2);
	public delegate void fiter(T n);

	public List<T> lst;
	
	private fcomparison f;

	public int Count()
	{
		return (lst.Count);
	}

	public PriorityQueue(fcomparison c)
	{
		lst = new List<T>();
		f = c;
	}

	public void Push(T elem)
	{
		int i = 0;

		while (i < lst.Count)
		{
			if (f(elem, lst[i]) > 0)
			{
				lst.Insert(i, elem);
				return ;
			}
			i++;
		}
		lst.Add(elem);
	}

	public void Iter(fiter f)
	{
		int i = 0;

		while (i < lst.Count)
		{
			f(lst[i]);
			i++;
		}
	}

	public T Pop()
	{
		T ret = lst[0];
		lst.RemoveAt(0);
		return (ret);
	}

}
