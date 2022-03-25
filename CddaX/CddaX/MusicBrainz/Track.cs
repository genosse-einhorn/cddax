using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CddaX.MusicBrainz
{
    public class Track
    {
        public string Id;
        public int Number;
        public string Title;
        public string[] Artists;
        public string[] Composers;
        public string[] Isrcs;

        public static Track FromXml(XmlNode trackEl)
        {
            Track t = new Track();

            if (trackEl.Attributes["id"] != null)
                t.Id = trackEl.Attributes["id"].InnerText;

            XmlNamespaceManager nsm = new XmlNamespaceManager(trackEl.OwnerDocument.NameTable);
            nsm.AddNamespace("mb", trackEl.NamespaceURI);

            XmlNode numberEl = trackEl.SelectSingleNode("./mb:number", nsm);
            if (numberEl != null)
            {
                int.TryParse(numberEl.InnerText, out t.Number);
            }

            XmlNode titleEl = trackEl.SelectSingleNode("./mb:recording/mb:title", nsm);
            if (titleEl != null)
            {
                t.Title = titleEl.InnerText;
            }

            XmlNodeList artistNamesEl = trackEl.SelectNodes("./mb:recording/mb:artist-credit/mb:name-credit/mb:artist/mb:name", nsm);
            t.Artists = new string[artistNamesEl.Count];
            for (int i = 0; i < t.Artists.Length; ++i)
            {
                t.Artists[i] = artistNamesEl[i].InnerText;
            }

            XmlNodeList composerNamesEl = trackEl.SelectNodes("./mb:recording/mb:relation-list[@target-type=\"work\"]/mb:relation[@type=\"performance\"]/mb:work/mb:relation-list[@target-type=\"artist\"]/mb:relation[@type=\"composer\"]/mb:artist/mb:name", nsm);
            t.Composers = new string[composerNamesEl.Count];
            for (int i = 0; i < t.Composers.Length; ++i)
            {
                t.Composers[i] = composerNamesEl[i].InnerText;
            }

            XmlNodeList isrcsEl = trackEl.SelectNodes("./mb:recording/mb:isrc-list/mb:isrc/@id", nsm);
            t.Isrcs = new string[isrcsEl.Count];
            for (int i = 0; i < t.Isrcs.Length; ++i)
            {
                t.Isrcs[i] = isrcsEl[i].InnerText;
            }

            return t;
        }
    }
}
