using System;

namespace SimpleTools.App.ViewModels
{
    public class MinecraftSeedMapViewModel : BaseViewModel
    {
        public const string SEED_MAP_URL = "https://www.chunkbase.com/apps/seed-map";

        private string _seedMapUrl;
        public string SeedMapUrl
        {
            get => _seedMapUrl;
            set => Set(ref _seedMapUrl, value);
        }

        public MinecraftSeedMapViewModel()
        {
            SeedMapUrl = SEED_MAP_URL;
        }
    }
}
