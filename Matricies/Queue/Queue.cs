using System;

namespace QueueTips {
    /// <summary>
    /// A Queue data structure
    /// </summary>
    /// <typeparam name="T">The type of data in the Queue</typeparam>
    public class Queue<T> {
        /// <summary>
        /// A node in the Queue
        /// </summary>
        protected class Node {
            /// <summary>
            /// The value the Node holds
            /// </summary>
            public T value;
            /// <summary>
            /// The next Node in the queue
            /// </summary>
            public Node next;
            /// <summary>
            /// Creates a new Node with a given value
            /// </summary>
            /// <param name="value"></param>
            public Node(T value) {
                this.value = value;
            }
            /// <summary>
            /// Calls the ToString of the value
            /// </summary>
            /// <returns>A string representation of the Node</returns>
            public override string ToString() {
                return value.ToString();
            }
        }
        /// <summary>
        /// The head node of the queue
        /// </summary>
        protected Node head;
        /// <summary>
        /// The tail of the queue
        /// </summary>
        protected Node tail;
        private int len;
        /// <summary>
        /// The number of items in the Queue
        /// </summary>
        public int Length => len;
        /// <summary>
        /// The value at the head of the queue
        /// </summary>
        public T Head {
            get {
                if (head != null)
                    return head.value;
                else
                    return default(T);
            }
        }
        /// <summary>
        /// The value at the tail of the queue
        /// </summary>
        public T Tail {
            get {
                if (tail != null)
                    return tail.value;
                else
                    return default(T);
            }
        }
        /// <summary>
        /// Creates a new Queue with no elements
        /// </summary>
        public Queue() {
            len = 0;
        }
        /// <summary>
        /// Enqueues a value into the queue
        /// </summary>
        /// <param name="val">The value to enqueue</param>
        public void Enqueue(T val) {
            Node node = new Node(val);
            if(head == null) {
                head = node;
                tail = head;
            }
            else {
                tail.next = node;
                tail = tail.next;
            }
            len++;
        }
        /// <summary>
        /// Dequeues a value from the queue
        /// </summary>
        /// <returns>The value being dequeued</returns>
        public T Dequeue() {
            if (head == null)
                throw new IndexOutOfRangeException("Queue is empty");
            T val = head.value;
            head = head.next;
            len--;
            return val;
        }
        /// <summary>
        /// Gets a string representation in the format: [x,y,z]
        /// </summary>
        /// <returns>The string representation of the queue</returns>
        public override string ToString() {
            string result = "[";
            Node current = head;
            while(current != null) {
                if (current.next != null) {
                    result += current.value + ", ";
                }
                else
                    result += current.value;
                current = current.next;
            }
            return result + "]";
        }
        /// <summary>
        /// Removes a value from the Queue at a given index
        /// </summary>
        /// <param name="index">The value to remove</param>
        /// <returns>The removed value</returns>
        public T Remove(int index) {
            if (index >= len || index < 0)
                throw new IndexOutOfRangeException(index + " is out of bounds of the Queue("+len+")");
            Queue<T> temp = new Queue<T>();
            T result = default(T);
            int i = 0;
            while(len > 0) {
                T val = Dequeue();
                if (i == index) {
                    result = val;
                }
                else
                    temp.Enqueue(val);
                i++;
            }
            while (temp.Length > 0)
                Enqueue(temp.Dequeue());
            return result;            
        }
        /// <summary>
        /// Checks to see if a value is in the queue
        /// </summary>
        /// <param name="val">The value to look for</param>
        /// <returns>true if the value is in the queue</returns>
        public bool Contains(T val) {
            Node current = head;
            while(current != null) {
                if (current.value.Equals(val))
                    return true;
                current = current.next;
            }
            return false;
        }

    }
}
