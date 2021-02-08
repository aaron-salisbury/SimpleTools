using GalaSoft.MvvmLight;
using SimpleTools.App.Views;
using System;
using System.Collections.Generic;

namespace SimpleTools.App.ViewModels
{
    public class GameToolsViewModel : ViewModelBase
    {
        public readonly List<(string Content, Type Page)> ToolPages = new List<(string Content, Type Page)>
        {
            ("Dice Roller", typeof(DiceRollerPage)),
            ("Minecraft Nether Calculator", typeof(MinecraftNetherCalculatorPage)),
            //("Minecraft Seed Map", typeof(MinecraftSeedMapPage))
        };
    }
}
