using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone_Demo.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void speechButton_Click(object sender, RoutedEventArgs e)
        {
            string region = "eastus";
            string key = "dd3c13c218d54876a99a7f78bc0184cb";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using(var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
				using (var recogniser = new SpeechRecognizer(speechConfig, audioConfig))
				{
                    var result = await recogniser.RecognizeOnceAsync();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
				}
			}
            
		}

		private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
            int ammountCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;
            statusTextBlock.Text = $"Document Length: {ammountCharacters} characters";
        }

		private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
            var selectedWeignt = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldButton.IsChecked = (selectedWeignt != DependencyProperty.UnsetValue) && (selectedWeignt.Equals(FontWeights.Bold));

			var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
			italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

			var selectedDecorations = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			underlinebutton.IsChecked = (selectedDecorations != DependencyProperty.UnsetValue) && (selectedDecorations.Equals(TextDecorations.Underline));
		}

		private void boldButton_Click(object sender, RoutedEventArgs e)
		{
			bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

			if (isButtonChecked)
			{
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
			}
			else
			{
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
			}

		}

		private void italicButton_Click(object sender, RoutedEventArgs e)
		{
			bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

			if (isButtonEnabled)
			{
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
			}
			else
			{
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontStyles.Normal);
			}
		}
		private void underlinebutton_Click(object sender, RoutedEventArgs e)
		{
			bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

			if (isButtonEnabled)
			{
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
			}
			else
			{
				TextDecorationCollection textDecorations;
				(contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
				contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
			}
		}
	}
}
