using MeetBriefly.Core.Interfaces;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Infrastructure.Converters
{
    public class AudioConverter : IAudioConverter
    {
        public Stream ConvertMp3ToWav(Stream stream)
        {
            stream.Position = 0;

            var outStream = new MemoryStream();
            using var mp3Reader = new Mp3FileReader(stream);

            using var pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader);
            WaveFileWriter.WriteWavFileToStream(outStream, pcmStream);

            outStream.Position = 0;
            return outStream;
        }
    }
}
