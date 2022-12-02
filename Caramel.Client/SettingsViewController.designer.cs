// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Caramel.Client
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		UIKit.UITextField AddressField { get; set; }

		[Outlet]
		UIKit.UITextField PortField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddressField != null) {
				AddressField.Dispose ();
				AddressField = null;
			}

			if (PortField != null) {
				PortField.Dispose ();
				PortField = null;
			}
		}
	}
}
