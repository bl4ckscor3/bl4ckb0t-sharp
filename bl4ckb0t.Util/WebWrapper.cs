using Coypu;
using NSoup;
using NSoup.Nodes;
using System.Net;

namespace bl4ckb0t.Util
{
	/// <summary>
	/// A class for wrappers to create web connections
	/// </summary>
	public class WebWrapper
    {
		/// <summary>
		/// Creates a new PhantomJS browser session using Coypu and returns it
		/// This session has JavaScript enabled
		/// </summary>
		/// <param name="page">The webpage to load initially</param>
		/// <returns>The browser session with the loaded webpage</returns>
		public static BrowserSession NewCoypu(string page)
		{
			BrowserSession browser = new BrowserSession(new SessionConfiguration {
				Port = 80,
				SSL = false,
				Driver = typeof(Coypu.Drivers.Selenium.SeleniumWebDriver),
				Browser = Coypu.Drivers.Browser.PhantomJS
			});

			browser.Visit(page);
			return browser;
		}

		/// <summary>
		/// Creates a new NSoup session and returns it. The user agent will be set to a Windows 10 + Firefox 51.0 system
		/// This session has no JavaScript enabled
		/// </summary>
		/// <param name="page">The webpage to load initially</param>
		/// <param name="ignoreContentType">Wether or not to ignore the content type of the website</param>
		/// <returns>The browser session with the loaded webpage</returns>
		public static Document NewNSoup(string page, bool ignoreContentType = false)
		{
			return NSoupClient.Connect(page).UserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:51.0) Gecko/20100101 Firefox/51.0").IgnoreContentType(ignoreContentType).Get();
		}

		/// <summary>
		/// Creates a new WebClient and sets its user agent to a Windows 10 + Firefox 51.0 system
		/// This does NOT load a webpage
		/// </summary>
		/// <returns>The client</returns>
		public static WebClient NewClient()
		{
			WebClient client = new WebClient();

			client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:51.0) Gecko/20100101 Firefox/51.0");
			return client;
		}
    }
}
