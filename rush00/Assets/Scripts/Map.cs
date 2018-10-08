using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Map : MonoBehaviour {
	[HideInInspector] static public Map map;

	[HideInInspector] public Node firstNode = null;
    [HideInInspector] public Dictionary<Vector2, Node> dico = new Dictionary<Vector2, Node>();

	public GameObject basGaucheObj;
	public GameObject hautDroiteObj;
	public Vector2 distanceNodes;

    private Vector2 basGauche;
    private Vector2 hautDroite;

    private float floatRound(float f, float toVal)
    {
        float f1 = (float)Math.Round(f * (1.0f / toVal), MidpointRounding.AwayFromZero);
        float f2 = (float)Math.Round(f1 / (1.0f / toVal), 1);
        return (f2);
    }

    public Node getClosestNode(Vector2 pos)
    {
        Node ret;
        Vector2 npos;

        npos.x = floatRound(pos.x, distanceNodes.x);
        npos.y = floatRound(pos.y, distanceNodes.y);

        ret = dico[npos];
        return (ret);
    }

    bool isOccupied(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), float.PositiveInfinity, LayerMask.GetMask("wall"));
        if (hit.collider && hit.collider.tag == "wall")
            return (true);
        else
            return (false);
    }

    void FillDico()
    {
        Vector2 current = basGauche;

        while (true)
        {
            dico.Add(current, new Node(isOccupied(current), current));
            if (current.x <= hautDroite.x)
            {
                current.x += distanceNodes.x;
            }
            else
            {
                current.x = basGauche.x;
                current.y += distanceNodes.y;
            }
            if (current.y > hautDroite.y)
            {
                return ;
            }
        }
    }

    bool IsInRectangle(Vector2 pos, Vector2 basGauche, Vector2 hautDroite)
    {
        if (pos.x >= basGauche.x && pos.y >= basGauche.y && pos.x <= hautDroite.x && pos.y <= hautDroite.y)
            return (true);
        else
            return (false);
    }

    List<Node> GetNeighbors(Vector2 origin)
    {
        List<Node> ret = new List<Node>();
        Vector2 tmp = new Vector2();

        tmp.Set(origin.x + distanceNodes.x, origin.y);
        if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
            ret.Add(dico[tmp]);
        tmp.Set(origin.x - distanceNodes.x, origin.y);
        if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
            ret.Add(dico[tmp]);
        tmp.Set(origin.x, origin.y + distanceNodes.y);
        if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
            ret.Add(dico[tmp]);
        tmp.Set(origin.x, origin.y - distanceNodes.y);
        if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
            ret.Add(dico[tmp]);

        // tmp.Set(origin.x + distanceNodes.x, origin.y + distanceNodes.y);
        // if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
        //     ret.Add(dico[tmp]);
        // tmp.Set(origin.x + distanceNodes.x, origin.y - distanceNodes.y);
        // if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
        //     ret.Add(dico[tmp]);
        // tmp.Set(origin.x - distanceNodes.x, origin.y + distanceNodes.y);
        // if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
        //     ret.Add(dico[tmp]);
        // tmp.Set(origin.x - distanceNodes.x, origin.y - distanceNodes.y);
        // if (IsInRectangle(tmp, basGauche, hautDroite) && dico[tmp].occupied == false)
        //     ret.Add(dico[tmp]);

        return (ret);
    }

    void FillNodes()
    {
        Vector2 current = basGauche;

        while (true)
        {
            List<Node> neighbors = GetNeighbors(current);
            dico[current].links = neighbors;

            if (current.x <= hautDroite.x)
                current.x += distanceNodes.x;
            else
            {
                current.x = basGauche.x;
                current.y += distanceNodes.y;
            }
            if (current.y > hautDroite.y)
                return ;
        }
    }

    void PrintNodes()
    {
        Node.iterNode f = delegate(Node n)
        {
            Vector2 npos = new Vector2();

            int numberPerLines = Mathf.FloorToInt((hautDroite.x - basGauche.x) * (1.0f / distanceNodes.x));

            npos.x = basGauche.x + ((n.id % numberPerLines) * distanceNodes.x);
            npos.y = basGauche.y + ((n.id / numberPerLines) * distanceNodes.y);

        };
        firstNode.Iter(f);
    }

	// Use this for initialization
	void Awake () {
        if (map == null)
            map = this;
        basGauche = basGaucheObj.transform.position;
        hautDroite = hautDroiteObj.transform.position;

        Debug.Log("basGauche : " + basGauche);
        Debug.Log("hautDroite : " + hautDroite);
        FillDico();
        Debug.Log("Dico size : " + dico.Count);
        FillNodes();

        firstNode = dico[basGauche];
        PrintNodes();
	}
    void OnDrawGizmos()
    {
        // if (firstNode != null)
        // {
        //     Node.iterNode f = delegate(Node n)
        //     {
        //         if (n.occupied)
        //             Gizmos.color = Color.red;
        //         else
        //             Gizmos.color = Color.green;
        //         Gizmos.DrawSphere(n.pos, .1f);
        //     };
        //     firstNode.Iter(f);
        // }
    
    }

	void Update () {
		
	}
}


/*
                (26.5, 5.0)

(-10.0, -14.5)

x size : 36.5 = 73
y size : 19.5 = 39

73 * 39 = 2847

*/