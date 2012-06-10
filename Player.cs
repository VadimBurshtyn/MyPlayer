using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;

namespace MyPlayer
{
    class Player
    {
        private int stream;
        public Player()
        {
            try
            {
                Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                BassNet.Registration("vadimburshtyn@gmail.com", "2X1731423152222");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Play(string path)
        {
            try
            {
                Bass.BASS_StreamFree(stream);
                stream = Bass.BASS_StreamCreateFile(path, 0L, 0L, BASSFlag.BASS_DEFAULT);
                Bass.BASS_ChannelPlay(stream, false);
/*
//                 BASSError be = Bass.BASS_ErrorGetCode();
//                 Bass.BASS_StreamFree(stream);
//                 Bass.BASS_Free();
*/

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Pause()
        {
            Bass.BASS_Pause();
        }
    }
}
