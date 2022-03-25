using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using CddaX.Log;

namespace CddaX.CddaLib
{
    class CddaOperations
    {
        public static Toc ReadToc(IScsiHandle c)
        {
            byte[] cdb = new byte[10];
            byte[] responsebuf = new byte[8192];

            cdb[0] = 0x43;
            cdb[1] = 2; // msf bit
            cdb[2] = 2; // format 0010b
            cdb[6] = 1;
            cdb[7] = (byte)(responsebuf.Length >> 8);
            cdb[8] = (byte)(responsebuf.Length & 0xff);

            c.ExecScsiCommand(cdb, responsebuf);

            int toclen = Math.Min(((int)responsebuf[0] << 8) | (int)responsebuf[1], responsebuf.Length - 2);
            int desccount = (toclen - 2) / 11;

            Toc t = new Toc();
            t.FirstTrackNo = 100;
            t.LastTrackNo = -1;
            for (int i = 0; i < 100; ++i)
            {
                t.Tracks[i].Index = -1;
                t.Tracks[i].Start = BlockAddress.FromLba(int.MinValue);
                t.Tracks[i].End = BlockAddress.FromLba(int.MaxValue);
            }

            // first run - handle regular tracks
            for (int i = 0; i < desccount; ++i)
            {
                int o = 4 + i * 11;

                int point = responsebuf[o + 3];
                if (point > 0 && point <= 99)
                {
                    // regular track
                    t.Tracks[point].Session = responsebuf[o + 0];
                    t.Tracks[point].Control = (byte)(responsebuf[o + 1] & 0xf);
                    t.Tracks[point].Adr = (byte)((responsebuf[o + 1] & 0xf0) >> 4);
                    t.Tracks[point].Index = point;
                    t.Tracks[point].Start = BlockAddress.FromMsf(responsebuf[o + 8], responsebuf[o + 9], responsebuf[o + 10]);

                    if (t.FirstTrackNo > point)
                        t.FirstTrackNo = point;
                    if (t.LastTrackNo < point)
                        t.LastTrackNo = point;
                }
            }

            // fixup end points and sanity-check start points
            for (int k = t.FirstTrackNo; k <= t.LastTrackNo; ++k)
            {
                if (t.Tracks[k].Start < t.Tracks[k - 1].Start)
                    t.Tracks[k].Start = t.Tracks[k - 1].Start;

                if (t.Tracks[k].Start < t.Tracks[k - 1].End)
                    t.Tracks[k - 1].End = t.Tracks[k].Start;
            }

            // second run - handle lead-out(s) and fixup more end points
            for (int i = 0; i < desccount; ++i)
            {
                int o = 4 + i * 11;

                int point = responsebuf[o + 3];
                if (point == 0xa2)
                {
                    // lead-out
                    BlockAddress loAdr = BlockAddress.FromMsf(responsebuf[o + 8], responsebuf[o + 9], responsebuf[o + 10]);

                    // fill in possibly missing end lba for other tracks
                    for (int k = t.FirstTrackNo; k <= t.LastTrackNo; ++k)
                    {
                        if (t.Tracks[k].Start < loAdr && t.Tracks[k].End > loAdr)
                            t.Tracks[k].End = loAdr;
                    }
                }
            }

            // if the first track starts after 00:02:00, something is fishy, i.e. there is a hidden track
            if (t.FirstTrackNo == 1 && t.Tracks[1].Start.Lba > 0)
            {
                t.Tracks[0].Index = 0;
                t.Tracks[0].Start = BlockAddress.FromLba(0);
                t.Tracks[0].Adr = t.Tracks[1].Adr;
                t.Tracks[0].Control = t.Tracks[1].Control;
                t.Tracks[0].Session = t.Tracks[1].Session;
                t.FirstTrackNo = 0;
            }

            return t;
        }

        public static CdTextData ReadCdText(IScsiHandle h)
        {
            byte[] cdb = new byte[10];
            byte[] responsebuf = new byte[4 + 2048 * 18];

            cdb[0] = 0x43;
            cdb[1] = 2; // msf flag
            cdb[2] = 5; // format 0101b
            cdb[6] = 1;
            cdb[7] = (byte)(responsebuf.Length >> 8);
            cdb[8] = (byte)(responsebuf.Length & 0xff);

            h.ExecScsiCommand(cdb, responsebuf);

            int toclen = Math.Min(((int)responsebuf[0] << 8) | (int)responsebuf[1], responsebuf.Length - 2);
            int desccount = (toclen - 2) / 18;

            List<byte> charbuf = new List<byte>();
            CdTextData r = new CdTextData();
            Encoding latin1 = Encoding.GetEncoding("latin1");

            for (int d = 0; d < desccount; ++d)
            {
                int o = 4 + d * 18;

                // TODO: verify CRC

                byte pack_type = responsebuf[o + 0];
                byte trackno = responsebuf[o + 1];
                byte counter = responsebuf[o + 2];
                byte character_pos = (byte)(responsebuf[o + 3] & 0xf);
                byte block_no = (byte)((responsebuf[o + 3] & 0x70) >> 4);
                byte double_byte = (byte)((responsebuf[o + 3] & 0x80) >> 7);

                if (block_no != 0)
                    continue; // FIXME! handle other blocks

                if (double_byte != 0)
                    continue; // FIXME! handle double byte encodings

                for (int i = 0; i < 12; ++i)
                {
                    if (responsebuf[o + 4 + i] != 0)
                    {
                        charbuf.Add(responsebuf[o + 4 + i]);
                    }
                    else if (charbuf.Count > 0)
                    {
                        if (pack_type == 0x80 /* title */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Title = r.TrackData[trackno - 1].Title;
                            }
                            else
                            {
                                r.TrackData[trackno].Title = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x81 /* artist */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Artist = r.TrackData[trackno - 1].Artist;
                            }
                            else
                            {
                                r.TrackData[trackno].Artist = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x82 /* writer */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Writer = r.TrackData[trackno - 1].Writer;
                            }
                            else
                            {
                                r.TrackData[trackno].Writer = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x83 /* composer */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Composer = r.TrackData[trackno - 1].Composer;
                            }
                            else
                            {
                                r.TrackData[trackno].Composer = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x84 /* arranger */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Arranger = r.TrackData[trackno - 1].Arranger;
                            }
                            else
                            {
                                r.TrackData[trackno].Arranger = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x85 /* Message */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Message = r.TrackData[trackno - 1].Message;
                            }
                            else
                            {
                                r.TrackData[trackno].Message = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x86 /* disc identification */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].DiscId = r.TrackData[trackno - 1].DiscId;
                            }
                            else
                            {
                                r.TrackData[trackno].DiscId = latin1.GetString(charbuf.ToArray());
                            }
                        }
                        if (pack_type == 0x8e /* isrc */)
                        {
                            if (charbuf[0] == '\t' && charbuf.Count == 1 && trackno > 0)
                            {
                                r.TrackData[trackno].Isrc = r.TrackData[trackno - 1].Isrc;
                            }
                            else
                            {
                                r.TrackData[trackno].Isrc = latin1.GetString(charbuf.ToArray());
                            }
                        }

                        trackno++;
                        charbuf.Clear();
                    }
                }
            }

            return r;
        }

        public static CdTextData ReadCdTextNofail(IScsiHandle h)
        {
            try
            {
                return ReadCdText(h);
            }
            catch (ScsiException e)
            {
                Logger.Exception(e, "Reading CD-TEXT");
                return new CdTextData();
            }
            catch (Win32Exception e)
            {
                Logger.Exception(e, "Reading CD-TEXT");
                return new CdTextData();
            }
        }

        public static string ReadIsrc(IScsiHandle h, int trackno)
        {
            byte[] responsebuf = new byte[24];
            byte[] cdb = new byte[10];

            cdb[0] = 0x42;
            cdb[2] = 0x40;
            cdb[3] = 3;
            cdb[6] = (byte)trackno;
            cdb[8] = (byte)responsebuf.Length;

            h.ExecScsiCommand(cdb, responsebuf);

            StringBuilder sb = new StringBuilder();
            if ((responsebuf[8] & 0x80) != 0)
            {
                for (int i = 0; i < 13; ++i)
                {
                    byte b = responsebuf[9 + i];
                    if (b == 0)
                        break;

                    sb.Append((char)b);
                }
            }

            return sb.ToString();
        }

        public static string ReadIsrcNofail(IScsiHandle h, int trackno)
        {
            try
            {
                return ReadIsrc(h, trackno);
            }
            catch (ScsiException e)
            {
                Logger.Exception(e, "Reading ISRC for Track {0}", trackno);
                return "";
            }
            catch (Win32Exception e)
            {
                Logger.Exception(e, "Reading ISRC for Track {0}", trackno);
                return "";
            }
        }

        public static string ReadMcn(IScsiHandle h)
        {
            byte[] responsebuf = new byte[24];
            byte[] cdb = new byte[10];

            cdb[0] = 0x42;
            cdb[2] = 0x40;
            cdb[3] = 2;
            cdb[8] = (byte)responsebuf.Length;

            h.ExecScsiCommand(cdb, responsebuf);

            StringBuilder sb = new StringBuilder();
            if ((responsebuf[8] & 0x80) != 0)
            {
                for (int i = 0; i < 14; ++i)
                {
                    byte b = responsebuf[9 + i];
                    if (b == 0)
                        break;

                    sb.Append((char)b);
                }
            }

            return sb.ToString();
        }

        public static string ReadMcnNofail(IScsiHandle h)
        {
            try
            {
                return ReadMcn(h);
            }
            catch (ScsiException e)
            {
                Logger.Exception(e, "Reading MCN");
                return "";
            }
            catch (Win32Exception e)
            {
                Logger.Exception(e, "Reading MCN");
                return "";
            }
        }

        public static void ReadAudioData(IScsiHandle h, BlockAddress start, BlockDelta length, byte[] buffer)
        {
            byte[] cdb = new byte[12];
            cdb[0] = 0xbe;
            cdb[1] = 4;
            cdb[2] = (byte)((start.Lba & 0xff000000) >> 24);
            cdb[3] = (byte)((start.Lba & 0x00ff0000) >> 16);
            cdb[4] = (byte)((start.Lba & 0x0000ff00) >> 8);
            cdb[5] = (byte)((start.Lba & 0x000000ff));
            cdb[6] = (byte)((length.Lba & 0x00ff0000) >> 16);
            cdb[7] = (byte)((length.Lba & 0x0000ff00) >> 8);
            cdb[8] = (byte)((length.Lba & 0x000000ff));
            cdb[9] = 16;

            h.ExecScsiCommand(cdb, buffer);
        }
    }
}
