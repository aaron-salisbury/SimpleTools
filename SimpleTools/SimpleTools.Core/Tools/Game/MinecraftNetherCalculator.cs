using Serilog;
using SimpleTools.Core.Base;
using SimpleTools.Core.Base.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTools.Core.Tools.Game
{
    public class MinecraftNetherCalculator : ValidatableModel
    {
        private const decimal NETHER_FACTOR = 8M;

        private ILogger _logger { get; set; }

        private string _overworldX;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        public string OverworldX
        {
            get => _overworldX;
            set
            { 
                _overworldX = value; 
                RaisePropertyChanged(nameof(OverworldX));
                SetNetherCoordFromOverworld(value, nameof(OverworldX));
            }
        }

        private string _overworldY;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} should be greater than or equal to {1}.")]
        public string OverworldY
        {
            get => _overworldY;
            set
            {
                _overworldY = value;
                RaisePropertyChanged(nameof(OverworldY));
                if (!string.Equals(_netherY, _overworldY))
                {
                    NetherY = _overworldY;
                }
            }
        }

        private string _overworldZ;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        public string OverworldZ
        {
            get => _overworldZ;
            set
            {
                _overworldZ = value;
                RaisePropertyChanged(nameof(OverworldZ));
                SetNetherCoordFromOverworld(value, nameof(OverworldZ));
            }
        }

        private string _netherX;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        public string NetherX
        {
            get => _netherX;
            set
            {
                _netherX = value;
                RaisePropertyChanged(nameof(NetherX));
                SetOverworldCoordFromNether(value, nameof(NetherX));
            }
        }

        private string _netherY;
        [Range(0, int.MaxValue, ErrorMessage = "The {0} should be greater than or equal to {1}.")]
        public string NetherY
        {
            get => _netherY;
            set
            {
                _netherY = value;
                RaisePropertyChanged(nameof(NetherY));
                if (!string.Equals(_overworldY, _netherY))
                {
                    OverworldY = _netherY;
                }
            }
        }

        private string _netherZ;
        [StringIsInt(ErrorMessage = "{0} must be an integer.")]
        public string NetherZ
        {
            get => _netherZ;
            set
            {
                _netherZ = value;
                RaisePropertyChanged(nameof(NetherZ));
                SetOverworldCoordFromNether(value, nameof(NetherZ));
            }
        }

        public MinecraftNetherCalculator(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
        }

        private void SetNetherCoordFromOverworld(string overworldCoord, string propertyName)
        {
            try
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    string netherCoord = null;

                    if (int.TryParse(overworldCoord, out int overworldCoordInt))
                    {
                        decimal result = overworldCoordInt / NETHER_FACTOR;
                        netherCoord = Convert.ToInt32(Math.Round(result)).ToString();
                    }

                    if (propertyName.Equals(nameof(OverworldX)) && !string.Equals(NetherX, netherCoord))
                    {
                        NetherX = netherCoord;
                    }
                    else if (propertyName.Equals(nameof(OverworldZ)) && !string.Equals(NetherZ, netherCoord))
                    {
                        NetherZ = netherCoord;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to convert coordinate: {e.Message}");
            }

        }

        private void SetOverworldCoordFromNether(string netherCoord, string propertyName)
        {
            try
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    string overworldCoord = null;

                    if (int.TryParse(netherCoord, out int netherCoordInt))
                    {
                        decimal result = netherCoordInt * NETHER_FACTOR;
                        overworldCoord = Convert.ToInt32(Math.Round(result)).ToString();
                    }

                    if (propertyName.Equals(nameof(NetherX)) && !string.Equals(OverworldX, overworldCoord))
                    {
                        OverworldX = overworldCoord;
                    }
                    else if (propertyName.Equals(nameof(NetherZ)) && !string.Equals(OverworldZ, overworldCoord))
                    {
                        OverworldZ = overworldCoord;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Failed to convert coordinate: {e.Message}");
            }
        }
    }
}
