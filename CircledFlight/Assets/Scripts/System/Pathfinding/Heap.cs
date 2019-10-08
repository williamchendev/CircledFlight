using System.Collections;
using System.Collections.Generic;
using System;

public class Heap<T> where T : IHeap<T> {

    //Data
    T[] data;
    int count;

    public Heap(int maxCount){
        data = new T[maxCount];
    }

    public void Add(T item){
        item.HeapIndex = count;
        data[count] = item;
        sortUp(item);
        count++;
    }

    public T removeFirst(){
        T item = data[0];
        count--;
        data[0] = data[count];
        data[0].HeapIndex = 0;
        sortDown(data[0]);
        return item;
    }

    public void UpdateData(T item){
        sortUp(item);
    }

    public int Count{
        get{
            return count;
        }
    }

    public bool Contains(T item){
        return Equals(data[item.HeapIndex], item);
    }

    private void sortUp(T item){
        int parent_index = (item.HeapIndex - 1) / 2;

        while (true){
            T parent = data[parent_index];
            if (item.CompareTo(parent) > 0){
                swap(parent, item);
            }
            else {
                break;
            }

            parent_index = (item.HeapIndex - 1) / 2;
        }
    }

    private void sortDown(T item){
        while (true){
            int left_child = (item.HeapIndex * 2) + 1;
            int right_child = (item.HeapIndex * 2) + 2;
            int swap_index = 0;

            if (left_child < count){
                swap_index = left_child;

                if (right_child < count){
                    if (data[left_child].CompareTo(data[right_child]) < 0){
                        swap_index = right_child;
                    }
                }

                if (item.CompareTo(data[swap_index]) < 0){
                    swap(item, data[swap_index]);
                }
                else {
                    return;
                }
            }
            else {
                return;
            }
        }
    }

    private void swap(T itemA, T itemB){
        data[itemA.HeapIndex] = itemB;
        data[itemB.HeapIndex] = itemA;
        int tempIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tempIndex;
    }

}

public interface IHeap<T> : IComparable<T> {
    int HeapIndex{
        get;
        set;
    }
}