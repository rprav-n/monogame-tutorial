﻿using System;
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
        private KeyboardState kStateOld = Keyboard.GetState();

        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];
        public bool dead = false;

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


            if (kState.IsKeyDown(Keys.Space))
            {
                isMoving = false;
            }

            if (dead)
                isMoving = false;

			if (isMoving)
			{
                switch (direction)
                {
                    case Dir.Right:
                        if (position.X < 1275)
                            position.X += speed * dt;
                        break;
                    case Dir.Left:
                        if (position.X > 225)
                            position.X -= speed * dt;
                        break;
                    case Dir.Up:
                        if (position.Y > 200)
                            position.Y -= speed * dt;
                        break;
                    case Dir.Down:
                        if (position.Y < 1250)
                            position.Y += speed * dt;
                        break;
                }
            }

            anim = animations[(int)direction];

            //switch (direction)
            //{
            //    case Dir.Down:
            //        anim = animations[0];
            //        break;
            //    case Dir.Up:
            //        anim = animations[1];
            //        break;
            //    case Dir.Left:
            //        anim = animations[2];
            //        break;
            //    case Dir.Right:
            //        anim = animations[3];
            //        break;
            //}

            anim.Position = new Vector2(position.X - 48, position.Y - 48);
            if (kState.IsKeyDown(Keys.Space))
            {
                anim.setFrame(0);
            }
            else if (isMoving)
            {
                anim.Update(gameTime);
            } else
            {
                anim.setFrame(1);
            }

            // Shoot Projectiles
            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                MySounds.projectileSound.Play(1f, 0.5f, 0f);
                Projectile.projectiles.Add(new Projectile(position, direction));
            }
            kStateOld = kState;
        }
    }
}

