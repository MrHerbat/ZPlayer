using CommunityToolkit.Mvvm.ComponentModel;

namespace Zplayer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static int _currentTime;

    public int CurrentTime
    {
        get => _currentTime;
        set => SetProperty(ref _currentTime, value);
    }
}
