using System;
using System.Collections.Generic;

namespace Trilogic.Common.Variables
{
    public delegate string VarStackKeyGenerator(string varKey);

    public class VarStack<T>
    {
        #region Class Members
        private List<VarSet<T>> mStack;
        private VarSet<T> mGlobals;
        private VarStackKeyGenerator mKeyGen;
        #endregion

        #region Constructors and Destructors
        public VarStack()
		{
			mStack = new List<VarSet<T>>();
            Push();
		}

        public VarStack(VarSet<T> varSet)
        {
            mStack = new List<VarSet<T>>();
            Push(varSet);
        }
        #endregion

        public VarStackKeyGenerator KeyGen
        {
            get { return mKeyGen; }
            set
            {
                mKeyGen = value;
                if (mKeyGen == null)
                {
                    mKeyGen = new VarStackKeyGenerator(VarStack<T>.CreateKeyIgnoreCase);
                }
            }
        }

        #region Stack Manipulation
        public VarSet<T> Peek()
		{
			return mStack[mStack.Count-1];
		}

		public VarSet<T> Pop()
		{
            VarSet<T> result = mStack[mStack.Count - 1];
            mStack.RemoveAt(mStack.Count - 1);
            if (mStack.Count == 0)
                mGlobals = null;
            return result;
		}

		public VarSet<T> Push()
		{
            return Push(new VarSet<T>(mKeyGen));
		}

		public VarSet<T> Push(VarSet<T> varSet)
		{
            mStack.Add(varSet);
            if (mStack.Count == 1)
                mGlobals = varSet;
            return varSet;
        }

		public VarSet<T> GetVarSet(string varName)
		{
            for (int i = mStack.Count - 1; i >= 0; i--)
                if (mStack[i].HasVar(varName))
                    return mStack[i];
            return null;
		}

        public void Clear()
        {
            while (mStack.Count > 0)
            {
                Peek().Clear();
                Pop();
            }
        }
        #endregion

        #region Get,Set,Unset,Exist (Stack)
        public VarItem<T> this[string varName]
        {
            get
            {
                VarItem<T> item = null;
                if (GetVar(varName, ref item))
                    return item;
                return null;
            }
        }


        public bool GetVar(string varName, ref VarItem<T> result)
        {
            VarSet<T> vs = GetVarSet(varName);
            if (vs == null)
                return false;
            return vs.GetVar(varName, ref result);
        }

        public bool SetVar(string varName, VarItem<T> varValue)
		{
            if (mStack.Count < 1) Push();
            VarSet<T> vs = GetVarSet(varName);
            if (vs == null)
                vs = mStack[mStack.Count - 1];
            return vs.SetVar(varName, varValue);
		}
        public bool SetVar(string varName, T varValue)
        {
            return SetVar(varName, new VarItem<T>(varValue));
        }


        public bool UnsetVar(string varName)
		{
            VarSet<T> vs = GetVarSet(varName);
            if (vs == null)
                return true;
            vs.UnsetVar(varName);
            return true;
		}
		
		public bool Exist(string varName)
		{
            return GetVarSet(varName) != null;
		}
        #endregion

        #region Get,Set,Unset,Exist (Global)
        public bool GetGlobal(string varName, ref VarItem<T> result)
        {
            if (mStack.Count == 0)
                Push();
            return mStack[0].GetVar(varName, ref result);
        }

        public bool SetGlobal(string varName, VarItem<T> varValue)
        {
            if (mStack.Count == 0)
                Push();
            return mStack[0].SetVar(varName, varValue);
        }
        public bool SetGlobal(string varName, T varValue)
        {
            return SetGlobal(varName, new VarItem<T>(varValue));
        }

        public bool UnsetGlobal(string varName, ref VarItem<T> result)
        {
            if (mStack.Count > 0)
                return mStack[0].UnsetVar(varName, ref result);
            return false;
        }
        public bool UnsetGlobal(string varName)
        {
            if (mStack.Count > 0)
            {
                VarItem<T> result = null;
                return mStack[0].UnsetVar(varName, ref result);
            }
            return false;
		}

		public bool ExistGlobal(string varName)
		{
            if (mStack.Count > 0)
                return mStack[0].HasVar(varName);
            return false;
		}
        #endregion

        #region Get,Set,Unset,Exist (Local)
        public bool GetLocal(string varName, ref VarItem<T> result)
        {
            if (mStack.Count == 0)
                Push();
            VarSet<T> vs = mStack[mStack.Count - 1];            
            return vs.GetVar(varName, ref result);
        }

        public bool SetLocal(string varName, VarItem<T> varValue)
        {
            if (mStack.Count == 0) Push();
            return mStack[mStack.Count - 1].SetVar(varName, varValue);
        }
        public bool SetLocal(string varName, T varValue)
        {
            return SetLocal(varName, new VarItem<T>(varValue));
        }

        public bool UnsetLocal(string varName, ref VarItem<T> result)
        {
            if (mStack.Count > 0)
                return mStack[mStack.Count - 1].UnsetVar(varName, ref result);
            return false;
        }
        public bool UnsetLocal(string varName)
        {
            if (mStack.Count > 0)
            {
                VarItem<T> result = null;
                return mStack[mStack.Count - 1].UnsetVar(varName, ref result);
            }
            return false;
        }

        public bool ExistLocal(string varName)
        {
            if (mStack.Count > 0)
                return mStack[mStack.Count-1].HasVar(varName);
            return false;
        }
        #endregion

        #region Key Generators
        public static string CreateKeyIgnoreCase(string key)
        {
            return key.ToLower();
        }
        public static string CreateKeyRespectCase(string key)
        {
            return key.ToLower();
        }
        #endregion
	}
}
