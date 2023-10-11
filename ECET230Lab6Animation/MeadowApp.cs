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
        //Main Graphics Object
        MicroGraphics graphics;

        public override Task Initialize()
        {
            //Init onboard LED
            var onboardLed = new RgbPwmLed(
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            //Init TFT Display
            var st7789 = new St7789
            (
                spiBus: Device.CreateSpiBus(),
                chipSelectPin: Device.Pins.D02,
                dcPin: Device.Pins.D01,
                resetPin: Device.Pins.D00,
                width: 240, height: 240
            );

            //Get Display Size from Display
            int displayWidth = Convert.ToInt32(st7789.Width);
            int displayHeight = Convert.ToInt32(st7789.Height);

            //Create Grapahics Object
            graphics = new MicroGraphics(st7789);

            //Set Display Rotation=
            graphics.Rotation = RotationType._270Degrees;

            //Clear the display
            graphics.Clear(updateDisplay: true);

            //Set the onboard LED to green
            onboardLed.SetColor(Color.Green);

            return base.Initialize();
        }

        //Run the main animation
        public void RunAnimation()
        {
            //Datetime object for the FPS counter
            DateTime time = DateTime.Now;

            //Font for FPS counter
            graphics.CurrentFont = new Font12x16();

            //Random number generator
            Random random = new Random();

            //The number of circles to display
            const int numOfCircles = 15;

            //Array of all circles
            Circle[] circles = new Circle[numOfCircles];


            //Create the given number of circles
            for (int i = 0; i < numOfCircles; i++)
            {
                //Random RGB color for
                Color randomColor = Color.FromRgb(random.Next(255), random.Next(255), random.Next(255));

                //Create a new circle using the random color
                circles[i] = new Circle(graphics, 240, 240, randomColor, true);

                //Center the circle
                circles[i].positionX = 120;
                circles[i].positionY = 120;

                //Set the radius to a random color
                circles[i].radius = random.Next(3, 20);

                //Set to a random speed
                circles[i].speedY = random.Next(5, 15);
                circles[i].speedX = random.Next(5, 15);
            }

            //Main loop
            while (true)
            {
                //Get the current time
                time = DateTime.Now;

                //Clear the display
                graphics.Clear();

                //Draw every circle in the array
                foreach (Circle circle in circles)
                {
                    circle.draw();
                }

                //Get the number of milliseconds since the start of the loop
                TimeSpan timeDiff = DateTime.Now - time;
                double timeDiffMilliseconds = timeDiff.TotalMilliseconds;

                //Convet the milliseconds to FPS and draw it on the screen
                graphics.DrawText(0, 0, $"FPS:{1.0 / (timeDiffMilliseconds / 1000.0):F1}");

                //Update the screen
                graphics.Show();

            }
        }

        public override Task Run()
        {

            //Animate
            RunAnimation();

            return base.Run();
        }
    }
}