using System.Diagnostics;

namespace OwnersManual.Features.Configuration
{
    [DebuggerDisplay("{" + nameof(Title) + "}")]
    public class DocumentEntry
    {
        public DocumentEntry(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; }
        public string Content { get; }
    }
}