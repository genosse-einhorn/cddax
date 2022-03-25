using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CddaX.MusicBrainz
{
    public class Medium
    {
        public int Position;
        public string[] DiscIds;
        public Track[] Tracks;

        public static Medium FromXml(XmlNode mediumEl)
        {
            Medium m = new Medium();

            XmlNamespaceManager nsm = new XmlNamespaceManager(mediumEl.OwnerDocument.NameTable);
            nsm.AddNamespace("mb", mediumEl.NamespaceURI);

            XmlNode positionEl = mediumEl.SelectSingleNode("./mb:position", nsm);
            if (positionEl != null)
            {
                int.TryParse(positionEl.InnerText, out m.Position);
            }

            XmlNodeList discIdEls = mediumEl.SelectNodes("./mb:disc-list/mb:disc/@id", nsm);
            m.DiscIds = new string[discIdEls.Count];
            for (int i = 0; i < m.DiscIds.Length; ++i)
            {
                m.DiscIds[i] = discIdEls[i].InnerText;
            }

            XmlNodeList trackEls = mediumEl.SelectNodes("./mb:pregap|./mb:track-list/mb:track", nsm);
            m.Tracks = new Track[trackEls.Count];
            for (int i = 0; i < m.Tracks.Length; ++i)
            {
                m.Tracks[i] = Track.FromXml(trackEls[i]);
            }

            return m;
        }
    }
}
