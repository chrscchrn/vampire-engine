using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vampire;

public class Engine : Game
{
    public static Engine Instance { get; private set; }
    public GraphicsDeviceManager _graphics;
    public SpriteBatch _spriteBatch;

    public static string AssemblyDirectory = Path.GetDirectoryName(
        Assembly.GetEntryAssembly().Location
    );
    public string ContentDirectory
    {
        get { return Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory); }
    }

    private Scene currentScene;
    private Scene nextScene;

    public Engine()
    {
        Instance = this;
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = false;
        Content.RootDirectory = @"Content/";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // nextScene = new Chandula();

    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();
        if (currentScene is not null)
        {
            currentScene.Update();
        }

        if (currentScene != nextScene)
        {
            Scene lastScene = currentScene;
            if (lastScene != null)
                lastScene.End();
            currentScene = nextScene;
            OnSceneTransition(lastScene, nextScene);
            if (currentScene != null)
                currentScene.Start();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SlateGray);

        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Matrix.CreateScale(6f, 6f, 1f)
        );
        if (currentScene != null)
        {
            currentScene.Render();
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    public Scene Scene
    {
        get => currentScene;
        set => nextScene = value; 
    }

    private void OnSceneTransition(Scene from, Scene to)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
