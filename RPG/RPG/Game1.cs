﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Comora;

namespace RPG;

enum Dir
{
    Right,
    Left,
    Up,
    Down
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

        player.anim = new SpriteAnimation(walkDown, 4, 8);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.anim.Position = player.Position;
        player.Update(gameTime);

        camera.Position = player.Position;
        camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(camera);

        _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);
        //_spriteBatch.Draw(playerSprite, player.Position, Color.White);
        player.anim.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

