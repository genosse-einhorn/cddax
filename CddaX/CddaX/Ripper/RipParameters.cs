using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CddaX.Ripper
{
    public class RipParameters
    {
        public enum FileFormats
        {
            Wav,
            Mp3,
            Flac
        }

        public string TargetBaseDirectory { get; set; }
        public string TargetSubDirectory { get; set; }
        public Boolean SubDirectoryEnabled { get; set; }
        public FileFormats FileFormat { get; set; }
        public Mp3Quality Mp3Quality { get; set; }

        public string CominedTargetDirectory
        {
            get
            {
                if (SubDirectoryEnabled)
                {
                    return Path.Combine(TargetBaseDirectory, TargetSubDirectory);
                }
                else
                {
                    return TargetBaseDirectory;
                }
            }
        }

        public string Drive { get; private set; }
        public MetaStore.DiscMeta DiscMeta { get; private set; }

        public RipParameters(string drive, MetaStore.DiscMeta meta)
        {
            this.Drive = drive;
            this.DiscMeta = meta;
            this.FileFormat = FileFormats.Flac;
            this.TargetBaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            this.SubDirectoryEnabled = true;

            string artistFolder = FileUtils.SanitizedFileName(meta.Artist);
            string titleFolder = FileUtils.SanitizedFileName(meta.Title);

            this.TargetSubDirectory = Path.Combine(
                string.IsNullOrEmpty(artistFolder) ? CddaX.Properties.Resources.UnknownArtist : artistFolder,
                string.IsNullOrEmpty(titleFolder) ? CddaX.Properties.Resources.UnknownAlbum : titleFolder);
            this.Mp3Quality = Mp3Quality.RecommendedQuality;
        }

        public void LoadFromRegistry(Util.RegistrySettings registrySettings)
        {
            string prevTargetDir = registrySettings.LoadString("TargetDir", null);
            if (!string.IsNullOrEmpty(prevTargetDir) && Directory.Exists(prevTargetDir))
            {
                this.TargetBaseDirectory = prevTargetDir;
            }

            this.SubDirectoryEnabled = registrySettings.LoadBool("CreateSubfolder", true);
            this.FileFormat = registrySettings.LoadEnum<Ripper.RipParameters.FileFormats>("FileFormat", this.FileFormat);
            this.Mp3Quality = Ripper.Mp3Quality.FindByLameParameter(registrySettings.LoadString("Mp3Quality", this.Mp3Quality.LameParameter));
        }

        public void SaveToRegistry(Util.RegistrySettings registrySettings)
        {
            registrySettings.SaveString("TargetDir", TargetBaseDirectory);
            registrySettings.SaveBool("CreateSubfolder", SubDirectoryEnabled);
            registrySettings.SaveEnum<Ripper.RipParameters.FileFormats>("FileFormat", FileFormat);
            registrySettings.SaveString("Mp3Quality", Mp3Quality.LameParameter);

        }
    }
}
