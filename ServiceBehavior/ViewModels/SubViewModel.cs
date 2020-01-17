using ServiceBehavior.Abstractions;
using ServiceBehavior.Abstractions.Constants;
using System;

namespace ServiceBehavior.ViewModels
{
    public class SubViewModel : BaseViewModel
    {
        public SubViewModel() : base()
        {
            InitializeCommands();
        }

        public SubViewModel(IServiceContainer serviceContainer) : base(serviceContainer)
        {
            InitializeCommands();
        }

        public IBackgroundColorService BackgroundColorService
        {
            get => GetService<IBackgroundColorService>();
        }

        public IBackgroundColorService BackgroundColorServiceWithKey
        {
            get => GetService<IBackgroundColorService>(ServiceKeyConstants.BackgroundColorService);
        }

        public IWPFCommand ChangeBackgroundCommand { get; set; }
        public IWPFCommand ChangeBackgroundWithKeyCommand { get; set; }

        private void InitializeCommands()
        {
            ChangeBackgroundCommand = CommandFactory.Create(() =>
            {
                BackgroundColorService.SetBackground(GetRandomBlueHex());
            });

            ChangeBackgroundWithKeyCommand = CommandFactory.Create(() =>
            {
                BackgroundColorServiceWithKey.SetBackground(GetRandomRedHex());
            });
        }

        #region RandomHexGenerator

        private int _randomByteMaxValue = Byte.MaxValue / 2;
        private string GetRandomBlueHex()
        {
            string red = GetRandomByte(Byte.MinValue, _randomByteMaxValue).ToString("X2");
            string green = GetRandomByte(Byte.MinValue, _randomByteMaxValue).ToString("X2");
            string blue = Byte.MaxValue.ToString("X2");
            return $"#{red}{green}{blue}";
        }

        private string GetRandomRedHex()
        {
            string red = Byte.MaxValue.ToString("X2");
            string green = GetRandomByte(Byte.MinValue, _randomByteMaxValue).ToString("X2");
            string blue = GetRandomByte(Byte.MinValue, _randomByteMaxValue).ToString("X2");
            return $"#{red}{green}{blue}";
        }

        private Random _rnd;

        private byte GetRandomByte(int minValue, int maxValue)
        {
            if (_rnd == null)
            {
                _rnd = new Random(DateTime.Now.Millisecond);
            }

            int number = _rnd.Next(Math.Max(minValue, Byte.MinValue), Math.Min(maxValue, Byte.MaxValue));
            return (byte)number;
        }

        #endregion
    }
}