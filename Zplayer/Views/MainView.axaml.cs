using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using GLib;
using Gst;
using ManagedBass;
using TagLib;
using Zplayer.Models;
using Picture = TagLib.Flac.Picture;
using Zplayer.ViewModels;

namespace Zplayer.Views;


public partial class MainView : UserControl
{
    private string sciezka,poprzedniaSciezka;
    private string[] result;
    private int handle;
    private double volumePercent;
    private Song songHandler;
    public MainView()
    {
        InitializeComponent();
    }

    public async void otworz(object? sender, RoutedEventArgs e)
    {
        Bass.Init();
        Bass.StreamFree(handle);
        Bass.MusicFree(handle);
        var window = this.GetVisualRoot() as Window;
        var dialog = new OpenFileDialog();
        dialog.Title = "Open mp3 file";
        result = await dialog.ShowAsync(window);
        if (result.Length < 1)
        {
            return;
        }
        else
        {
            Console.Write(result[0]);
            sciezka = result[0];
            TagLib.File plik = TagLib.File.Create(result[0]);
            handle = Bass.CreateStream(sciezka);

            songHandler = new Song(sciezka);

            title.Text = songHandler.Title;
            Album.Text = songHandler.Album;
            artist.Text = songHandler.Artist;
            cover.Source = songHandler.Cover;
            timeSlider.Maximum = songHandler.Duration;
        }
    }

    public void play(object? sender, RoutedEventArgs e)
    {
        if (handle == 0 || poprzedniaSciezka != sciezka)
        {
            poprzedniaSciezka = sciezka;
        }
        Bass.ChannelPlay(handle);
    }
    public void pause(object? sender, RoutedEventArgs e)
    {
        Bass.ChannelPause(handle);
    }

    public void nextSong(object? sender, RoutedEventArgs e)
    {
        
    }
    public void prevSong(object? sender, RoutedEventArgs e)
    {
        
    }
}