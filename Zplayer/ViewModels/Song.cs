using Avalonia.Media.Imaging;
using ManagedBass;
using TagLib;
using Picture = TagLib.Flac.Picture;
using System.IO;


namespace Zplayer.Models;

public class Song
{
    public string Source { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Album { get; set; }
    public double Duration { get; set; }
    public Bitmap Cover { get; set; }

    public Song(string source, string artist, string title, string album, double duration, Bitmap cover)
    {
        Source = source;
        Artist = artist;
        Title = title;
        Album = album;
        Duration = duration;
        Cover = cover;
    }

    public Song(string source)
    {
        Source = source;
        TagLib.File file = TagLib.File.Create(source);
        Artist = (!string.IsNullOrEmpty(file.Tag.Title)) ? file.Tag.Title : "Unknown";
        Title = (!string.IsNullOrEmpty(file.Tag.FirstPerformer)) ? file.Tag.FirstPerformer : "Unknown";
        Album = (!string.IsNullOrEmpty(file.Tag.Album)) ? file.Tag.Album : "Unknown";
        Duration = file.Properties.Duration.TotalSeconds;
        if (file.Tag.Pictures.Length>0)
        {
            var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
            MemoryStream ms = new MemoryStream(bin);
            ms.Seek(0, SeekOrigin.Begin);
            Cover= new Bitmap(ms);
        }
        else
        {
            Cover = new Bitmap("../../../../Zplayer/Assets/cover.jpg");
        }
    }
}