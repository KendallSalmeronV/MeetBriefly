using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetBriefly.Core.Entities
{
    public class ChatResponse
    {
        public ChatValue Value { get; set; }
    }

    public class ChatValue
    {
        public DateTime CreatedAt { get; set; }
        public int FinishReason { get; set; }
        public List<object> ContentTokenLogProbabilities { get; set; }
        public List<object> RefusalTokenLogProbabilities { get; set; }
        public int Role { get; set; }
        public List<ChatContent> Content { get; set; }
        public List<object> ToolCalls { get; set; }
        public object? Refusal { get; set; }
        public object? FunctionCall { get; set; }
        public string Id { get; set; }
        public string Model { get; set; }
        public string SystemFingerprint { get; set; }
        public Usage Usage { get; set; }
    }

    public class Usage
    {
        public int OutputTokenCount { get; set; }
        public int InputTokenCount { get; set; }
        public int TotalTokenCount { get; set; }
        public OutputTokenDetails OutputTokenDetails { get; set; }
        public InputTokenDetails InputTokenDetails { get; set; }
    }

    public class OutputTokenDetails
    {
        public int ReasoningTokenCount { get; set; }
        public int AudioTokenCount { get; set; }
    }

    public class InputTokenDetails
    {
        public int AudioTokenCount { get; set; }
        public int CachedTokenCount { get; set; }
    }

    public class ChatContent
    {
        public int Kind { get; set; }
        public string Text { get; set; }
        public string? ImageUri { get; set; }
        public byte[]? ImageBytes { get; set; }
        public string? ImageBytesMediaType { get; set; }
        public string? ImageDetailLevel { get; set; }
        public object? Refusal { get; set; }
    }
}
