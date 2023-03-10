using UWPTestProject.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPTestProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CurrencyViewModel ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new CurrencyViewModel();
            this.ViewModel = this.DataContext as CurrencyViewModel;
        }
    }
}
