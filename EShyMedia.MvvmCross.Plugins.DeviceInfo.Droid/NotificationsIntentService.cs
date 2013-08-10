using Android.App;
using Android.Content;
using Android.OS;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Droid
{
    [Service]
    public class NotificationsIntentService : IntentService
    {
        static PowerManager.WakeLock sWakeLock;
        static object LOCK = new object();

        internal static void RunIntentInService(Context context, Intent intent)
        {
            lock (LOCK)
            {
                if (sWakeLock == null)
                {
                    // This is called from BroadcastReceiver, there is no init.
                    var pm = PowerManager.FromContext(context);
                    sWakeLock = pm.NewWakeLock(
                        WakeLockFlags.Partial, "My WakeLock Tag");
                }
            }

            sWakeLock.Acquire();
            intent.SetClass(context, typeof(NotificationsIntentService));
            context.StartService(intent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                var context = this.ApplicationContext;
                var action = intent.Action;

                if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
                {
                    var registrationId = intent.GetStringExtra("registration_id");

                    if (null != OnRegistrationCompleted)
                    {
                        OnRegistrationCompleted(this, new RegistrationEventArgs(registrationId));
                    }
                    //HandleRegistration(context, intent);
                }
                else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
                {
                    //HandleMessage(context, intent);
                }
            }
            finally
            {
                lock (LOCK)
                {
                    //Sanity check for null as this is a public method
                    if (sWakeLock != null)
                        sWakeLock.Release();
                }
            }
        }
    }
}