using System;
using System.Collections.Generic;
using System.Text;

namespace Trilogic.Common.Variables
{
    public class VarItem<T>
    {
        #region Class Properties
        protected T _value = default(T);
        #endregion

        #region Constructors and Desctructors
        public VarItem()
        {
        }

        public VarItem(T value)
        {
            _value = value;
        }
        #endregion

        public virtual T GetValue()
        {
            return _value;
        }

        public virtual T SetValue(T value)
        {
            return _value = value;
        }

        public virtual T UnsetValue()
        {
            return _value;
        }

        #region Raw Get/Set/Unset
        public T RawSetValue(T value)
        {
            return _value = value;
        }

        public T RawGetValue()
        {
            return _value;
        }

        public T RawUnsetValue()
        {
            return _value;
        }
        #endregion

        public delegate T VarItemDelegateGet(VarItem<T> varItem);
        public delegate T VarItemDelegateSet(VarItem<T> varItem, T value);
        public delegate T VarItemDelegateUnset(VarItem<T> varItem);
    }


    public class VarItemRandomFromList<T> : VarItem<T>
    {
        Random _rand = new Random(DateTime.Now.Millisecond);
        protected List<T> _list = new List<T>();

        public VarItemRandomFromList()
            : base()
        {
        }

        public List<T> List
        {
            get { return _list; }
        }

        public void Add(T value)
        {
            _list.Add(value);
        }
        public void Add(T val1, T val2)
        {
            _list.Add(val1);
            _list.Add(val2);
        }
        public void Add(T val1, T val2, T val3)
        {
            _list.Add(val1);
            _list.Add(val2);
            _list.Add(val3);
        }
        public void Add(T val1, T val2, T val3, T val4)
        {
            _list.Add(val1);
            _list.Add(val2);
            _list.Add(val3);
            _list.Add(val4);
        }
        public void Add(T val1, T val2, T val3, T val4, T val5)
        {
            _list.Add(val1);
            _list.Add(val2);
            _list.Add(val3);
            _list.Add(val4);
            _list.Add(val5);
        }

        public override T GetValue()
        {
            if (_list.Count == 0)
                return default(T);
            return _list[_rand.Next(_list.Count)];
        }
    }

    public class VarItemRandomInt : VarItem<int>
    {
        Random _rand = new Random(DateTime.Now.Millisecond);
        protected int _minValue = 1;
        protected int _maxValue = 100;

        public VarItemRandomInt()
            : base()
        {
        }
        public VarItemRandomInt(int maxValue)
            : base()
        {
            _maxValue = maxValue;
        }
        public VarItemRandomInt(int minValue, int maxValue)
            : base()
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override int GetValue()
        {
            return _minValue + _rand.Next(1 + (_maxValue - _minValue));
        }
    }

    public class DelegatedVarItem<T> : VarItem<T>
    {
        protected VarItemDelegateGet _getter;
        protected VarItemDelegateSet _setter;
        protected VarItemDelegateUnset _unsetter;

        public DelegatedVarItem()
            : base()
        {
        }

        public DelegatedVarItem(VarItemDelegateSet setter)
            : base()
        {
            _setter = setter;
        }
        public DelegatedVarItem(VarItemDelegateSet setter, VarItemDelegateGet getter)
            : base()
        {
            _setter = setter;
            _getter = getter;
        }
        public DelegatedVarItem(VarItemDelegateSet setter, VarItemDelegateGet getter, VarItemDelegateUnset unsetter)
            : base()
        {
            _setter = setter;
            _getter = getter;
            _unsetter = unsetter;
        }
        public DelegatedVarItem(VarItemDelegateGet getter)
            : base()
        {
            _getter = getter;
        }
        public DelegatedVarItem(VarItemDelegateUnset unsetter)
            : base()
        {
            _unsetter = unsetter;
        }

        public override T GetValue()
        {
            if (_getter != null)
                return _getter(this);
            return base.GetValue();
        }

        public override T SetValue(T value)
        {
            if (_setter != null)
                return _setter(this, value);
            return base.SetValue(value);
        }

        public override T UnsetValue()
        {
            if (_unsetter != null)
                return _unsetter(this);
            return base.UnsetValue();
        }
    }
}
