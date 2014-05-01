using System;
using System.Xml;
using System.IO;
using System.Timers;

namespace RSSConsole
{
	class MainClass
	{	
		public static void Main (string[] args)
		{	
			Timer aTimer = new Timer ();
			aTimer.Interval = 7200000;
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Enabled = true;
			Console.Title = "RSSConsole [Loading]";
			Reader.Read ();

		}
		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			Console.Clear ();
			Console.Title = "RSSConsole [Loading]";
			Console.WriteLine ("|\tRSS канал обновлен {0}" +
				"\n|\tПриложение самостоятельно проверит обновление ленты через 2 часа", DateTime.Now);
			Console.WriteLine ("|---------------------------------------------------------------");
			Reader.Read ();
			Console.WriteLine ("");
		}
	}

	class Reader
	{

		public static void Read()
		{
			XmlDocument xml = new XmlDocument ();
			xml.Load ("http://mychar.ru/rss");
		
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
			nsmgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			Console.Clear ();
			Console.WriteLine ("|\tИнформация RSS канала обновлена {0}", DateTime.Now);
			Console.WriteLine ("|---------------------------------------------------------------");
			Console.WriteLine ("|\tИнформация о канале ");
			foreach (XmlNode n in xml.SelectNodes("/rss/channel")) {
				Console.WriteLine ("|\tНазвание: {0}", n.SelectSingleNode ("title").InnerText);
				Console.WriteLine ("|\tАдрес: {0}", n.SelectSingleNode ("link").InnerText);
				Console.WriteLine ("|\tЯзык: {0}", n.SelectSingleNode ("language").InnerText);
				Console.WriteLine("|---------------------------------------------------------------");
			}
			foreach (XmlNode n in xml.SelectNodes("/rss/channel/item"))
			{
				Console.WriteLine("|\tЗаголовок: {0}", n.SelectSingleNode("title").InnerText);
				//Console.WriteLine("|\tКомментарии: {0}", n.SelectSingleNode("comments").InnerText);
				Console.WriteLine("|\tОпубликовано: {0}", n.SelectSingleNode("pubDate").InnerText);
				Console.WriteLine("|\tАвтор: {0}", n.SelectSingleNode("dc:creator",nsmgr).InnerText);
				Console.WriteLine("|\tСсылка: {0}", n.SelectSingleNode("link").InnerText);

				Console.WriteLine("|---------------------------------------------------------------");
			}
			Console.Title = "RSSConsole";
			Console.CursorVisible = false;
			Console.SetCursorPosition(0,0);
			Console.ReadLine ();

		}





	}
}

