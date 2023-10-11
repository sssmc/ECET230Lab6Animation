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

            DateTime time = DateTime.Now;

            Random random = new Random();

            int numOfBalls = 150;

            Circle[] circles = new Circle[numOfBalls];

            for(int i = 0; i < numOfBalls; i++)
            {
                graphics.CurrentFont = new Font12x16();

                Color randomColor = Color.FromRgb(random.Next(255), random.Next(255), random.Next(255));

                circles[i] = new Circle(graphics, 240, 240, randomColor, false);

                circles[i].setPosition(120, 120);

                circles[i].setRadius(random.Next(3,20));

                circles[i].setSpeed(random.Next(5,15), random.Next(5, 15));
            }

            while (true)
            {
                time = DateTime.Now;

                graphics.Clear();

                foreach (Circle circle in circles)
                {
                    circle.draw();
                }

                TimeSpan timeDiff = DateTime.Now - time;
                double timeDiffMilliseconds = timeDiff.TotalMilliseconds;
                //int fps = (1 / (timeDiffMilliseconds / 1000));

                graphics.DrawText(0, 0, $"FPS:{1.0 / (timeDiffMilliseconds / 1000.0):F1}");

                graphics.Show();
                
            }


            return base.Run();
        }
    }
}