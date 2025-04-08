using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LibVLCSharp.Shared;
using LibVLCSharp.WPF;

namespace LibVLCSharp_MultiVideoCrash;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        try
        {
            Core.Initialize(@"c:\Program Files\VideoLAN\VLC\");

            App app = new();
            app.Run();
        }
        catch (Exception e)
        {
            MessageBox.Show($"{e.Message}{Environment.NewLine}{e.StackTrace}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

public class App
    : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow mainWindow = new();
        mainWindow.Show();
    }
}

public class MainWindow
    : Window
{
    private readonly LibVLC _libVlc = new ();
    private readonly VideoView[] _videoViews;
    
    public MainWindow()
    {   
        Button button = new()
        {
            Content = "Set height to zero"
        };
        button.Click += (_ ,_) => Height = 0;

        _videoViews =
        [
            new VideoView(),
            new VideoView()
        ];

        UniformGrid grid = new()
        {
            Columns = _videoViews.Length,
            Rows = 2
        };
        ForAllVideoViews(view => grid.Children.Add(view));
        grid.Children.Add(button);
        
        Content = grid;

        Loaded += (_, _) =>
        {
            ForAllVideoViews(view => view.MediaPlayer = new MediaPlayer(_libVlc));
            ForAllVideoViews(view => view.MediaPlayer!.Play(new Media(_libVlc, @"c:\home\Videos\sol5_1920_av01.mkv")));
        };
    }
    
    private void ForAllVideoViews(Action<VideoView> action)
    {
        foreach (VideoView videoView in _videoViews)
        {
            action(videoView);
        }
    }
}