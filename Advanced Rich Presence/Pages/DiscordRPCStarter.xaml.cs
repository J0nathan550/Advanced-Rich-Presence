using Advanced_Rich_Presence.Classes;
using DiscordRPC;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Advanced_Rich_Presence.Pages;

public partial class DiscordRPCStarter : System.Windows.Controls.Page
{
    private DiscordRpcClient? client;
    private List<SymbolIcon> playIconSymbols = [];
    private int lastButtonIndex = -1;

    public DiscordRPCStarter()
    {
        InitializeComponent();
        CreateUI();
    }

    /// <summary>
    /// Opens 'Create Template' Page
    /// </summary>
    private void CreateTemplateButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (MainWindow.MainWindowInstance != null)
        {
            if (client != null && client.IsInitialized) client.Deinitialize();
            Helpers.discordStatusModel = new();
            Helpers.IsEditing = false;
            if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(CreateOrEditDiscordRPCStatus)) return;
            MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(CreateOrEditDiscordRPCStatus));
        }
    }

    /// <summary>
    /// Updates and creates UI in constructor. Using settings of the user loads specific data of the template.
    /// </summary>
    private void CreateUI()
    {
        SettingsClass.Settings = new SettingsClass();
        SettingsClass.LoadSettings();

        if (SettingsClass.Settings.DiscordStatusTemplates.Count < 1)
        {
            TextBlock noTemplatesBlock = new()
            {
                Text = "There are no templates right now...",
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = (FontFamily)FindResource("Montserrat"),
                FontWeight = FontWeights.Bold,
                FontSize = 18,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            templatePanel.Children.Add(noTemplatesBlock);
            return;
        }

        playIconSymbols = [];
        int index = 0; 
        foreach (var template in SettingsClass.Settings.DiscordStatusTemplates)
        {
            Border border = new()
            {
                Background = new SolidColorBrush(Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF)),
                CornerRadius = new CornerRadius(3),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            SettingsClass.Settings.DiscordStatusTemplates[index].TemplateID = index;

            Grid grid = new();

            ColumnDefinition colDef1 = new();
            ColumnDefinition colDef2 = new();
            ColumnDefinition colDef3 = new();
            ColumnDefinition colDef4 = new();
            colDef2.Width = GridLength.Auto;
            colDef3.Width = GridLength.Auto;
            colDef4.Width = GridLength.Auto;

            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);
            grid.ColumnDefinitions.Add(colDef3);
            grid.ColumnDefinitions.Add(colDef4);

            TextBlock textBlock = new()
            {
                Text = template.NameOfTemplate + " (" + template.ApplicationID + ")",
                FontFamily = (FontFamily)FindResource("Montserrat"),
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 0, 0, 0),
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextTrimming = TextTrimming.CharacterEllipsis
            };

            System.Windows.Controls.Button deleteButton = new()
            {
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            deleteButton.Click += async (s, e) =>
            {
                ContentDialog userDeleteQuestion = new()
                {
                    Title = "Are you sure you want to delete '" + template.NameOfTemplate + "'?",
                    Content = "Are you sure you want to remove this template?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete"
                };
                var result = await userDeleteQuestion.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    SettingsClass.Settings.DiscordStatusTemplates.Remove(template);
                    templatePanel.Children.Remove(border);
                    SettingsClass.SaveSettings();
                }
            };
            SymbolIcon symbolIcon1 = new()
            {
                Symbol = Symbol.Delete
            };
            deleteButton.Content = symbolIcon1;

            System.Windows.Controls.Button editButton = new()
            {
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            SymbolIcon symbolIcon2 = new()
            {
                Symbol = Symbol.Edit
            };
            editButton.Content = symbolIcon2;
            editButton.Click += (s, e) =>
            {
                if (MainWindow.MainWindowInstance != null)
                {
                    if (client != null && client.IsInitialized) client.Deinitialize();
                    Helpers.IsEditing = true;
                    Helpers.discordStatusModel = template;
                    if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(CreateOrEditDiscordRPCStatus)) return;
                    MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(CreateOrEditDiscordRPCStatus));
                }
            };

            System.Windows.Controls.Button playButton = new()
            {
                Name = $"p{index}",
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            SymbolIcon symbolIcon3 = new()
            {
                Symbol = Symbol.Play
            };
            playIconSymbols.Add(symbolIcon3);
            playButton.Content = symbolIcon3;
            playButton.Click += (s, e) =>
            {
                int thisIndex = int.Parse(playButton.Name[1..]);
                if (lastButtonIndex != thisIndex)
                {
                    if (lastButtonIndex != -1) playIconSymbols[lastButtonIndex].Symbol = Symbol.Play;
                    playIconSymbols[thisIndex].Symbol = Symbol.Pause;

                    DiscordStart(thisIndex);
                 
                    lastButtonIndex = thisIndex;
                }
                else
                {
                    if (playIconSymbols[thisIndex].Symbol == Symbol.Play)
                    {
                        DiscordStart(thisIndex);
                        playIconSymbols[thisIndex].Symbol = Symbol.Pause;
                    }
                    else if (playIconSymbols[thisIndex].Symbol == Symbol.Pause)
                    {
                        if (client != null && client.IsInitialized) client.Deinitialize();
                        playIconSymbols[thisIndex].Symbol = Symbol.Play;
                    }
                    lastButtonIndex = thisIndex;
                }
            };

            Grid.SetColumn(textBlock, 0);
            Grid.SetColumn(deleteButton, 1);
            Grid.SetColumn(editButton, 2);
            Grid.SetColumn(playButton, 3);

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);
            grid.Children.Add(editButton);
            grid.Children.Add(playButton);

            border.Child = grid;
            templatePanel.Children.Add(border);
            index++;
        }
    }

    private void DiscordStart(int thisIndex)
    {
        if (SettingsClass.Settings == null)
        {
            return;
        }
        if (client != null && client.IsInitialized) client.Deinitialize();
        client = new DiscordRpcClient(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].ApplicationID);
        client.Initialize();

        RichPresence richPresence = new();

        if (string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url) && string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url))
        {
            if (SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets == null) SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets = new Assets();

            richPresence = new RichPresence()
            {
                State = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].State,
                Details = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Details,
                Assets = new Assets()
                {
                    LargeImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageKey,
                    LargeImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageText,
                    SmallImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageKey,
                    SmallImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageText,
                },
                Timestamps = Timestamps.Now
            };
        }
        else if (!string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url) && !string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url) && string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Url) && string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Url))
        {
            if (SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets == null) SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets = new Assets();

            richPresence = new RichPresence()
            {
                State = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].State,
                Details = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Details,
                Assets = new Assets()
                {
                    LargeImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageKey,
                    LargeImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageText,
                    SmallImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageKey,
                    SmallImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageText,
                },
                Buttons =
                [
                    new DiscordRPC.Button()
                                {
                                    Label = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Label,
                                    Url = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url
                                }
                ],
                Timestamps = Timestamps.Now
            };
        }
        else if (!string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url) && !string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url) && !string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Url) && !string.IsNullOrEmpty(SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Url))
        {
            if (SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets == null) SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets = new Assets();

            richPresence = new RichPresence()
            {
                State = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].State,
                Details = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Details,
                Assets = new Assets()
                {
                    LargeImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageKey,
                    LargeImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.LargeImageText,
                    SmallImageKey = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageKey,
                    SmallImageText = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Assets.SmallImageText,
                },
                Buttons =
                [
                    new DiscordRPC.Button()
                                {
                                    Label = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Label,
                                    Url = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[0].Url
                                },
                                new DiscordRPC.Button()
                                {
                                    Label = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Label,
                                    Url = SettingsClass.Settings.DiscordStatusTemplates[thisIndex].Buttons[1].Url
                                }
                ],
                Timestamps = Timestamps.Now
            };
        }

        client.SetPresence(richPresence);
    }
}