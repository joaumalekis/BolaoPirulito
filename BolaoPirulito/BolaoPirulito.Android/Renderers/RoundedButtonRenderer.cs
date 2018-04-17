﻿using System;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using BolaoPirulito;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(BolaoPirulito.Controls.RoundedButton), typeof(BolaoPirulito.Droid.Renderers.RoundedButtonRenderer))]
namespace BolaoPirulito.Droid.Renderers
{
    public class RoundedButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        private GradientDrawable _normal, _pressed;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = (Controls.RoundedButton)e.NewElement;

                button.SizeChanged += (s, args) =>
                {
                    var radius = (float)Math.Min(button.Width, button.Height);

                    // Create a drawable for the button's normal state
                    _normal = new GradientDrawable();

                    if (button.BackgroundColor.R == -1.0 && button.BackgroundColor.G == -1.0 && button.BackgroundColor.B == -1.0)
                        _normal.SetColor(Android.Graphics.Color.ParseColor("#ff2c2e2f"));
                    else
                        _normal.SetColor(button.BackgroundColor.ToAndroid());

                    _normal.SetCornerRadius(radius);

                    // Create a drawable for the button's pressed state
                    _pressed = new GradientDrawable();
                    var highlight = Context.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ColorActivatedHighlight }).GetColor(0, Android.Graphics.Color.Gray);
                    _pressed.SetColor(highlight);
                    _pressed.SetCornerRadius(radius);

                    // Add the drawables to a state list and assign the state list to the button
                    var sld = new StateListDrawable();
                    sld.AddState(new int[] { Android.Resource.Attribute.StatePressed }, _pressed);
                    sld.AddState(new int[] { }, _normal);
                    Control.SetBackgroundDrawable(sld);
                };
            }
        }
    }
}