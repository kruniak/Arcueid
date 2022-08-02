using MimeMapping;

namespace Arcueid.Server.Core;

public record StreamFileContent(Stream Content, string ContentType, string? DownloadFileName = null);

public record ByteArrayFileContent(byte[] Content, string ContentType, string? DownloadFileName = null);

public record UploadStreamFileContent(Stream Content, string? FileName, int Length, string? Description = null)
    : StreamFileContent(Content, MimeUtility.GetMimeMapping(FileName), FileName);

public record UploadByteArrayFileContent(byte[] Content, string? FileName, string? Description = null)
    : ByteArrayFileContent(Content, MimeUtility.GetMimeMapping(FileName), FileName)
{
    public long Length => Content.Length;
}
