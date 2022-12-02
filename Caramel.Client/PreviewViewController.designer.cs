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
	[Register ("PreviewViewController")]
	partial class PreviewViewController
	{
		[Outlet]
		ARKit.ARSCNView ARKitView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl PreviewSelector { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ARKitView != null) {
				ARKitView.Dispose ();
				ARKitView = null;
			}

			if (PreviewSelector != null) {
				PreviewSelector.Dispose ();
				PreviewSelector = null;
			}
		}
	}
}
