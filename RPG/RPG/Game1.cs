﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using Comora;

namespace RPG;

public enum Dir
{
    Down,
    Up,
    Left,
    Right
}

public static class MySounds
{
    public static SoundEffect projectileSound;
    public static Song bgMusic;
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D playerSprite;
    Texture2D walkDown;
    Texture2D walkUp;
    Texture2D walkRight;
    Texture2D walkLeft;

    Texture2D background;
    Texture2D ball;
    Texture2D skull;

    Player player = new Player();

    Camera camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;

        _graphics.ApplyChanges();

        camera = new Camera(_graphics.GraphicsDevice);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        playerSprite = Content.Load<Texture2D>("Player/player");
        walkDown = Content.Load<Texture2D>("Player/walkDown");
        walkUp = Content.Load<Texture2D>("Player/walkUp");
        walkRight = Content.Load<Texture2D>("Player/walkRight");
        walkLeft = Content.Load<Texture2D>("Player/walkLeft");

        background = Content.Load<Texture2D>("background");
        ball = Content.Load<Texture2D>("ball");
        skull = Content.Load<Texture2D>("skull");

        MySounds.projectileSound = Content.Load<SoundEffect>("Sounds/blip");
        MySounds.bgMusic = Content.Load<Song>("Sounds/nature");
       
        MediaPlayer.Play(MySounds.bgMusic);

        player.animations[0] = new SpriteAnimation(walkDown, 4, 8);
        player.animations[1] = new SpriteAnimation(walkUp, 4, 8);
        player.animations[2] = new SpriteAnimation(walkLeft, 4, 8);
        player.animations[3] = new SpriteAnimation(walkRight, 4, 8);

        player.anim = player.animations[0];



        //Enemy.enemies.Add(new Enemy(new Vector2(100, 100), skull));
        //Enemy.enemies.Add(new Enemy(new Vector2(700, 500), skull));

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.anim.Position = player.Position;
        player.Update(gameTime);
        if (!player.dead)
            Controller.Update(gameTime, skull);

        camera.Position = player.Position;
        camera.Update(gameTime);

        foreach(Projectile proj in Projectile.projectiles)
        {
            proj.Update(gameTime);
        }

        foreach (Enemy e in Enemy.enemies)
        {
            int sum = 32 + e.radius;

            if (Vector2.Distance(player.Position, e.Position) <= sum)
            {
                player.dead = true;
            }

            e.Update(gameTime, player.Position, player.dead);
        }

        foreach (Projectile proj in Projectile.projectiles)
        {
            foreach (Enemy e in Enemy.enemies)
            {
                int sum = proj.radius + e.radius;

                if (Vector2.Distance(proj.Position, e.Position) <= sum)
                {
                    proj.Collided = true;
                    e.Dead = true;
                }
            }
        }

        Projectile.projectiles.RemoveAll(p => p.Collided);
        Enemy.enemies.RemoveAll(e => e.Dead);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(camera);

        _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);

        foreach (Enemy e in Enemy.enemies)
        {
            e.anim.Draw(_spriteBatch);
        }

        foreach (Projectile proj in Projectile.projectiles)
        {
            _spriteBatch.Draw(ball, new Vector2(proj.Position.X - 48, proj.Position.Y - 48), Color.White);
        }

        //_spriteBatch.Draw(playerSprite, player.Position, Color.White);
        if (!player.dead) 
            player.anim.Draw(_spriteBatch);


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

