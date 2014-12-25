using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.ObjectModel;

namespace Mephi.Cybernetics.Nm.TaskManager
{
    class TaskQueue
    {
        private ObservableCollection<TaskRE> _pendingTaskList;
        private ObservableCollection<TaskRE> _completedTaskList;
        
        private ThreadManager _threadManager;
        
        public TaskRE GetTask()
        {
            var popedTask = _pendingTaskList[0];
            _pendingTaskList.RemoveAt(0);
            return popedTask;
        }

        public void AddTask(TaskRE task)
        {
            _pendingTaskList.Add(task);
        }

        public void OnPendingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock (e)
            {
                if(e.NewItems != null)
                {
                    //Проверить если уже есть такая таска с такими параметрами то вернуть ответ сразу
                    var k = _pendingTaskList[0];
                    if (_threadManager.TryGetThread(k))
                        _pendingTaskList.RemoveAt(0);
                }


            }
        }

        public void OnCompletedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //
        }

        public TaskQueue()
        {
            _pendingTaskList = new ObservableCollection<TaskRE>();
            _completedTaskList = new ObservableCollection<TaskRE>();
            _threadManager = new ThreadManager();

            _pendingTaskList.CollectionChanged += this.OnPendingCollectionChanged;
            _completedTaskList.CollectionChanged += this.OnCompletedCollectionChanged;
        }

        public bool IsTaskDone(TaskRE task)
        {
            foreach (var taskRE in _completedTaskList)
            {
                if ( taskRE.Arguments.GetHashCode() == task.Arguments.GetHashCode())
                {
                    Console.WriteLine("kjyhjjyj");
                }
            }
            return false;
        }
        
    }
}

