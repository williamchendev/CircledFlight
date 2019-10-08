using System.Collections;
using UnityEngine;

public class Node : IHeap<Node> {

	private bool empty;
    private Vector2 position;
    private int grid_position;

    public int gcost;
    public int hcost;
    public Node parent;
    private int heapindex;

    public Node (bool place_free, float x, float y, int grid_num){
        empty = place_free;
        position = new Vector2(x, y);
        grid_position = grid_num;
    }

    public bool isEmpty (){
        return empty;
    }

    public int getGridNum(){
        return grid_position;
    }

    public Vector2 getPosition(){
        return position;
    }

    public int fcost{
        get {
            return (gcost + hcost);
        }
    }

    public int CompareTo(Node comparable){
        int compare = fcost.CompareTo(comparable.fcost);
        if (compare == 0){
            compare = hcost.CompareTo(comparable.hcost);
        }
        return -compare;
    }

    public int HeapIndex {
        get
        {
            return heapindex;
        }

        set
        {
            heapindex = value;
        }
    }
}
