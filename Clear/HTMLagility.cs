using System.IO;
using HtmlAgilityPack;

public class HTMLagility
{
	internal static void ExtractHref(string URL, string path)
	{
		// declaring & loading dom
		HtmlWeb web = new HtmlWeb();
		HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
		doc = web.Load(URL);

		// extracting all links
		foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
		{
			HtmlAttribute att = link.Attributes["href"];

			if (att.Value.Contains("a"))
			{
				// showing output
				File.WriteAllText(path, att.Value);

			}
		}
	}

}
