using System;

namespace QueueTips {
    /// <summary>
    /// A Queue specified for only having a limited number of items in the queue
    /// </summary>
    /// <typeparam name="T">The type of items in the queue</typeparam>
    public class LimitedQueue<T> : Queue<T> where T: IComparable {
        /// <summary>
        /// The max number of items allowed in the queue
        /// </summary>
        public int MaxSize { get; }
        /// <summary>
        /// Creates a new queue with a limited number of items
        /// </summary>
        /// <param name="maxSize">The limit of items allowed in the queue</param>
        public LimitedQueue(int maxSize) : base() {
            MaxSize = maxSize;
        }
        /// <summary>
        /// Enqueues an item into the queue, dequeues if the length becomes larger than the MaxSize
        /// </summary>
        /// <param name="value">The value to enqueue</param>
        public new void Enqueue(T value) {
            base.Enqueue(value);
            if (Length > MaxSize)
                Dequeue();
        }
        /// <summary>
        /// Dequeues the largest item from the queue
        /// </summary>
        /// <returns>The value removed</returns>
        public new T Dequeue() {
            Node current = head;
            T max = current.value;
            int maxIndex = 0;
            int i = 0;
            while(i < Length) {
                if(current.value.CompareTo(max) > 0) {
                    max = current.value;
                    maxIndex = i;
                }
                current = current.next;
                i++;
            }
            return Remove(maxIndex);

        }
    }
}
