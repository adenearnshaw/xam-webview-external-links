using System.ComponentModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace PrivacyPolicyLinks
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewmodel();
        }
    }

    public class MainPageViewmodel
    {
        public string DocumentText { get; }

        public MainPageViewmodel()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName =$"{nameof(PrivacyPolicyLinks)}.Resources.Index.html";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                DocumentText = reader.ReadToEnd();
            }
        }
    }
}
