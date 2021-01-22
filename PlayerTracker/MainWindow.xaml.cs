/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Derived from https://www.codeproject.com/articles/380027/csharp-speech-to-text
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

#region using directives

using System;
using System.Speech.Recognition;
using System.Windows;
using Algorithm.FuzzyStrings;
using System.Linq;
using MahApps.Metro.Controls;

#endregion

namespace PlayerTracker
{
    /// <summary>
    /// MainWindow class
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region locals

        /// <summary>
        /// the engine
        /// </summary>
        SpeechRecognitionEngine speechRecognitionEngine = null;

        //Commands
        const String ClearPlayersCommand = "Clear Player List";
        const String AddPlayerCommand = "Track Player";

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // create the engine
                speechRecognitionEngine = createSpeechEngine();

                // hook to events
                speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(AudioLevelUpdatedHandler);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedHandler);
                speechRecognitionEngine.LoadGrammar(new DictationGrammar());

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Voice recognition failed");
            }
        }

        #endregion

        #region internal functions and methods

        /// <summary>
        /// Creates the speech engine.
        /// </summary>
        /// <returns></returns>
        private SpeechRecognitionEngine createSpeechEngine()
        {
            speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
            return speechRecognitionEngine;
        }

        /// <summary>
        /// Loads the grammar and commands.
        /// </summary>
        private void loadCommands()
        {
            try
            {
                speechRecognitionEngine.UnloadAllGrammars();

                Choices commands = new Choices();
                var players = txtPlayers.Text.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None
                );

                if (players.Length > 0 && !String.IsNullOrEmpty(players[0]))
                {
                    foreach (var player in players)
                    {
                        if (!String.IsNullOrEmpty(player))
                        {
                            commands.Add($"{AddPlayerCommand} {player.Trim()}");
                        }
                    }
                }
                else
                {
                    speechRecognitionEngine.LoadGrammar(new DictationGrammar());
                }

                commands.Add(ClearPlayersCommand);
                Grammar commandsList = new Grammar(new GrammarBuilder(commands));
                speechRecognitionEngine.LoadGrammar(commandsList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearPlayers()
        {
            txtSpoken.Text = String.Empty;
            CopyTextToFile();
        }

        #endregion

        #region speechEngine events

        /// <summary>
        /// Handles the SpeechRecognized event of the engine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Speech.Recognition.SpeechRecognizedEventArgs"/> instance containing the event data.</param>
        void SpeechRecognizedHandler(object sender, SpeechRecognizedEventArgs e)
        {
            var result = e.Result.Text;
            var words = result.Split(' ');

            if (words.Length > 2 && words[0].FuzzyEquals("Track") && words[1].FuzzyEquals("player"))
            {
                txtSpoken.Text += String.Join("", words.SubArray(2, words.Length - 2).Select(c => UppercaseString(c))) + Environment.NewLine;
                CopyTextToFile();
            }
            else if (result.FuzzyEquals(ClearPlayersCommand))
            {
                ClearPlayers();
            };
        }

        private string UppercaseString(string inputString)
        {
            return $"{char.ToUpper(inputString[0])}{inputString.Substring(1)}";
        }

        /// <summary>
        /// Copies the txtSpoken contents to a file
        /// </summary>
        void CopyTextToFile()
        {
            System.IO.File.WriteAllText($"{Environment.CurrentDirectory}\\TrackedPlayersList.txt", txtSpoken.Text);
        }

        /// <summary>
        /// Handles the AudioLevelUpdated event of the engine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Speech.Recognition.AudioLevelUpdatedEventArgs"/> instance containing the event data.</param>
        void AudioLevelUpdatedHandler(object sender, AudioLevelUpdatedEventArgs e)
        {
            prgLevel.Value = e.AudioLevel * 10;
        }

        #endregion

        #region window closing

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // unhook events
            speechRecognitionEngine.RecognizeAsyncStop();
            // clean references
            speechRecognitionEngine.Dispose();
            ClearPlayers();

        }

        #endregion

        #region GUI events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            ClearPlayers();
            loadCommands();
        }

        #endregion
    }
}
