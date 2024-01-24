using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Queue
{
    public sealed class DefaultBackgroundTaskQueue<T> : IBackgroundTaskQueue<T> where T : class
    {
        private readonly ConcurrentQueue<T> _items = new ConcurrentQueue<T>();

    public void Enqueue(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _items.Enqueue(item);
    }

    public T? Dequeue()
    {
        //testing
        var success = _items.TryDequeue(out var workItem);

        return success
            ? workItem
            : null;
    }
    }
}