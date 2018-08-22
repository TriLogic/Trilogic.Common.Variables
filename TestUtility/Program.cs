using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trilogic.Common.Variables;
using Trilogic.Common.Substitution;

namespace TestUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            VarSet<string> glb = new VarSet<string>();
            VarStack<string> stk = new VarStack<string>(glb);
            TagReplacer se = new TagReplacer("This is a ${desc} test", 
                delegate(string key, out string value) {
                if (stk.Exist(key)) {
                    value = stk[key].GetValue();
                    return true;
                } 
                    value = String.Empty;
                    return false;
                }
                );

            VarItemRandomFromList<string> vrfl = new VarItemRandomFromList<string>();
            vrfl.List.Add("big");
            vrfl.List.Add("large");
            vrfl.List.Add("giant");
            vrfl.List.Add("ginormous");
            vrfl.List.Add("huge");
            vrfl.List.Add("humongous");
            stk.SetGlobal("desc", vrfl);

            for (int i = 0; i < 100; i++)
            {
                string msg = se.Replace();
                Console.WriteLine("The Message is: {0}", msg);
            }

            Console.ReadKey();
        }
    }
}
