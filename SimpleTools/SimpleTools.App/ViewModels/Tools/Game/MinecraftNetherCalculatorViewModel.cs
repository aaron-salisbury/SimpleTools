using SimpleTools.Core.Tools.Game;

namespace SimpleTools.App.ViewModels
{
    public class MinecraftNetherCalculatorViewModel : BaseViewModel
    {
        public MinecraftNetherCalculator MinecraftNetherCalculator { get; set; }

        public MinecraftNetherCalculatorViewModel()
        {
            MinecraftNetherCalculator = new MinecraftNetherCalculator(AppLogger);
        }
    }
}
