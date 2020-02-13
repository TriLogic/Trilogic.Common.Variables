# Trilogic.Common.Variables
A library for storing and retrieving runtime variables.

Variables are managed using a VarStack<VarSet<string,VarItem<T>>> structure. The lowest level of the stack is assumed to be the global scope whereas the top of stack is assumed to be the local scope.  Get/Set/Exist operations can be limited to local or global scope or allowed to traverse the entire stack.

Values within the stack can be initialized with get/set/delete delegates to mimic readonly values, auto incrementing values or to implement any type of variable behavior desired.

The original library dates back back quite a few years with the original source being written in VB5.
