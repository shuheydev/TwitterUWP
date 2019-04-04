using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TwitterUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            WebView_Main.Navigate(new Uri("https://twitter.com"));

            SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
            {
                if (WebView_Main.CanGoBack)
                {
                    WebView_Main.GoBack();
                    args.Handled = true;
                }
            };
        }

        private async void WebView_Main_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                await WebView_Main.InvokeScriptAsync("eval", new string[] { SetScrollbarScript });
            }
            catch (Exception e)
            {
                WebView_Main.NavigateToString("Offline.");
            }
        }


        string SetScrollbarScript = @"
            function setScrollbar()
            {
                document.body.style.msOverflowStyle='scrollbar';   
            } 
            setScrollbar();";

        private void WebView_Main_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
        }
    }
}

