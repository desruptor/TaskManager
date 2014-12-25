using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mephi.Cybernetics.Nm.TaskManager
{
    class TaskRE
    {
        private object[] _arguments;
        private ResultValue _resultValue;

        public ResultValue ResultValue
        {
            get { return _resultValue; }
            set { _resultValue = value; }
        }


        public FuncInfo Func { get; private set; }

        public string Name { get; private set; }

        public object[] Arguments
        {
            get
            {
                return _arguments;
            }
        }

        #region Ctor

        public TaskRE(string name, object[] argumnets, Delegate d)
        {
            Name = name;
            _arguments = argumnets;
            Func = new FuncInfo(d);
            for (int i = 0; i < _arguments.Length; i++)
            {
                if (Func.ArgumentsType[i] != _arguments[i].GetType()) throw new ArgumentException("delegate argument");
            }

            for (int i = 0; i < Func.Arity; i++)
            {
                if (Func.ArgumentsType[i] != _arguments[i].GetType()) throw new ArgumentException("delegate argument");
            }
            ResultValue = new ResultValue() ;
        }

        #endregion

    }
}
