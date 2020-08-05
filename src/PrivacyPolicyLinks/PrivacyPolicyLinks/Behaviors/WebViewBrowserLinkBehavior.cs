using Behaviors;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrivacyPolicyLinks.Behaviors
{
	public class WebViewBrowserLinkBehavior : BehaviorBase<WebView>
	{
		public BrowserLaunchOptions LaunchOptions { get; set; } = new BrowserLaunchOptions
		{
			LaunchMode = BrowserLaunchMode.SystemPreferred,
			TitleMode = BrowserTitleMode.Default,
			PreferredControlColor = Color.FromHex("#f3f3f3"),
			PreferredToolbarColor = Color.FromHex("#212121")
		};

		public BrowserLaunchMode LaunchMode { get => LaunchOptions.LaunchMode; set => LaunchOptions.LaunchMode = value; }
		public BrowserTitleMode TitleMode { get => LaunchOptions.TitleMode; set => LaunchOptions.TitleMode = value; }

		public static readonly BindableProperty ToolbarBackgroundColorProperty
			= BindableProperty.Create(nameof(ToolbarBackgroundColor), typeof(Color), typeof(WebViewBrowserLinkBehavior), propertyChanged: UpdateBackgroundColor);
		public static readonly BindableProperty ToolbarForegroundColorProperty
			= BindableProperty.Create(nameof(ToolbarForegroundColor), typeof(Color), typeof(WebViewBrowserLinkBehavior), propertyChanged: UpdateForegroundColor);

		public Color ToolbarBackgroundColor
		{
			get { return (Color)GetValue(ToolbarBackgroundColorProperty); }
			set { SetValue(ToolbarBackgroundColorProperty, value); }
		}
		public Color ToolbarForegroundColor
		{
			get { return (Color)GetValue(ToolbarForegroundColorProperty); }
			set { SetValue(ToolbarForegroundColorProperty, value); }
		}


		protected override void OnAttachedTo(WebView webView)
		{
			base.OnAttachedTo(webView);
			webView.Navigating += WebViewOnNavigating;
		}

		protected override void OnDetachingFrom(WebView webView)
		{
			base.OnDetachingFrom(webView);
			webView.Navigating -= WebViewOnNavigating;
		}

		private void WebViewOnNavigating(object sender, WebNavigatingEventArgs e)
		{
			if (!e.Url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
				return;

			e.Cancel = true;
			_ = Browser.OpenAsync(e.Url, this.LaunchOptions);
		}

		private static BindableProperty.BindingPropertyChangedDelegate UpdateBackgroundColor = (b, o, n) =>
			 ((WebViewBrowserLinkBehavior)b).LaunchOptions.PreferredToolbarColor = (Color)n;

		private static BindableProperty.BindingPropertyChangedDelegate UpdateForegroundColor = (b, o, n) =>
			((WebViewBrowserLinkBehavior)b).LaunchOptions.PreferredControlColor = (Color)n;
	}
}
