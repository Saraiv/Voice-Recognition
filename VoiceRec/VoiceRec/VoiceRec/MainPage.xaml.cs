using System;
using Xamarin.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace VoiceRec
{
    public partial class MainPage : ContentPage
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();

        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnRecordClick(object sender, EventArgs e)
        {
            BtnRecord.IsEnabled = false;
            BtnStopRecord.IsEnabled = true;
            clist.Add(new string[] { "Hello", "Good Morning", "Welcome", "Thank you" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch(Exception ex)
            {
                DisplayAlert(ex.Message, "Error", "OK!");
            }
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            LabelText.Text += e.Result.ToString() + Environment.NewLine;
        }

        private void BtnStopRecordingClick(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            BtnRecord.IsEnabled = true;
            BtnStopRecord.IsEnabled = false;
        }
    }
}
