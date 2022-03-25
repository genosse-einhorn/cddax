using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CddaX.CddaLib;

namespace CddaX.MusicBrainz
{
    class ReleaseFinder
    {
        public struct Release
        {
            public string Id;
            public string Artist;
            public string Album;
            public string Date;
            public string Country;
            public string Barcode;

            public override string ToString()
            {
                return string.Format("CddaX.MusicBrainz.Release(Id=\"{0}\")", Id);
            }
        }

        public static IList<Release> FindReleases(string discid)
        {
            var list = new List<Release>();

            ApiClient c = new ApiClient(); 
            string url = string.Format("/discid/{0}?inc=recordings+isrcs+labels+artists", discid);
            XmlDocument d = c.Xml(url);
            XmlNamespaceManager nsm = new XmlNamespaceManager(d.NameTable);
            nsm.AddNamespace("mb", d.DocumentElement.NamespaceURI);

            foreach (XmlNode releaseNode in d.SelectNodes("/mb:metadata/mb:disc/mb:release-list/mb:release", nsm))
            {
                if (releaseNode.Attributes["id"] == null)
                    continue; // WTF! release without id. Legal according to the schema

                Release r = new Release();
                r.Id = releaseNode.Attributes["id"].InnerText;

                XmlNode titleNode = releaseNode.SelectSingleNode("mb:title", nsm);
                if (titleNode != null)
                    r.Album = titleNode.InnerText;

                var artistList = new List<string>();
                foreach (XmlNode artistNode in releaseNode.SelectNodes("mb:artist-credit/mb:name-credit/mb:artist/mb:name", nsm))
                {
                    artistList.Add(artistNode.InnerText);
                }

                if (artistList.Count != 0)
                    r.Artist = string.Join(", ", artistList);

                XmlNode dateNode = releaseNode.SelectSingleNode("mb:date", nsm);
                if (dateNode != null)
                    r.Date = dateNode.InnerText;

                XmlNode countryNode = releaseNode.SelectSingleNode("mb:country", nsm);
                if (countryNode != null)
                    r.Country = countryNode.InnerText;

                XmlNode barcodeNode = releaseNode.SelectSingleNode("mb:barcode", nsm);
                if (barcodeNode != null)
                    r.Barcode = barcodeNode.InnerText;

                list.Add(r);
            }

            return list;
        }

        public static IList<Release> FindReleases(Toc toc)
        {
            return FindReleases(DiscId.FromToc(toc));
        }
    }
}
