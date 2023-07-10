using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG
{
	public class Player
	{
		private Vector2 position = new Vector2(500, 300);
		private int speed = 300;
		private Dir direction = Dir.Down;
		private bool isMoving = false;

        public SpriteAnimation anim;

		public Vector2 Position
		{
			get
			{
				return position;
			}
		}

		public void setX(float newX)
		{
			position.X = newX;
		}

        public void setY(float newY)
        {
            position.Y = newY;
        }

		public void Update(GameTime gameTime)
		{
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            isMoving = false;

			KeyboardState kState = Keyboard.GetState();

			if (kState.IsKeyDown(Keys.Right))
			{
				direction = Dir.Right;
				isMoving = true;

            };
            if (kState.IsKeyDown(Keys.Left))
			{
                direction = Dir.Left;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up))
			{
                direction = Dir.Up;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Down))
			{
                direction = Dir.Down;
                isMoving = true;
            }

			if (isMoving)
			{
                switch (direction)
                {
                    case Dir.Right:
                        position.X += speed * dt;
                        break;
                    case Dir.Left:
                        position.X -= speed * dt;
                        break;
                    case Dir.Up:
                        position.Y -= speed * dt;
                        break;
                    case Dir.Down:
                        position.Y += speed * dt;
                        break;
                }
            }
            anim.Position = new Vector2(position.X - 48, position.Y - 48);
            anim.Update(gameTime);
        }
    }
}

