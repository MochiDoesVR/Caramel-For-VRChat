// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using SharpOSC;

namespace Caramel.Client
{
	public partial class SettingsViewController : UITableViewController
	{
		public SettingsViewController (IntPtr handle) : base (handle)
		{
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AddressField.EditingDidEnd += UpdateSenderAddress;
            PortField.EditingDidEnd += UpdateSenderAddress;

            Xamarin.IQKeyboardManager.SharedManager.Enable = true;
            Xamarin.IQKeyboardManager.SharedManager.ShouldToolbarUsesTextFieldTintColor = true;
        }

        private void UpdateSenderAddress(object sender, EventArgs e)
        {
            if (int.TryParse(PortField.Text, out var port) && System.Net.IPAddress.TryParse(AddressField.Text, out var addr))
            {
                ARKitSkeletonDelegate.client = new SharpOSC.UDPSender(addr.ToString(), port);
                try
                {
                    ARKitSkeletonDelegate.client.Send(new OscMessage("/crml/net/testoscpermissions"));
                }
                catch (Exception)
                {

                }
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
