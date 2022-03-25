using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.IO;
using CddaX.Log;

namespace CddaX.Ripper
{
    public class RipWorker : BackgroundWorker
    {
        public RipWorker()
        {
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            RipParameters p = (RipParameters)e.Argument;



            this.ReportProgress(0, CddaX.Properties.Resources.InitializingMessage);

            // Create Directory
            Directory.CreateDirectory(p.CominedTargetDirectory);

            // Calculate stuff for progress information
            int totalFramesToRead = 0;
            int numberOfFramesRead = 0;
            foreach (MetaStore.TrackMeta track in p.DiscMeta.Tracks)
            {
                if (track.Selected)
                {
                    totalFramesToRead += track.Length.Lba;
                }
            }

            // Save cover.jpg
            if (p.DiscMeta.CoverBytes != null && p.DiscMeta.CoverBytes.Length > 0)
            {
                this.ReportProgress(0, CddaX.Properties.Resources.SavingCoverMessage);
                using (var f = FileUtils.CreateFileExclusiveNumbered(p.CominedTargetDirectory, "cover", "jpg"))
                {
                    f.Write(p.DiscMeta.CoverBytes, 0, p.DiscMeta.CoverBytes.Length);
                }
            }

            using (var h = CddaLib.ScsiHandle.Create(p.Drive))
            {
                // Extract the tracks
                byte[] buffer = new byte[2352 * 20];
                foreach (MetaStore.TrackMeta track in p.DiscMeta.Tracks)
                {
                    if (!track.Selected)
                        continue;

                    CddaLib.BlockAddress pos = track.Start;
                    CddaLib.BlockDelta remaining = track.Length;

                    using (IFileWriter writer = CreateWriter(p))
                    {
                        InitializeWriter(writer, p, track);

                        while (remaining.Lba > 0)
                        {
                            if (this.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            CddaLib.BlockDelta tocopy = CddaLib.BlockDelta.FromLba(
                                Math.Min(remaining.Lba, buffer.Length / 2352));

                            CddaLib.CddaOperations.ReadAudioData(h, pos, tocopy, buffer);

                            writer.WriteData(buffer, 0, tocopy.Lba * 588);

                            numberOfFramesRead += tocopy.Lba;
                            pos.Lba += tocopy.Lba;
                            remaining.Lba -= tocopy.Lba;
                            this.ReportProgress(numberOfFramesRead * 100 / totalFramesToRead,
                                string.Format("{0} ({1})", 
                                    string.Format(CddaX.Properties.Resources.TrackNoMessage, track.TrackNo),
                                CddaLib.BlockDelta.FromDifference(track.Start, pos)));
                        }
                    }
                }
            }

            e.Result = "Finish!";
        }

        private IFileWriter CreateWriter(Ripper.RipParameters p)
        {
            IFileWriter w = null;
            if (p.FileFormat == RipParameters.FileFormats.Flac)
            {
                w = new FlacWriter();
            }
            else if (p.FileFormat == RipParameters.FileFormats.Mp3)
            {
                w = new LameWriter();
            }
            else
            {
                w = new WavFileWriter();
            }

            return new PregapDetectingWriter(w);
        }

        private void InitializeWriter(IFileWriter w, RipParameters p, MetaStore.TrackMeta track)
        {
            FileWriterMeta m = new FileWriterMeta();

            m.TrackNo = track.TrackNo;
            m.TotalTrackCount = p.DiscMeta.Tracks.Length;
            m.Artist = p.DiscMeta.Artist;
            if (!string.IsNullOrEmpty(track.Artist))
                m.Artist = track.Artist;
            m.Composer = p.DiscMeta.Composer;
            if (!string.IsNullOrEmpty(track.Composer))
                m.Composer = track.Composer;
            m.Title = track.Title;
            m.AlbumTitle = p.DiscMeta.Title;
            m.Year = p.DiscMeta.Year;
            m.DiscNo = p.DiscMeta.DiscNo;
            m.Mp3Quality = p.Mp3Quality;

            string title = string.Format("Track {0}", m.TrackNo);
            if (!string.IsNullOrEmpty(m.Title))
                title = m.Title;

            string basename = (string.Format("{0:D2} - {1}", track.TrackNo, FileUtils.FilenameWithInvalidCharsReplaced(title)));
            w.Begin(FileUtils.CreateFileExclusiveNumbered(p.CominedTargetDirectory, basename, w.FilenameExtension), m);
        }
    }
}
