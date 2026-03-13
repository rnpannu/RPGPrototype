using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;


namespace MonoGameLibrary;

public class Core : Game
{

    // Gets a reference to the core instance
    internal static Core s_instance;

    public static Core Instance => s_instance;

    private static Scene s_activeScene;

    private static Scene s_nextScene;


    // Controls the presentation of graphics
    public static GraphicsDeviceManager Graphics { get; private set; }
    
    // Create graphics resource and perform primitive rendering. Primitive as in general basic or OpenGL primitive?
    public static new GraphicsDevice GraphicsDevice {  get; private set; }

    public static SpriteBatch SpriteBatch;

    public static new ContentManager Content { get; private set; }

    public static InputManager Input { get; private set; }

    public static bool ExitOnEscape { get; set; }

    public static AudioController Audio { get; private set; }
    
    public static int Width { get; private set; } = 320;
    public static int Height { get; private set; } = 180;
    
    
    // Will change depending on the size of the screen
    public static int VirtualWidth { get; set; } = 1600;

    public static int VirtualHeight { get; set; } = 900;
    
    public static Matrix Scale { get; set; }

    public static Viewport Viewport { get; private set; }


    public static bool Vsync { get; private set; } = true;

    public static float DT, FPS;

    /// <summary>
    /// Creates a new Core instance.
    /// </summary>
    /// <param name="title">The title to display in the title bar of the game window.</param>
    /// <param name="width">The initial width, in pixels, of the game window.</param>
    /// <param name="height">The initial height, in pixels, of the game window.</param>
    /// <param name="fullScreen">Indicates if the game should start in fullscreen mode.</param>
    public Core(string title, int width, int height, bool fullScreen)
    {
        if (s_instance != null)
        {
            throw new InvalidOperationException("Only a single core instance can be created.");
        }

        s_instance = this;

        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreferredBackBufferWidth = width;
        VirtualWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        VirtualHeight = height;
        Graphics.SynchronizeWithVerticalRetrace = Vsync;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();


        Window.Title = title;

        Content = base.Content;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;

        ExitOnEscape = true;

    }

    protected override void Initialize()
    {
        base.Initialize();

        GraphicsDevice = base.GraphicsDevice;

        SpriteBatch = new SpriteBatch(GraphicsDevice);

        Input = new InputManager();

        Audio = new AudioController();
        
        CalculateScale();
    }

    protected override void UnloadContent()
    {
        Audio.Dispose();
        base.UnloadContent();
    }
    
    /// <summary>
    /// Calculate scaling from base resolution to output resolution.
    /// Logic to implement dynamic resizing is there if we want to make an event listen register for it
    ///     - Window.ClientSizeChanged event
    /// </summary>
    public void CalculateScale()
    {
        /*float screenWidth = Core.GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = Core.GraphicsDevice.PresentationParameters.BackBufferHeight;
        
        if (screenWidth / Width > screenHeight / Height)
        {
            int aspect = (int) (screenHeight / Height);
            VirtualWidth = (aspect * Width);
            VirtualHeight = (Height);
        }
        else
        {
            int aspect = (int)screenWidth / Width;
            VirtualWidth = (Width);
            VirtualHeight = (aspect * Height);
        }*/
        
        Scale = Matrix.CreateScale((float)VirtualWidth / (float)Width);
        
        // Can implement later, center scaling so it doesn't scale from the top left
        Viewport = new Viewport
        {
            X = (int)(0),
            Y = (int)(0),
            Width = VirtualWidth,
            Height = VirtualHeight
        };
    }
    protected override void Update(GameTime gameTime)
    {
        DT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        FPS = (float)(1 / DT);

        Input.Update(gameTime);

        Audio.Update();
        if (ExitOnEscape && Input.Keyboard.IsKeyDown(Keys.Escape))
        {
            //Exit();
        }


        if(s_nextScene != null)
        {
            TransitionScene();
        }

        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);
        }

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        if (s_activeScene != next)
        {
            {
                s_nextScene = next;
            }
        }
    }

    private static void TransitionScene()
    {
        if(s_activeScene != null)
        {
            s_activeScene.Dispose();
        }


        // Call garbage collector 
        GC.Collect();

        s_activeScene = s_nextScene;

        s_nextScene = null;

        if(s_activeScene != null)
        {
            Console.Out.WriteLine("Initialize scene: " + (s_activeScene.GetType()));
            s_activeScene.Initialize();
        }
    }

    protected void ExitGame()
    {
        Exit();
    }
}
