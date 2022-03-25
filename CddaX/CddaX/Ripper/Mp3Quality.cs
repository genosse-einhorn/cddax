using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.Ripper
{
    public struct Mp3Quality
    {
        public string Description;
        public string LameParameter;

        private Mp3Quality(string d, string p)
        {
            this.Description = d;
            this.LameParameter = p;
        }

        public override string ToString()
        {
            return Description;
        }

        public static Mp3Quality FindByLameParameter(string p)
        {
            foreach (Mp3Quality q in SupportedQualities)
            {
                if (string.Equals(q.LameParameter, p, StringComparison.InvariantCultureIgnoreCase))
                {
                    return q;
                }
            }

            return RecommendedQuality;
        }

        public static Mp3Quality[] SupportedQualities = new Mp3Quality[] {
            new Mp3Quality("  8kbit/s", "-b 8"),
            new Mp3Quality(" 16kbit/s", "-b 16"),
            new Mp3Quality(" 24kbit/s", "-b 24"),
            new Mp3Quality(" 32kbit/s", "-b 32"),
            new Mp3Quality(" 40kbit/s", "-b 40"),
            new Mp3Quality(" 48kbit/s", "-b 48"),
            new Mp3Quality(" 64kbit/s", "-b 64"),
            new Mp3Quality(" 80kbit/s", "-b 80"),
            new Mp3Quality(" 96kbit/s", "-b 96"),
            new Mp3Quality("112kbit/s", "-b 112"),
            new Mp3Quality("128kbit/s", "-b 128"),
            new Mp3Quality("160kbit/s", "-b 160"),
            new Mp3Quality("192kbit/s " + CddaX.Properties.Resources.RecommendedSuffix, "-b 192"),
            new Mp3Quality("224kbit/s", "-b 224"),
            new Mp3Quality("256kbit/s", "-b 256"),
            new Mp3Quality("320kbit/s", "-b 320"),
            new Mp3Quality("VBR 0 " + CddaX.Properties.Resources.BestQualitySuffix, "-V 0"),
            new Mp3Quality("VBR 1", "-V 1"),
            new Mp3Quality("VBR 2", "-V 2"),
            new Mp3Quality("VBR 3", "-V 3"),
            new Mp3Quality("VBR 4", "-V 4"),
            new Mp3Quality("VBR 5", "-V 5"),
            new Mp3Quality("VBR 6", "-V 6"),
            new Mp3Quality("VBR 7", "-V 7"),
            new Mp3Quality("VBR 8", "-V 8"),
            new Mp3Quality("VBR 9 " + CddaX.Properties.Resources.WorstQualitySuffix, "-V 9")
        };

        public static Mp3Quality RecommendedQuality = SupportedQualities[12];
    }
}
