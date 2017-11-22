using System;
using System.Collections.Generic;
using System.Threading;

namespace Queue
{
    public class ThreadingQueue<T> : IDisposable
    {
        private Queue<T> queue = new Queue<T>();
        private Semaphore pushed = new Semaphore(0, int.MaxValue);

        public void Push(T item)
        {
            lock (queue)
            {
                queue.Enqueue(item);
            }

            pushed.Release();
        }

        public T Pop()
        {
            pushed.WaitOne();

            lock (queue)
            {
                return queue.Dequeue();
            }
        }

        public virtual void Dispose()
        {
            if (pushed != null)
            {
                pushed.Dispose();
                pushed = null;

                GC.SuppressFinalize(this);
            }
        }

        ~ThreadingQueue()
        {
            Dispose();
        }
    }
}