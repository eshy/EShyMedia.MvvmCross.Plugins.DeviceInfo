using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Sample.iOS.Views
{
    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
        public override void ViewDidLoad()
        {
            View = new UIView(){ BackgroundColor = UIColor.White};
            base.ViewDidLoad();

            var label = new UILabel(new RectangleF(10, 10, 300, 40));
            Add(label);

            var label2 = new UILabel(new RectangleF(10, 50, 300, 40));
            Add(label2);

            var label3 = new UILabel(new RectangleF(10, 90, 300, 40));
            Add(label3);

            var label4 = new UILabel(new RectangleF(10, 130, 300, 40));
            Add(label4);

            var label5 = new UILabel(new RectangleF(10, 170, 300, 40));
            Add(label5);

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(label).To(vm => vm.DeviceInfo.DeviceName);
            set.Bind(label2).To(vm => vm.DeviceInfo.DeviceType);
            set.Bind(label3).To(vm => vm.DeviceInfo.SoftwareVersion);
            set.Bind(label4).To(vm => vm.DeviceInfo.HardwareVersion);
            set.Bind(label5).To(vm => vm.DeviceInfo.HardwareId);

            set.Apply();
        }
    }
}