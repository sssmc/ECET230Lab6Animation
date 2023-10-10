using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECET230Lab6Animation
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        readonly Color WatchBackgroundColor = Color.White;

        MicroGraphics graphics;
        int displayWidth, displayHeight;
        int hour, minute, tick;

        public override Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            var st7789 = new St7789
            (
                spiBus: Device.CreateSpiBus(),
                chipSelectPin: Device.Pins.D02,
                dcPin: Device.Pins.D01,
                resetPin: Device.Pins.D00,
                width: 240, height: 240
            );
            displayWidth = Convert.ToInt32(st7789.Width);
            displayHeight = Convert.ToInt32(st7789.Height);

            graphics = new MicroGraphics(st7789);
            graphics.Rotation = RotationType._270Degrees;

            onboardLed.SetColor(Color.Green);

            graphics.Clear(updateDisplay: true);

            return base.Initialize();
        }

        public override Task Run()
        {

            while (true)
            {
                

                Random random = new Random();

                Color randomColor = Color.FromRgb(random.Next(255), random.Next(255), random.Next(255));

                Circle circle = new Circle(graphics, 240, 240, randomColor, false); ;

                circle.setPosition(random.Next(240), random.Next(240));
                circle.setRadius(random.Next(3, 40));

                circle.draw();

                graphics.Show();

                Thread.Sleep(500);
            }


            return base.Run();
        }
    }
}