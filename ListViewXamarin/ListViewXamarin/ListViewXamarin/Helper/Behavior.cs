using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.XForms.Themes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;
        Button ThemeButton;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ListView.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Contacts);
                    return item.ContactName[0].ToString();
                }
            });

            ThemeButton = bindable.FindByName<Button>("ThemeButton");
            ThemeButton.Clicked += ThemeButton_Clicked;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ThemeButton.Clicked -= ThemeButton_Clicked;
            ThemeButton = null;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBack
        private void ThemeButton_Clicked(object sender, EventArgs e)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            var darkTheme = mergedDictionaries.OfType<DarkTheme>().FirstOrDefault();
            if (darkTheme != null)
            {
                mergedDictionaries.Remove(darkTheme);
                mergedDictionaries.Add(new LightTheme());
                var groupHeaderColor = mergedDictionaries.OfType<ResourceDictionary>().FirstOrDefault();
                groupHeaderColor["GroupHeaderBackgroundColor"] = Color.LightGray;
            }
            else
            {
                mergedDictionaries.Remove(mergedDictionaries.OfType<LightTheme>().FirstOrDefault());
                mergedDictionaries.Add(new DarkTheme());
                var groupHeaderColor = mergedDictionaries.OfType<ResourceDictionary>().FirstOrDefault();
                groupHeaderColor["GroupHeaderBackgroundColor"] = Color.Red;
            }
        }
        #endregion
    }
}