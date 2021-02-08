using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Base.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Tools.Dev
{
    public class UUIDGenerator : ValidatableModel
    {
        private ILogger _logger { get; set; }

        private bool _capitalize;
        public bool Capitalize
        {
            get => _capitalize;
            set { _capitalize = value; RaisePropertyChanged(nameof(Capitalize)); }
        }

        private string _uUID;
        [StringLength(36, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 36)]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "The {0} cannot have spaces.")]
        [LettersNumbersDashes(ErrorMessage = "The {0} may only contain letters, numbers, and dashes.")]
        public string UUID
        {
            get => _uUID;
            set { _uUID = value; RaisePropertyChanged(nameof(UUID)); }
        }

        public UUIDGenerator(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
        }

        public bool Initiate()
        {
            try
            {
                _logger.Information("Beginning to generate new UUID.");

                Guid newGuid = Guid.NewGuid();

                UUID = Capitalize
                    ? newGuid.ToString().ToUpper()
                    : newGuid.ToString();

                _logger.Information($"Generated new UUID, {UUID}");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }
    }
}
