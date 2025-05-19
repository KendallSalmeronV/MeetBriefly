using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Core.Interfaces
{
    public interface IAudioConverter
    {
        Stream ConvertMp3ToWav(Stream stream);
    }
}
