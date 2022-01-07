using System;
using System.IO;

namespace S_T_serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test purpose
            int N = 1000000;
            Random random = new Random();
            
            ListRand randomList = new ListRand();
            ListNode[] nodes = new ListNode[N];

            for (int i = 0; i < N; i++)
            {
                nodes[i] = new ListNode();
                nodes[i].Data = i.ToString();
                
                if (i == 0)
                    continue;
                
                nodes[i - 1].Next = nodes[i];
                nodes[i].Prev = nodes[i - 1];
            }
            
            randomList.Head = nodes[0];
            randomList.Tail = nodes[N-1];
            randomList.Count = N;
            
            for (int i = 0; i < N; i++)
            {
                if (random.Next(9) > 0)
                    nodes[i].Rand = nodes[random.Next(N)];
            }
            
            var startTime = DateTime.Now;
            using (var a = new FileStream("output.txt", FileMode.OpenOrCreate))
            {
                randomList.Serialize(a);
            };
            
            ListRand randomListMonstrosity = new ListRand();
            using (var b = new FileStream("output.txt", FileMode.Open))
            {
                randomListMonstrosity.Deserialize(b);
            }
            var finishTime = DateTime.Now;

            Console.WriteLine((finishTime-startTime).TotalMilliseconds);
        }
    }
}