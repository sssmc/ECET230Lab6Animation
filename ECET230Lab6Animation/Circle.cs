using System;
using Meadow.Foundation.Graphics;
using Meadow.Foundation;

namespace ECET230Lab6Animation
{
	public class Circle
	{

		public readonly MicroGraphics graphics;

		public int displayWidth { get; set; }
		public int displayHeight { get; set; }

		public int radius { get; set; }
		public int positionX { get; set; }
		public int positionY { get; set; }

		public bool filled { get; set; }

		public int speedX { get; set; }
		public int speedY { get; set; }

		public Color color { get; set; }

		public Circle(
			MicroGraphics graphics,
			int displayWidth,
			int displayHeight,
			Color color,
			bool filled)
		{
			this.graphics = graphics;
			this.displayWidth = displayWidth;
			this.displayHeight = displayHeight;
			this.filled = filled;
			this.color = color ;
			positionX = 0;
			positionY = 0;
			speedX = 0;
			speedY = 0;
		}

		private void updatePosition()
		{
			//Move the circle in x and y depending on the x and y speeds
			positionX += speedX;
			positionY += speedY;

			//Check if the circle is outside the display
			if((positionX >= (displayWidth - radius)) || (positionX <= (0 + radius)))
			{
				//Reverse the speed
				speedX *= -1;

			}
            if ((positionY >= (displayHeight - radius)) || (positionY <= (0 + radius)))
            {
                speedY *= -1;

            }
        }

		public void draw()
		{
			//Update the circle's position
			updatePosition();

			//Draw the Circle
            graphics.DrawCircle
            (
                centerX: positionX,
                centerY: positionY,
                radius: radius,
                color: color,
				filled: filled
            );
        }
	}
}

