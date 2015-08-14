//using System;
//using System.Collections.Concurrent;
//using System.Threading;
//using System.Threading.Tasks;

//namespace kasthack.Performance.Parallel
//{
//    public static class ParallelAsync
//    {
//        public static async Task For(long start, long end, Func<long,Task> action, ParallelAsyncOptions options = null)
//        {
//            options = options ?? new ParallelAsyncOptions();
//            var ct = new ParallelForExecutionContext(start, end, action, options);
//            await ct.Run().ConfigureAwait(false);
//        }

//    }

//    internal class ParallelForExecutionContext
//    {
//        private readonly long _start;
//        private readonly long _end;
//        private readonly Func<long, Task> _action;
//        private readonly ParallelAsyncOptions _options;
//        private SemaphoreSlim _limiter;
//        private ManualResetEventSlim _joiner;
//        private ConcurrentDictionary<int, Task> _awaiter;

//        public ParallelForExecutionContext(long start, long end, Func<long, Task> action, ParallelAsyncOptions options)
//        {
//            _start = start;
//            _end = end;
//            _action = action;
//            _options = options;
//            _joiner = new ManualResetEventSlim(false);
//            _awaiter = new ConcurrentDictionary<int, Task>();
//            if (options.MaxDegreeOfParallelism > 0)
//                _limiter = new SemaphoreSlim(options.MaxDegreeOfParallelism);
//        }

//        public async Task Run()
//        {
//            for (var i = _start; i < _end; i++)
//            {
//                _options.CancellationToken.ThrowIfCancellationRequested();
//                if (_limiter != null) await _limiter.WaitAsync().ConfigureAwait(false);
//                Wrap(i);
//            }
            
//        }

//        private async void Wrap(long parameter)
//        {
//            try
//            {
//                await _action(parameter).ConfigureAwait(false);
//            }
//            catch (Exception ex)
//            {
//                //...
//            }
//            finally
//            {
//                if (_limiter != null) _limiter.Release();
//            }
//        }
//    }

//    public class ParallelAsyncOptions : ParallelOptions
//    {
//        public int RequestRate { get; set; } = 0;
//        public ParallelAsyncOptions() : base()
//        {
//        }
//    }
//}
