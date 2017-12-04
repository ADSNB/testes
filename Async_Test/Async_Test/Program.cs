using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.IO.File;

namespace Async_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EventLog.WriteEntry("Teste", "Boring");
            EventLog.WriteEntry("Teste", "Boring", EventLogEntryType.Information);
            //Run();
            Console.ReadLine();
        }
        static void Run()
        {
            var task = ComputeFileLengthAsync(null);
            Console.WriteLine("Computing file length");
            Console.WriteLine(task.Result);
            Console.WriteLine("done!");
        }


        #region solução 1
        static Task<int> ComputeFileLengthAsync(string fileName)
        {
            Console.WriteLine("Before If");
            if (fileName == null)
            {
                Console.WriteLine("Inside");
                //throw new ArgumentNullException("fileName");
            }
            Console.WriteLine("After");

            return new Task<int>(() =>
            {
                using (var fileStream2 = OpenText(fileName))
                {
                    var content2 = fileStream2.ReadToEndAsync().Result;
                    return content2.Length;
                }
            });
        }
        #endregion

        #region solução 2
        /*
        public static Task<int> ComputeFileLengthAsync(string fileName)
        {
            Console.WriteLine("Before If");
            if (fileName == null)
            {
                Console.WriteLine("Inside");
                throw new ArgumentNullException("fileName");
            }
            Console.WriteLine("After");

            return ComputeFileLengthAsyncImpl(fileName);
        }

        private static async Task<int> ComputeFileLengthAsyncImpl(string fileName)
        {
            using (var fileStream2 = OpenText(fileName))
            {
                var content2 = await fileStream2.ReadToEndAsync();
                return content2.Length;
            }
        }*/
        #endregion
    }

    [StructLayout(LayoutKind.Auto)]
    public struct ComputeFileLengthAsync_StateMachine : IAsyncStateMachine
    {
        public int _state;
        public AsyncTaskMethodBuilder<int> _builder;
        public string fileName;
        private StreamReader _fileStream;
        private TaskAwaiter<string> _awaiter;

        public void MoveNext()
        {
            int num = _state;
            int length;
            try
            {
                if (num != 0)
                {
                    Console.WriteLine("Before If");
                    if (fileName == null)
                    {
                        Console.WriteLine("Inside");
                        throw new ArgumentNullException("fileName");
                    }
                    Console.WriteLine("After");
                    _fileStream = File.OpenText(fileName);
                }
                try
                {
                    TaskAwaiter<string> taskAwaiter;
                    if (num != 0)
                    {
                        taskAwaiter = _fileStream.ReadToEndAsync().GetAwaiter();
                        if (!taskAwaiter.IsCompleted)
                        {
                            num = (_state = 0);
                            _awaiter = taskAwaiter;
                            _builder.AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        taskAwaiter = _awaiter;
                        _awaiter = default(TaskAwaiter<string>);
                        num = (_state = -1);
                    }
                    string result = taskAwaiter.GetResult();
                    length = result.Length;
                }
                finally
                {
                    if (num > 0)
                    {
                        _fileStream?.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                _state = -2;
                _builder.SetException(exception);
                return;
            }
            _state = -2;
            _builder.SetResult(length);
        }

        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            _builder.SetStateMachine(stateMachine);
        }
    }
}