using System.IO;
using System.Collections.Generic;

namespace S_T_serialize
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            StreamWriter stream = new StreamWriter(s);
            
            Dictionary<ListNode, int> indexes = new Dictionary<ListNode, int>();
            ListNode currentNode = Head;
            for (int i = 0; i < Count; i++)
            {
                indexes[currentNode] = i;
                currentNode = currentNode.Next;
            }
            
            currentNode = Head;
            stream.WriteLine(Count);
            for (int i = 0; i < Count; i++)
            {
                int currentIndex = currentNode.Rand != null 
                    ? indexes[currentNode.Rand] 
                    : -1;
                stream.WriteLine(currentIndex);
                stream.WriteLine(currentNode.Data);
                currentNode = currentNode.Next;
            }
            
            stream.Close();
        }
        
        public void Deserialize(FileStream s)
        {
            StreamReader stream = new StreamReader(s);

            Count = int.Parse(stream.ReadLine() ?? "0");
            int[] randomIndexes = new int[Count];
            ListNode[] nodes = new ListNode[Count];
            
            ListNode previousNode = null;
            for (int i = 0; i < Count; i++)
            {
                randomIndexes[i] = int.Parse(stream.ReadLine() ?? "-1");
                
                ListNode currentNode = new ListNode();
                nodes[i] = currentNode;
                currentNode.Data = stream.ReadLine() ?? "";

                if (previousNode != null)
                {
                    currentNode.Prev = previousNode;
                    previousNode.Next = currentNode;
                }

                if (i == 0)
                {
                    Head = currentNode;
                }

                if (i == Count - 1)
                {
                    Tail = currentNode;
                }
                
                previousNode = currentNode;
            }

            for (int i = 0; i < Count; i++)
            {
                if (randomIndexes[i] != -1)
                {
                    nodes[i].Rand = nodes[randomIndexes[i]];
                }
            }
            
            stream.Close();
        }
    }
}