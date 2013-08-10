using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Eyegregator.Controller;
using System.Net;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Xml;
using System.Collections.Generic;

namespace Eyegregator.Controller
{
    public class RSSUtil
    {
		public BackgroundWorker bgndWorker = new BackgroundWorker();
		public XmlDocument doc = new XmlDocument();
		String streamResult;
		public List<string> Urls
		{ get; set ;}
		public List<string> rssFeeds = new List<string>();


        public RSSUtil()
        {
			rssFeeds.Clear ();
			bgndWorker.DoWork += (sender, e) => 
			{
				foreach (var _url in Urls) {		
					try {
						var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri(_url));
						httpReq.ContentType = "application/xml";
						httpReq.Method = "GET";

						using (WebResponse response = httpReq.GetResponse()) {
							using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
								streamResult = reader.ReadToEnd ();	
								doc.LoadXml (streamResult);
								foreach (var item in doc.GetElementsByTagName("title").Cast<XmlNode>()) {
									rssFeeds.Add (item.InnerText);
								}
							}
						}	
					} catch (Exception ex)
					{
					}
				}
			};
        }


    }
}