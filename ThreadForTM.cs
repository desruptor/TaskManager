using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mephi.Cybernetics.Nm.TaskManager
{
    enum State
    {
        Free,
        InProcess,
        Done
    }
    class ThreadForTM
    {
        private static int counter = 0;
        private Thread _thread;
        public TaskRE Task { get; private set; }
        public int ID { get; private set; }
        public State State { get; private set; }
        public ThreadForTM()
        {
            ID = counter;
            ++counter;
        }

        public void Invoke(TaskRE task)
        {
            Task = task;
            _thread = new Thread(() =>
            {
                this.State = State.InProcess;
                Task.ResultValue.Value = Task.Func.FuncDelegate.DynamicInvoke(Task.Arguments);
                this.State = State.Done;
            });
            _thread.Start();
            
        }
    }
}
