using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Spaceship
{
	public class Controller
	{

		public bool inGame = false;
		public double totalTime = 0;

		public List<Asteroid> asteroids = new List<Asteroid>();
		public double timer = 2;
		public double maxTime = 2;
		public int nextSpeed = 240;

		public void Update(GameTime gameTime)
		{
			if (inGame)
			{
				double totalSeconds = gameTime.ElapsedGameTime.TotalSeconds;
                timer -= totalSeconds;
				totalTime += totalSeconds;
            } else
			{
				KeyboardState kState = Keyboard.GetState();
				if (kState.IsKeyDown(Keys.Enter))
				{
					inGame = true;
					totalTime = 0;
					timer = 2;
                    maxTime = 2;
					nextSpeed = 240;
				}
			}

			if (timer <= 0)
			{
				asteroids.Add(new Asteroid(nextSpeed));
				timer = maxTime;
				if (maxTime > 0.5)
				{
                    maxTime -= 0.1;
                }

				if (nextSpeed < 720)
				{
					nextSpeed += 4;
                }
            }
			
		}
	}
}

