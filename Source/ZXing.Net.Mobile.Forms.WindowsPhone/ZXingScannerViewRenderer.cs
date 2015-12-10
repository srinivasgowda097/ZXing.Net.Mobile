﻿using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing.Net.Mobile.Forms.WindowsUniversal;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ZXingScannerView), typeof(ZXingScannerViewRenderer))]
namespace ZXing.Net.Mobile.Forms.WindowsUniversal
{
    //[Preserve(AllMembers = true)]
    public class ZXingScannerViewRenderer : ViewRenderer<ZXingScannerView, ZXing.Mobile.ZXingScannerControl>
    {
        ZXingScannerView formsView;

        ZXing.Mobile.ZXingScannerControl zxingControl;

        protected override void OnElementChanged(ElementChangedEventArgs<ZXingScannerView> e)
        {
            formsView = Element;

            if (zxingControl == null)
            {
                zxingControl = new ZXing.Mobile.ZXingScannerControl();

                formsView.SetInternalHandlers(options => {
                    // Start Scanning
                    var opt = options ?? ZXing.Mobile.MobileBarcodeScanningOptions.Default;
                    zxingControl.StartScanning(formsView.RaiseScanResult, opt);
                }, () => {
                    // Stop Scanning
                    zxingControl.StopScanning();
                }, () => {
                    // Toggle Flash
                    zxingControl.ToggleTorch();
                }, () => {
                    // Get Flash
                    return zxingControl.IsTorchOn;
                }, (on) => {
                    // Set Flash Handler
                    zxingControl.Torch(on);
                }, (x, y) => {
                    // Autofocus
                    zxingControl.AutoFocus();
                });

                base.SetNativeControl(zxingControl);
            }

            base.OnElementChanged(e);
        }
    }
}
