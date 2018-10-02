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

