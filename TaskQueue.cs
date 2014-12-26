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
            if(!IsTaskDone(task))
                _pendingTaskList.Add(task);
        }

        public void OnPendingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock (e)
            {
                if(e.NewItems != null)
                {
                    var t = _pendingTaskList[0];
                    if (_threadManager.TryGetThread(t))
                        _pendingTaskList.RemoveAt(0);
                }
            }
        }

        public void OnCompletedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock(e)
            {
                if(e.NewItems != null)
                {
                    var t = _pendingTaskList[0];
                    if(_threadManager.TryGetThread(t))
                    {
                        _pendingTaskList.RemoveAt(0);
                    }
                }
            }
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
                if ((task.Name == taskRE.Name) && ( taskRE.Arguments.GetHashCode() == task.Arguments.GetHashCode()))
                {
                    task = taskRE;//We need moar test for this
                    return true;
                }
            }
            return false;
        }
        
    }
}

