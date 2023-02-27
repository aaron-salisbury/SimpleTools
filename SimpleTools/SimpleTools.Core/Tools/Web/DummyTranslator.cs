using SimpleTools.Core.Base;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Tools.Web
{
    public class DummyTranslator : ValidatableModel
    {
        private string _phrase;
        [Required]
        public string Phrase
        {
            get => _phrase;
            set { _phrase = value; RaisePropertyChanged(); }
        }

        private string _translatedPhrase;
        public string TranslatedPhrase
        {
            get => _translatedPhrase;
            set { _translatedPhrase = value; RaisePropertyChanged(); }
        }

        public void Translate()
        {
            if (!string.IsNullOrEmpty(Phrase))
            {
                string translatedPhrase = string.Empty;
                bool lastWasLowered = false;

                for (int i = 0; i < Phrase.Length; i++)
                {
                    string character = Phrase[i].ToString();

                    if (string.IsNullOrWhiteSpace(character))
                    {
                        translatedPhrase += character;
                    }
                    else
                    {
                        string translatedLetter = lastWasLowered ? character.ToUpper() : character.ToLower();
                        translatedPhrase += translatedLetter;
                        lastWasLowered = !lastWasLowered;
                    }
                }

                TranslatedPhrase = translatedPhrase;
            }
            else
            {
                TranslatedPhrase = null;
            }
        }
    }
}
