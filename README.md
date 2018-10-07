# multithread-lab

## First Demo ##


## Cancelling Tasks ## 
* The `CancellationTokenSource.CreateLinkedTokenSource` can linked multiple `CancellationTokenSource` to stop task whatever linked token be fired.

## Waiting For Time To Pass ##


## Waiting For Task ##
* `Task.WaitAll(t1, t2)` waiting for the longest task. In the sample will be waiting the `t1` task completed
* `Task.WaitAny(t1, t2)` waiting for the first completed task. In the sample will be waiting the `t2` task completed
* `Task.WaitAny(new[] { t1, t2 }, 4000)` only waiting for 4 seconds. In the sample will be waiting the `t2` task completed

## Exception Handling ##


## Critical Sections ##
* `Interlocked.Add` to add new value to origin one.
* `Interlocked.Increment` to increment one to origin value.
* `Interlocked.Decrement` to decrement one to origin value.
* `Interlocked.MemoryBarrier` shorthand for `Thread.MemoryBarrier()`.
    * Synchronizes memory access as follows.
* `Interlocked.Exchange` sets a variable to a specified value as an atomic operation.
* `SpinLock` object for atomic operation that until you are able to execute.
    * `SpinLock(true)` enable thread owner tracking.
* Mutex object
    * Can shared between server different processes.
* Reader-Writer locks
    * Support lock recursion in ctor paramater but not recommended.


## Concurrent Collections ##
* `ConcurrentDictionary` asking to count that's an expensive operation.
* `ConcurrentQueue`
* `ConcurrentStack` provides the `TryPopRange` to pop items at once.
* `ConcurrentBag` provides no ordering guarantees.
* `BlockingCollection` is a wrapper around one of the `IProducerConsumerCollection` classes.
    * Provides blocking and bounding capabilites.

## Task Coordination ##
* Contiuation
    * `ContinueWith` api provides to continue next task.
        * Continuations can be conditional.
            * TaskContinuationOptions.NotOnFaulted.
        * Beware of waiting on continuations that might not occur by faulted. That will be waiting forever.
    * `Task.Factory.ContinueWhenAll` provides api to waiting for all tasks.
        * One-to-many continuations.
    * `Task.Factory.ContinueWhenAny` provides api to waiting any first completed task.
        * One-to-any continuations.
        
* Child task
    * `TaskCreationOptions.AttachedToParent` for attach to parent.
    * `TaskContinuationOptions.OnlyOnRanToCompletion` continue when task ran to completion.
    * `TaskContinuationOptions.OnlyOnFaulted` continue when task ran to failted.

* Synchronization Primitives
    * All do same thing. 
        * They have a counter.
        * Let you execute N threads at a time.
        * Other threads are unblocked until state changes.

    * `Barrier`
        * Blocks all threads until N are waiting , then lets those N through via `SignalAndWait()`.
    * `CountdownEvent`
        * Signaling and waiting separate; waits until signal level reaches 0, then unblocks.
    * `ManualResetEventSlim`
        * Like `CountdownEvent` with a count of 1.
        * `Set()` to release all block threads. After `Set()` signaled, threads that call `WaitOne()` do not block.
        * `Reset` starting block threads. 
    * `AutoResetEvent`
        * Resets after waiting.
    * `SemaphoreSlim`
        * Counter `CurrentCount` decreased by `Wait()` and increased by `Release(N)`.
        * Can have a maximum.

## Parallel Loops ## 