using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace sandcastle_html_sitemap_gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sandcastle HTML Sitemap Generator");

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: sandcastle-html-sitemap-gen.exe folder-with-html-output website-url");
                return;
            }

            var path = args[0];
            var baseUrl = args[1];

            var urls = new List<string>();
            urls.Add(baseUrl + "index.html");

            var files2 = Directory.GetFiles(path + "\\html", "*.htm", SearchOption.TopDirectoryOnly);
            foreach (var s in files2)
            {
                var file = s.Replace(path + "\\html", "").Replace("\\", "");
                urls.Add(baseUrl + "html/" + file);
            }

            var gen = new SitemapGenFromURLs();
            var xml = gen.Generate(urls.ToArray());

            File.WriteAllLines(path + "\\sitemap.xml", xml);
        }
    }
}
