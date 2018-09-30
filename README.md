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

