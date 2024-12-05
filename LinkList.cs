using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dosomething
{
    internal class LinkList
    {
        private Node head;
        public LinkList()
        {
            head = null;
        } 

       public void Add(int element)
        {   
          Node newNode=new Node(element);
            if(head ==null)
                head = newNode;
            else
            {
                Node current = head;

                while (current.Next != null)
                    current = current.Next;

                current.Next = newNode;
            }
        }
        public void Remove()
        {
            if (head.Next == null)
                head = null;
            else
            {
                Node currentNode = head;
                while (currentNode.Next.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = null;
            }
        }
        public void insert(int element)
        {
        }

    }
}
