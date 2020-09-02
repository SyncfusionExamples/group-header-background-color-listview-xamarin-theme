# How to change GroupHeader color based on theme in Xamarin.Forms ListView (SfListView)

The Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview) allows you to apply light and dark [theme](https://help.syncfusion.com/xamarin/themes/themes) using resource keys. You can find all the keys for SfListView control in this [documentation](https://help.syncfusion.com/xamarin/themes/keys#sflistview). The [ListViewItem](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html) background will be updated based on theme by default. For [GroupHeaderItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GroupHeaderItem.html), you can change the [BackgroundColor](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.backgroundcolor) using custom resource dictionary.

You can also refer the following article.

https://www.syncfusion.com/kb/11917/how-to-change-groupheader-color-based-on-theme-in-xamarin-forms-listview-sflistview

**XAML**

``` xml
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:syncTheme="clr-namespace:Syncfusion.XForms.Themes;assembly=Syncfusion.Core.XForms"
             x:Class="ListViewXamarin.App">
    <Application.Resources>
        <syncTheme:SyncfusionThemeDictionary>
            <syncTheme:SyncfusionThemeDictionary.MergedDictionaries>
                <!-- Theme resource dictionary -->
                <syncTheme:DarkTheme />
                <ResourceDictionary>
                    <Color x:Key="GroupHeaderBackgroundColor">Red</Color>
                </ResourceDictionary>
            </syncTheme:SyncfusionThemeDictionary.MergedDictionaries>
        </syncTheme:SyncfusionThemeDictionary>
    </Application.Resources>
</Application>
```
**XAML**

To update the ListViewItem text color, bind **SfListViewForegroundColor** key to [Label.TextColor](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.label.textcolor#Xamarin_Forms_Label_TextColor) property. For [GroupHeaderItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.GroupHeaderItem.html), bind **BackgroundColor** of the element loaded in the [GroupdHeaderTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~GroupHeaderTemplate.html) with the custom group header resource key. Also, set the group header text color with **SfListViewGroupHeaderForegroundColor** key.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    
    <ContentPage.Behaviors>
        <local:Behavior/>
    </ContentPage.Behaviors>

    <ContentPage.Content>
        <StackLayout>
            <Button Text="Change theme" x:Name="ThemeButton"/>
            <syncfusion:SfListView x:Name="listView" ItemSize="60" ItemsSource="{Binding ContactsInfo}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <Grid x:Name="grid">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="70" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                            <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center">
                                <Label LineBreakMode="NoWrap" TextColor="{DynamicResource SfListViewForegroundColor}" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="{DynamicResource SfListViewForegroundColor}" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
                <syncfusion:SfListView.GroupHeaderTemplate>
                    <DataTemplate>
                        <StackLayout BackgroundColor="{DynamicResource GroupHeaderBackgroundColor}" >
                            <Label Text="{Binding Key}" FontSize="22" TextColor="{DynamicResource SfListViewGroupHeaderForegroundColor}" FontAttributes="Bold" VerticalOptions="Center" HorizontalOptions="Start" Margin="20,0,0,0" />
                        </StackLayout>
                    </DataTemplate>
                </syncfusion:SfListView.GroupHeaderTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**C#**

In the [Button.Clicked](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.button.clicked) event, change the theme by removing the resource dictionary from the **MergedResourceDictionary** and add the new theme. Also, change the group header resource key value based on theme.

``` c#
namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
        Button ThemeButton;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            ThemeButton = bindable.FindByName<Button>("ThemeButton");
            ThemeButton.Clicked += ThemeButton_Clicked;

            base.OnAttachedTo(bindable);
        }

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
    }
}
```

**Output**

![GroupHeaderBackgroundColorTheme](https://github.com/SyncfusionExamples/group-header-background-color-listview-xamarin-theme/blob/master/ScreenShot/GroupHeaderBackgroundColorTheme.gif)
