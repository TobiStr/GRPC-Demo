using GRPCDemo.Client;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GRPCDemo.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDemoClient demoClient;

        public ObservableCollection<string> Events { get; } = new ObservableCollection<string>();

        public string EchoMessage { get; set; } = "";

        public MainWindow(DemoClientFactory demoClientFactory)
        {
            if (demoClientFactory is null) throw new System.ArgumentNullException(nameof(demoClientFactory));

            InitializeComponent();
            demoClient = demoClientFactory.GetService();
        }

        private async void btnEcho_Click(object sender, RoutedEventArgs e)
            => Events.Add($"Server returned: '{await demoClient.Echo(EchoMessage)}'");

        private async void btnGetFile_Click(object sender, RoutedEventArgs e)
            => imgBox.Source = ByteToImage((await demoClient.GetFile("")).ByteContent);
        
        ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
