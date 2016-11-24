using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TentativeTitle
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        internal int DisplayWidth { get; set; }
        [DataMember]
        internal int DisplayHeight { get; set; }
        [DataMember]
        internal bool BorderlessMode { get; set; }
        [DataMember]
        internal bool FullScreen { get; set; }
        [DataMember]
        internal bool VSync { get; set; }
        [DataMember]
        internal double VolumeSound { get; set; }
        [DataMember]
        internal double VolumeMusic { get; set; }

        private static Settings _singleton = null;

        public Settings()
        { }

        public static Settings GetSingleton() { return _singleton; }

        public static void UpdateSingleton(Settings instance)
        {
            _singleton = instance;
        }

        public void DefaultSettings()
        {
            DisplayWidth = 800;
            DisplayHeight = 480;
            BorderlessMode = false;
            FullScreen = false;
            VSync = false;
            VolumeSound = 1.0;
            VolumeMusic = 1.0;
        }

        public Settings CopySelf()
        {
            Settings copy = new Settings();
            copy.DisplayWidth = DisplayWidth;
            copy.DisplayHeight = DisplayHeight;
            copy.BorderlessMode = BorderlessMode;
            copy.FullScreen = FullScreen;
            copy.VSync = VSync;
            copy.VolumeSound = VolumeSound;
            copy.VolumeMusic = VolumeMusic;
            return copy;
        }
    }
}
