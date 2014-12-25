using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Mephi.Cybernetics.Nm.TaskManager
{
    class ThreadManager
    {
        private ObservableCollection<TaskRE> _currentTaskList;

        private ObservableCollection<ThreadForTM> _busyThreads;
        private ObservableCollection<ThreadForTM> _freeThreads;

        const int quantityOfThreads = 5;

        #region Ctor

        public ThreadManager()
        {
            _currentTaskList = new ObservableCollection<TaskRE>();
            _busyThreads = new ObservableCollection<ThreadForTM>();
            _freeThreads = new ObservableCollection<ThreadForTM>();   

            for (int i = 0; i < quantityOfThreads; ++i)
            {
                _freeThreads.Add(new ThreadForTM());
            }
            
        }

        #endregion

        public bool TryGetThread( TaskRE task )
        {
            lock (_freeThreads)
            {
                if (_freeThreads.Count != 0)
                {
                    ThreadForTM freeThread = _freeThreads[0];
                    _currentTaskList.Add(task);
                    _freeThreads.RemoveAt(0);
                    _busyThreads.Add(freeThread);

                    freeThread.Invoke(task);
                    return true;
                }
            }
            return false;
        }

        private void OnStateThreadForTMCHanged(object sender, ThreadForTM e)
        {
            if (e.State == State.Done)
            {
                _currentTaskList.Remove(e.Task);
                
                //вернуть eTask в TQ в completed
                _busyThreads.Remove(e);
                _freeThreads.Add(e);
                
            }
        }

    }
}
 