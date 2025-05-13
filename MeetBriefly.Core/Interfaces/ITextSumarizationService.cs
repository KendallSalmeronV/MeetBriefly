using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Core.Interfaces
{
    public interface ITextSumarizationService
    {
        Task<string> SumarizeTextAsync(string text);

    }
}
