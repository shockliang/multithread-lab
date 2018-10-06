# AutoResetEvent sample #

### Execution Result ###
* `event_1` auth reset default was `true`
* `event_2` auth reset default was `false`

Thread_2 waits on AutoResetEvent #1.            => Entry the ThreadProc method and block by `event_1`
Thread_3 waits on AutoResetEvent #1.            => Entry the ThreadProc method and block by `event_1`
Thread_1 waits on AutoResetEvent #1.            => Entry the ThreadProc method and block by `event_1`
Thread_2 is released from AutoResetEvent #1.    => Release from `event_1`
Thread_2 waits on AutoResetEvent #2.            => Block by `event_2`
Press Enter to release another thread.          => Block by main thread by console readline.

Thread_3 is released from AutoResetEvent #1.    => Release from `event_1`
Thread_3 waits on AutoResetEvent #2.            => Block by `event_2`
Press Enter to release another thread.          => Block by main thread by console readline.

Thread_1 is released from AutoResetEvent #1.    => Release from `event_1`
Thread_1 waits on AutoResetEvent #2.            => Block by `event_2`

All threads are now waiting on AutoResetEvent #2.   => All threads release from `event_1`.
Press Enter to release a thread.                => Block by main thread by console readline.

Thread_2 is released from AutoResetEvent #2.    => Release from `event_2`
Thread_2 ends.                                  => `Thread_2` Execution finish.
Press Enter to release a thread.                => Block by main thread by console readline.   

Thread_3 is released from AutoResetEvent #2.    => Release from `event_2`
Thread_3 ends.                                  => `Thread_3` Execution finish.
Press Enter to release a thread.                => Block by main thread by console readline.   

Thread_1 is released from AutoResetEvent #2.    => Release from `event_2`
Thread_1 ends.                                  => `Thread_1` Execution finish.