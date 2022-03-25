using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CddaX.MusicBrainz
{
    public class Release
    {
        public string Id { get; set; }
        public string[] Artists { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Country { get; set; }
        public string Barcode { get; set; }
        public Medium[] Media { get; set; }

        public string ArtistsStr
        {
            get
            {
                return string.Join(", ", Artists);
            }
        }

        public override string ToString()
        {
            return string.Format("MusicBrainz.Release(Id={0})", Id);
        }

        public Medium MediumForDiscId(string discid)
        {
            foreach (Medium m in Media)
            {
                foreach (string id in m.DiscIds)
                {
                    if (id == discid)
                        return m;
                }
            }

            return null;
        }

        public Medium MediumForToc(CddaLib.Toc toc)
        {
            return MediumForDiscId(DiscId.FromToc(toc));
        }

        public static Release FromXml(XmlNode releaseEl)
        {
            Release r = new Release();

            XmlNamespaceManager nsm = new XmlNamespaceManager(releaseEl.OwnerDocument.NameTable);
            nsm.AddNamespace("mb", releaseEl.NamespaceURI);

            if (releaseEl.Attributes["id"] != null)
                r.Id = releaseEl.Attributes["id"].InnerText;

            XmlNode titleEl = releaseEl.SelectSingleNode("./mb:title", nsm);
            if (titleEl != null)
            {
                r.Title = titleEl.InnerText;
            }

            XmlNodeList artistNameEls = releaseEl.SelectNodes("./mb:artist-credit/mb:name-credit/mb:artist/mb:name", nsm);
            r.Artists = new string[artistNameEls.Count];
            for (int i = 0; i < r.Artists.Length; ++i)
            {
                r.Artists[i] = artistNameEls[i].InnerText;
            }

            XmlNode dateEl = releaseEl.SelectSingleNode("./mb:date", nsm);
            if (dateEl != null)
            {
                r.Date = dateEl.InnerText;
            }

            XmlNode countryEl = releaseEl.SelectSingleNode("./mb:country", nsm);
            if (countryEl != null)
            {
                r.Country = countryEl.InnerText;
            }

            XmlNode barcodeEl = releaseEl.SelectSingleNode("./mb:barcode", nsm);
            if (barcodeEl != null)
            {
                r.Barcode = barcodeEl.InnerText;
            }

            XmlNodeList mediaEls = releaseEl.SelectNodes("./mb:medium-list/mb:medium", nsm);
            r.Media = new Medium[mediaEls.Count];
            for (int i = 0; i < r.Media.Length; ++i)
            {
                r.Media[i] = Medium.FromXml(mediaEls[i]);
            }

            return r;
        }
    }
}
