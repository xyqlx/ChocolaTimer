using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChocolaTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            if(culture.TwoLetterISOLanguageName == "zh")
            {
                var langResource = LoadComponent(new Uri(@"lang\Chinese.xaml", UriKind.Relative)) as ResourceDictionary;
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(langResource);
            }
            base.OnStartup(e);
        }
    }
}
