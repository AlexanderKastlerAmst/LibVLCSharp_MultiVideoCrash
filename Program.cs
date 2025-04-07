using System.Windows;
using System.Windows.Controls;

namespace LibVLCSharp_MultiVideoCrash;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        App app = new ();
        app.Run();
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
    public MainWindow()
    {
        Button button = new()
        {
            Content = "Set height to zero"
        };
        button.Click += (_ ,_) => Height = 0; 
        
        StackPanel stackPanel = new();
        stackPanel.Children.Add(button);
        
        Content = stackPanel;
    }
}