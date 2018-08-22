using System;
using System.Collections.Generic;

namespace Trilogic.Common.Variables
{
	/// <summary>
	/// Class to hold a single layer of variables.
	/// </summary>
	public class VarSet<T>
    {
        #region Class Members
        private Dictionary<string, VarItem<T>> mDict;
        private VarStackKeyGenerator mKeyGen;
        #endregion

        #region Constructors and Destructors
        public VarSet()
		{
            this.mDict = new Dictionary<string, VarItem<T>>();
            this.KeyGen = null;
		}
        public VarSet(VarStackKeyGenerator keyGen)
        {
            this.mDict = new Dictionary<string, VarItem<T>>();
            this.KeyGen = keyGen;
        }
        #endregion

        #region Class Properties
        public VarStackKeyGenerator KeyGen
        {
            get { return mKeyGen; }
            set
            {
                mKeyGen = value;
                if (mKeyGen == null)
                    mKeyGen = new VarStackKeyGenerator(VarStack<T>.CreateKeyIgnoreCase);
            }
        }
        #endregion

        #region Variable Get,Set,Exists,Clear
        public bool HasVar(string varName)
		{
            return mDict.ContainsKey(mKeyGen(varName));
		}

        public virtual bool SetVar(string varName, VarItem<T> varValue)
        {
            string key = mKeyGen(varName);
            if (mDict.ContainsKey(key))
            {
                mDict.Remove(key);
                mDict.Add(key, varValue);
                return true;
            }
            mDict.Add(key, varValue);
            return false;
        }

		public virtual bool GetVar(string varName, ref VarItem<T> result)
		{
            string key = mKeyGen(varName);
            if (mDict.ContainsKey(key))
            {
                result = mDict[key];
                return true;
            }
            return false;
		}
        public virtual bool UnsetVar(string varName)
        {
            VarItem<T> result = null;
            return UnsetVar(varName, ref result);
        }

        public virtual bool UnsetVar(string varName, ref VarItem<T> item)
		{
            string key = mKeyGen(varName);
            if (mDict.ContainsKey(key))
            {
                item = mDict[key];
                mDict.Remove(key);
                return true;
            }
            item = null;
            return false;
		}

        public virtual void Clear()
        {
            mDict.Clear();
        }
        #endregion
    }
}
