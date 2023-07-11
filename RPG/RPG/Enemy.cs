using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPG
{
	public class Enemy
	{

        public static List<Enemy> enemies = new List<Enemy>();

        private Vector2 position = new Vector2(0, 0);
		private int speed = 100;
		public int radius = 30;
        public SpriteAnimation anim;
		private bool dead = false;

        public Enemy(Vector2 newPos, Texture2D sprite)
		{
			position = newPos;
			anim = new SpriteAnimation(sprite, 10, 6);
		}

		public Vector2 Position
		{
			get
			{
				return position;
			}
		}

		public bool Dead
		{
			get { return dead; }
            set { dead = value; }
        }

		public void Update(GameTime gameTime, Vector2 playerPos, bool isPlayerDead)
		{

            anim.Position = new Vector2(position.X - 48, position.Y - 66);
            anim.Update(gameTime);

            if (!isPlayerDead)
			{
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                Vector2 moveDir = playerPos - position;
                moveDir.Normalize();
                position += moveDir * speed * dt;
            }
        }
	}
}

