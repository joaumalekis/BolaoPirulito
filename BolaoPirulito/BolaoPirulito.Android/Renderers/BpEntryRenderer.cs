using Android.Content;
using Android.Views;
using BolaoPirulito.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(BpEntryRenderer))]

namespace BolaoPirulito.Droid.Renderers
{
    public class BpEntryRenderer : EntryRenderer
    {
        public BpEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            Control.Gravity = GravityFlags.CenterVertical;
            Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, Android.Graphics.PorterDuff.Mode.SrcIn);
        }
    }
}