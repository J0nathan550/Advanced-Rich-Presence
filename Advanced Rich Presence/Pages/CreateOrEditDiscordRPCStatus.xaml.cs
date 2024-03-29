﻿using Advanced_Rich_Presence.Classes;
using DiscordRPC;
using DiscordRPC.Exceptions;
using System.Windows.Controls;

namespace Advanced_Rich_Presence.Pages
{
    public partial class CreateOrEditDiscordRPCStatus : Page
    {
        private readonly DiscordRPC.Button[] discordButtons = [new DiscordRPC.Button() {}, new DiscordRPC.Button(){}]; // placeholder to parse data to DiscordButton.cs file. Maximum value of buttons is 2.
        private bool exceptionWasRaised = false; // boolean to represent if we showed and error message in program to not clear issue for the first time.

        public CreateOrEditDiscordRPCStatus()
        {
            InitializeComponent();
            SettingsClass.LoadSettings();
            LoadVisuals();
        }

        private void LoadVisuals()
        {
            if (Helpers.IsEditing) discordTemplateTextBlock.Text = "Editing of the Discord Status Template";
            else discordTemplateTextBlock.Text = "Creation of the Discord Status Template";
            nameOfTemplateTextBox.Text = Helpers.DiscordStatusModel.NameOfTemplate;
            applicationIDTextBox.Text = Helpers.DiscordStatusModel.ApplicationID;
            statusTextBox.Text = Helpers.DiscordStatusModel.State;
            detailsTextBox.Text = Helpers.DiscordStatusModel.Details;
            largeImageLinkTextBox.Text = Helpers.DiscordStatusModel.Assets?.LargeImageKey ?? string.Empty;
            largeImageTextTextBox.Text = Helpers.DiscordStatusModel.Assets?.LargeImageText ?? string.Empty;
            smallImageLinkTextBox.Text = Helpers.DiscordStatusModel.Assets?.SmallImageKey ?? string.Empty;
            smallImageTextTextBox.Text = Helpers.DiscordStatusModel.Assets?.SmallImageText ?? string.Empty;
            firstButtonLinkTextBox.Text = Helpers.DiscordStatusModel.Buttons[0].Url;
            firstButtonTextTextBox.Text = Helpers.DiscordStatusModel.Buttons[0].Label;
            secondButtonLinkTextBox.Text = Helpers.DiscordStatusModel.Buttons[1].Url;
            secondButtonTextTextBox.Text = Helpers.DiscordStatusModel.Buttons[1].Label;
        }

        /// <summary>
        /// Go's back to the template page. 
        /// </summary>
        private void CancelButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.MainWindowInstance != null)
            {
                if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(DiscordRPCStarter)) return;
                MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(DiscordRPCStarter));
            }
        }

        /// <summary>
        /// Grabs all of the data from the placeholder 'Helpers.discordStatusModel', if something is bad or null, it throws error in program
        /// That user should modify something before creating new template. The most vital part that is checking is buttons. 
        /// </summary>
        private void AcceptButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.NameOfTemplate))
            {
                errorParserMessage.Text = "You did not specify Name of Template for the Discord Status!";
                return;
            }

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.ApplicationID))
            {
                errorParserMessage.Text = "You did not specify Application ID for the Discord RPC!";
                return;
            }

            // if all buttons are null we are good to go. 
            if (string.IsNullOrEmpty(firstButtonLinkTextBox.Text) && string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Label) && string.IsNullOrEmpty(secondButtonLinkTextBox.Text) && string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Label))
            {
                SaveAndCloseTemplate();
                return;
            }

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Label))
            {
                errorParserMessage.Text = "You didn't fill the Label for the First Button!";
                return;
            }

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Url))
            {
                errorParserMessage.Text = "You did not fill the Url, or the Url is no valid for the First Button!";
                return;
            }

            // if first button filled but second is not we are good to go. 
            if (!string.IsNullOrEmpty(firstButtonLinkTextBox.Text) && !string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Label) && string.IsNullOrEmpty(secondButtonLinkTextBox.Text) && string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Label))
            {
                SaveAndCloseTemplate();
                return;
            }

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Label))
            {
                errorParserMessage.Text = "You didn't fill the Label for the Second Button!";
                return;
            }

            if (string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Url))
            {
                errorParserMessage.Text = "You did not fill the Url, or the Url is no valid for the Second Button!";
                return;
            }

            // if you filled out info for second but for first no, you are not good to go. 
            if (string.IsNullOrEmpty(firstButtonLinkTextBox.Text) && string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Label) && !string.IsNullOrEmpty(secondButtonLinkTextBox.Text) && !string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Label))
            {
                errorParserMessage.Text = "You should specify first paramaters for the First Button, and then for the Second Button!";
                return;
            }

            // if everything is filled we are good to go. 
            if (!string.IsNullOrEmpty(firstButtonLinkTextBox.Text) && !string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[0].Label) && !string.IsNullOrEmpty(secondButtonLinkTextBox.Text) && !string.IsNullOrEmpty(Helpers.DiscordStatusModel.Buttons[1].Label))
            {
                SaveAndCloseTemplate();
                return;
            }

        }

        private static void SaveAndCloseTemplate()
        {
            if (SettingsClass.Settings == null)
            {
                SettingsClass.Settings = new SettingsClass();
                SettingsClass.LoadSettings();
            }

            if (Helpers.IsEditing) SettingsClass.Settings.DiscordStatusTemplates[Helpers.DiscordStatusModel.TemplateID] = Helpers.DiscordStatusModel;
            else SettingsClass.Settings.DiscordStatusTemplates.Add(Helpers.DiscordStatusModel);

            SettingsClass.SaveSettings();
            if (MainWindow.MainWindowInstance != null)
            {
                if (MainWindow.MainWindowInstance.mainPageView.CurrentSourcePageType == typeof(DiscordRPCStarter)) return;
                MainWindow.MainWindowInstance.mainPageView.Navigate(typeof(DiscordRPCStarter));
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void NameOfTemplate_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.NameOfTemplate = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = string.Empty;
                Helpers.DiscordStatusModel.NameOfTemplate = sender.Text;
                errorParserMessage.Text = "Name of Tempalte text is too big!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void ApplicationID_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.ApplicationID = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = string.Empty;
                Helpers.DiscordStatusModel.ApplicationID = sender.Text;
                errorParserMessage.Text = "Application ID text is too big!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void StateStatus_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.State = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.State = sender.Text;
                errorParserMessage.Text = "Maximum State length can be 128 bytes!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void DetailsStatus_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.Details = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Details = sender.Text;
                errorParserMessage.Text = "Maximum Details length can be 128 bytes!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void LargeImageLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.Assets ??= new Assets();
                Helpers.DiscordStatusModel.Assets.LargeImageKey = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Assets.LargeImageKey = sender.Text;
                errorParserMessage.Text = "Maximum Large Image Link length can be 256 bytes!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void LargeImageText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.Assets ??= new Assets();
                Helpers.DiscordStatusModel.Assets.LargeImageText = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Assets.LargeImageText = sender.Text;
                errorParserMessage.Text = "Maximum Large Image Text length can be 256 bytes!";
                exceptionWasRaised = true;
            }

        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void SmallImageLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.Assets ??= new Assets();
                Helpers.DiscordStatusModel.Assets.SmallImageKey = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Assets.SmallImageKey = sender.Text;
                errorParserMessage.Text = "Maximum Small Image Link length can be 256 bytes!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void SmallImageText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                Helpers.DiscordStatusModel.Assets ??= new Assets();
                Helpers.DiscordStatusModel.Assets.SmallImageText = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Assets.SmallImageText = sender.Text;
                errorParserMessage.Text = "Maximum Small Image Text length can be 256 bytes!";
                exceptionWasRaised = true;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void FirstButtonText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                discordButtons[0].Label = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
                Helpers.DiscordStatusModel.Buttons[0].Label = discordButtons[0].Label;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                discordButtons[0].Label = sender.Text;
                errorParserMessage.Text = "Maximum First Button Text length can be 32 bytes!";
                exceptionWasRaised = true;
                Helpers.DiscordStatusModel.Buttons[0].Label = discordButtons[0].Label;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void FirstButtonLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                discordButtons[0].Url = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
                Helpers.DiscordStatusModel.Buttons[0].Url = discordButtons[0].Url;
            }
            catch(ArgumentException)
            {
                Helpers.DiscordStatusModel.Buttons[0].Url = string.Empty;
                errorParserMessage.Text = "The Url you specified to First Button, must be valid!";
                exceptionWasRaised = true;
            }
            catch(StringOutOfRangeException)
            {
                sender.Text = sender.Text[^1..];
                Helpers.DiscordStatusModel.Buttons[0].Url = sender.Text;
                discordButtons[0].Url = sender.Text;
                errorParserMessage.Text = "Maximum First Button Url length can be 512 bytes!";
                exceptionWasRaised = true;
                Helpers.DiscordStatusModel.Buttons[0].Url = discordButtons[0].Url;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void SecondButtonText_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                discordButtons[1].Label = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
                Helpers.DiscordStatusModel.Buttons[1].Label = discordButtons[1].Label;
            }
            catch
            {
                sender.Text = sender.Text[^1..];
                discordButtons[1].Label = sender.Text;
                errorParserMessage.Text = "Maximum Second Button Text length can be 32 bytes!";
                exceptionWasRaised = true;
                Helpers.DiscordStatusModel.Buttons[1].Label = discordButtons[1].Label;
            }
        }

        /// <summary>
        /// Updates text, and gives errors if something bad is happening while you are typing text.
        /// </summary>
        private void SecondButtonLink_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            try
            {
                discordButtons[1].Url = sender.Text;
                if (!exceptionWasRaised) errorParserMessage.Text = string.Empty;
                exceptionWasRaised = false;
                Helpers.DiscordStatusModel.Buttons[1].Url = sender.Text;
            }
            catch (ArgumentException)
            {
                errorParserMessage.Text = "The Url you specified to Second Button, must be valid!";
                exceptionWasRaised = true;
            }
            catch (StringOutOfRangeException)
            {
                sender.Text = sender.Text[^1..];
                discordButtons[1].Url = sender.Text;
                errorParserMessage.Text = "Maximum Second Button Url length can be 512 bytes!";
                exceptionWasRaised = true;
                Helpers.DiscordStatusModel.Buttons[1].Url = discordButtons[1].Url;
            }
        }
    }
}