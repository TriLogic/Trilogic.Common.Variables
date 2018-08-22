using System;

namespace OmniTasks.Core
{
	/// <summary>
	/// Summary description for OTSecureVariableSet.
	/// </summary>
	class OTSecureVariableSet : OTVariableSet
	{
		public OTSecureVariableSet() : base()
		{
			// nothing required
		}

		public void SecureSetVar( string Name, string Value )
		{
			base.SetVar( Name, Value );
		}

		public void SecureUnSetVar( string Name )
		{
			base.UnSetVar( Name );
		}

		public override void SetVar( string Name, string Value )
		{
			// do nothing
		}
		public override string GetVar( string Name )
		{
			return base.GetVar( Name );
		}
		public override void UnSetVar( string Name )
		{
			// do nothing
		}
	}
}
