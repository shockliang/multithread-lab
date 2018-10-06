# ManualRestEvent Sample #

### Execution result ###
* `mre` was ManualResetEvent variable.

Start 3 named threads that block on a ManualResetEvent:

Thread_0 starts and calls mre.WaitOne()             => Block by `mre`
Thread_2 starts and calls mre.WaitOne()             => Block by `mre`
Thread_1 starts and calls mre.WaitOne()             => Block by `mre`

When all three threads have started, press Enter to call Set()
to release all the threads.

Thread_0 ends.                                      => Release from `mre` at the same time after `mre.Reset();`
Thread_1 ends.                                      => Release from `mre` at the same time after `mre.Reset();`
Thread_2 ends.                                      => Release from `mre` at the same time after `mre.Reset();`

When a ManualResetEvent is signaled, threads that call WaitOne()
do not block. Press Enter to show this.

Thread_3 starts and calls mre.WaitOne()             => Block by `mre`
Thread_3 ends.                                      => But do block cause not call the `mre.Reset()`
Thread_4 starts and calls mre.WaitOne()             => Block by `mre`
Thread_4 ends.                                      => But do block cause not call the `mre.Reset()`

Press Enter to call Reset(), so that threads once again block
when they call WaitOne().

Thread_5 starts and calls mre.WaitOne()             => Block by `mre` befeore calling the `mre.Reset()`.

Press Enter to call Set() and conclude the demo.

Thread_5 ends.                                      => Release by `mre` after calling the `mre.Set();`