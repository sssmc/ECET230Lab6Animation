using System;
using Meadow.Foundation.Graphics;
using Meadow.Foundation;

namespace ECET230Lab6Animation
{
	public class Circle
	{
		private MicroGraphics graphics;
		private int displayWidth;
		private int displayHeight;

		private int radius;
		private int positionX;
		private int positionY;

		private bool filled;

		private int speedX;
		private int speedY;

		private Color ballColor;

		public Circle(MicroGraphics graphics, int displayWidth, int displayHeight, Color ballColor, bool filled)
		{
			this.graphics = graphics;
			this.displayWidth = displayWidth;
			this.displayHeight = displayHeight;
			this.filled = filled;
			this.ballColor = ballColor;
			positionX = 0;
			positionY = 0;
			speedX = 0;
			speedY = 0;
		}

		public void setGraphics(MicroGraphics graphics)
		{
			this.graphics = graphics;
		}

		public void setDisplaySize(int width, int height)
		{
			displayHeight = height;
			displayWidth = width;
		}

		public int getPositionY()
		{
			return positionY;

		}
		public int getPositionX()
		{
			return positionX;
		}

		public void setPosition(int x, int y)
		{
			positionX = x;
			positionY = y;
		}

		public void setSpeed(int speedX, int speedY)
		{
			this.speedX = speedX;
			this.speedY = speedY;
		}

		public int getRadius()
		{
			return radius;
		}

		public void setRadius(int radius)
		{
			this.radius = radius;
		}

		public void setFilled(bool filled)
		{
			this.filled = filled;
		}

		public Color getBallColor()
		{
			return ballColor;
		}
		public void setBallColor(Color ballColor)
		{
			this.ballColor = ballColor;
		}

		private void updatePosition()
		{
			positionX += speedX;
			positionY += speedY;

			if((positionX >= (displayWidth - radius)) || (positionX <= (0 + radius)))
			{
				speedX *= -1;

			}
            if ((positionY >= (displayHeight - radius)) || (positionY <= (0 + radius)))
            {
                speedY *= -1;

            }
        }

		public void draw()
		{
			updatePosition();

            graphics.DrawCircle
            (
                centerX: positionX,
                centerY: positionY,
                radius: radius,
                color: ballColor,
				filled: filled
            );
        }
	}
}

