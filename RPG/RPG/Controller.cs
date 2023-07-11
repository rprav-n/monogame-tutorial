using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG
{
	public class Controller
	{
		private static double timer = 1d;
		public static double maxTime = 1d;
		static Random rand = new Random();

		public static void Update(GameTime gameTime, Texture2D sprite)
		{
			timer -= gameTime.ElapsedGameTime.TotalSeconds;
			if (timer <= 0)
			{
				// Spawn enemy

				int side = rand.Next(4);

				Vector2 pos;

				switch (side)
				{
					case 0:
						pos = new Vector2(-500, rand.Next(-500, 2000));
						break;
                    case 1:
                        pos = new Vector2(2000, rand.Next(-500, 2000));
                        break;
                    case 2:
                        pos = new Vector2(rand.Next(-500, 2000), -500);
                        break;
                    case 3:
                        pos = new Vector2(rand.Next(-500, 2000), 2000);
                        break;
                    default:
                        pos = new Vector2(-500, rand.Next(-500, 2000));
                        break;
                }
				
                Enemy.enemies.Add(new Enemy(pos, sprite));

				timer = maxTime;
				if (maxTime > 0.5)
				{
                    maxTime -= 0.05;
                }
			} 
		}
	}
}

