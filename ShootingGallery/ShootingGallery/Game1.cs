using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D targetSprite;
    Texture2D crosshairsSprites;
    Texture2D backgroundSprite;

    SpriteFont gameFont;

    Vector2 targetPosition = new Vector2(300, 300);
    const int targetRadius = 45;
    const int crosshairHalfSize = 25;

    MouseState mState;
    bool isLeftButtonReleased = true;
    int score = 0;

    double timer = 10;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;

        int[] mynums = { };
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        targetSprite = Content.Load<Texture2D>("target");
        crosshairsSprites = Content.Load<Texture2D>("crosshairs");
        backgroundSprite = Content.Load<Texture2D>("sky");

        gameFont = Content.Load<SpriteFont>("galleryFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (timer > 0)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (timer < 0)
        {
            timer = 0;
        }
        

        mState = Mouse.GetState();

        if (mState.LeftButton == ButtonState.Pressed && isLeftButtonReleased)
        {

            float mouseTargetDist = Vector2.Distance(new Vector2(targetPosition.X + targetRadius,
                targetPosition.Y + targetRadius), mState.Position.ToVector2());

            if (mouseTargetDist < targetRadius && timer > 0)
            {
                score++;

                Random rand = new Random();
                targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth - targetSprite.Bounds.Width);
                targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight - targetSprite.Bounds.Height);

            }
            
            isLeftButtonReleased = false;
        }

        if (mState.LeftButton == ButtonState.Released)
        {
            isLeftButtonReleased = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(10, 10), Color.White);
        //_spriteBatch.DrawString(gameFont, "Time: " + timer.ToString("F0"), new Vector2(10, 50), Color.White);
        _spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(10, 50), Color.White);

        if (timer > 0)
        {
            _spriteBatch.Draw(targetSprite, targetPosition, Color.White);
        }

        _spriteBatch.Draw(crosshairsSprites, new Vector2(mState.Position.X - crosshairHalfSize,
            mState.Position.Y - crosshairHalfSize), Color.White);
        

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

