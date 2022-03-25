using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using CddaX.CddaLib;

namespace CddaX.MusicBrainz
{
    class ApiClient : IDisposable
    {
        private string m_baseUrl = "https://musicbrainz.org/ws/2";
        private string m_coverArtBaseUrl = "https://coverartarchive.org";
        private WebClient m_webclient = new WebClient();

        public ApiClient()
        {
            string url = Environment.GetEnvironmentVariable("MUSICBRAINZ_API_URL");
            if (!string.IsNullOrEmpty(url))
            {
                m_baseUrl = url.TrimEnd('/');
            }

            url = Environment.GetEnvironmentVariable("COVERARTARCHIVE_API_URL");
            if (!string.IsNullOrEmpty(url))
            {
                m_coverArtBaseUrl = url.TrimEnd('/');
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_webclient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApiClient()
        {
            Dispose(false);
        }

        private void PrepareClient()
        {
            m_webclient.Encoding = Encoding.UTF8;
            m_webclient.Headers.Set(HttpRequestHeader.UserAgent, "CddaX.NET/2022.03 (jonas@kuemmerlin.eu)");
        }

        public XmlDocument Xml(string url)
        {
            PrepareClient();
            string data = m_webclient.DownloadString(m_baseUrl + url);
            XmlDocument d = new XmlDocument();
            d.LoadXml(data);
            return d;
        }

        public Release[] ReleasesForDiscId(string discid)
        {
            XmlDocument d = null;

            try
            {
                string url = string.Format("/discid/{0}?inc=labels+artists", discid);
                d = Xml(url);
            }
            catch (WebException e)
            {
                // HTTP 404 is okay, will be mapped to empty list
                // other errors are not okay
                if (!(e.Response is HttpWebResponse) || ((HttpWebResponse)e.Response).StatusCode != HttpStatusCode.NotFound)
                    throw;
            }

            if (d != null)
            {
                XmlNamespaceManager nsm = new XmlNamespaceManager(d.NameTable);
                nsm.AddNamespace("mb", d.DocumentElement.NamespaceURI);

                XmlNodeList releaseEls = d.SelectNodes("/mb:metadata/mb:disc/mb:release-list/mb:release", nsm);

                Release[] result = new Release[releaseEls.Count];
                for (int i = 0; i < releaseEls.Count; ++i)
                {
                    result[i] = Release.FromXml(releaseEls[i]);
                }

                return result;
            }

            return new Release[0];
        }

        public Release[] ReleasesForToc(Toc toc)
        {
            return ReleasesForDiscId(DiscId.FromToc(toc));
        }

        public Release FullReleaseById(string releaseId)
        {
            string url = string.Format("/release/{0}?inc=recordings+artist-credits+recording-rels+work-rels+recording-level-rels+work-level-rels+discids+artist-rels+isrcs", releaseId);
            XmlDocument d = Xml(url);

            XmlNamespaceManager nsm = new XmlNamespaceManager(d.NameTable);
            nsm.AddNamespace("mb", d.DocumentElement.NamespaceURI);

            XmlNode releaseEl = d.SelectSingleNode("/mb:metadata/mb:release", nsm);
            if (releaseEl != null)
                return Release.FromXml(releaseEl);

            return null;
        }

        public byte[] CoverArtById(string releaseId)
        {
            PrepareClient();
            return m_webclient.DownloadData(m_coverArtBaseUrl + string.Format("/release/{0}/front-1200", releaseId));
        }
    }
}
