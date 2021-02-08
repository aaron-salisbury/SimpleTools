using SimpleTools.App.Base.Helpers;
using SimpleTools.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimpleTools.App.Views
{
    public sealed partial class DiceRollerPage : Page
    {
        private DiceRollerViewModel ViewModel
        {
            get => ViewModelLocator.Current.DiceRollerViewModel;
        }

        public DiceRollerPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void ModifierTextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = false;
            }
            else
            {
                bool shouldCancelForNonNumsOrMinus = args.NewText.Any(c => !char.IsDigit(c) && c != '-');
                bool shouldCancelForMinusInMiddle = !string.IsNullOrEmpty(sender.Text) && args.NewText.Last().Equals('-');

                args.Cancel = shouldCancelForNonNumsOrMinus || shouldCancelForMinusInMiddle;
            }
        }

        private void DiceListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedDie = (sender as ListView).SelectedItem;
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedDie = (sender as Button).DataContext; // In case they clicked the button without first selecting the item.
            ViewModel.DeleteDie();
        }

        private void BtnRoll_OnClick(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn)
            {
                PlayDiceRollSound();
            }

            RollDiceImages();
        }

        private void PlayDiceRollSound()
        {
            //TODO: Some bluetooth audio devices do not maintain constant connection, so sound will play after a long delay.
            //      It would be worth it to check for a good connection before even offering the toggle to turn sound on.
            //      That, or there may be a way to configure the app to establish and maintain that connection on launch, even without active audio.

            MediaPlayer diceRollMediaPlayer = new MediaPlayer
            {
                AudioCategory = MediaPlayerAudioCategory.Media,
                Source = Windows.Media.Core.MediaSource.CreateFromUri(
                    new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Sounds/Dice-Roll-Sound.wav")))
            };

            diceRollMediaPlayer.Play();
        }

        private void RollDiceImages()
        {
            List<Image> dieImages = new List<Image>();
            ElementFinder.FindChildren(dieImages, this);

            foreach (Image dieImage in dieImages.Where(img => Equals("DieImage", img.Tag)))
            {
                if (float.MaxValue - 360 < dieImage.Rotation)
                {
                    dieImage.Rotation = 0;
                }
                else
                {
                    dieImage.Rotation += 360;
                }
            }
        }
    }
}
