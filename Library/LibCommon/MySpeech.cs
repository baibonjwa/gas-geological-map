using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SpeechLib;

namespace LibCommon
{
    public class MySpeech
    {
        private static MySpeech _instance = null;
        private SpVoiceClass voice = null;
        //private SpeechLib.SpVoice voice = null;
        private StreamReader _reader = null;

        private MySpeech()
        {
            Initialize();
        }

        public static MySpeech Instance()
        {
            if (_instance == null)
                _instance = new MySpeech();
            return _instance;
        }

        private void Initialize()
        {
            try
            {
                voice = new SpVoiceClass();
                voice.EndStream += Voice_EndStream;

                ISpeechObjectTokens objTokens = voice.GetVoices("", "");
                const string useVoice = "ScanSoft Mei-Ling_Full_22kHz";
                int useIndex = -1;
                for (int i = 0; i < objTokens.Count; i++)
                {
                    SpObjectToken sot = objTokens.Item(i);
                    if (sot.GetDescription(0) == useVoice)
                    {
                        useIndex = i;
                        break;
                    }
                }
                if (useIndex == -1)
                {
                    useIndex = 0;
                }
                voice.Voice = objTokens.Item(useIndex);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }
        }

        public void Voice_EndStream(int StreamNumber, object StreamPosition)
        {

        }

        public int Volume
        {
            get
            {
                return voice.Volume;
            }
            set
            {
                voice.SetVolume((ushort)(value));
                //voice.Volume = value;
            }
        }

        public int Rate
        {
            get
            {
                return voice.Rate;
            }
            set
            {
                voice.SetRate(value);
            }
        }

        public void Speak()
        {
            try
            {
                _reader = new StreamReader
                    (
                    Application.StartupPath + "\\NoticeTxt.txt",
                    Encoding.Default
                    );
                string text = _reader.ReadToEnd();
                _reader.Close();
                voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
            catch (Exception err)
            {
                throw (new Exception("an error occurs: " + err.Message));
            }
        }

        public void Stop()
        {
            voice.Speak(string.Empty,
                SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }

        public void Pause()
        {
            voice.Pause();
        }

        public void Continue()
        {
            voice.Resume();
        }
    }
}
