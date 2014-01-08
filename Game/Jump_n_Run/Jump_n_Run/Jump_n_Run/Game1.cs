using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Jump_n_Run.classes;
namespace Jump_n_Run
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle mainFrame = new Rectangle(0, 0, 1024, 768);
        Rectangle bgFrame = new Rectangle(0, 0, 2048, 768);
        
        Texture2D background;
        Texture2D objTex;
        Texture2D objItemKey;
        Texture2D objItemKeyHole;
        
        SpriteFont Arial;
        int updateCounter = 0;
        long initMemory;

        Stopwatch sw = new Stopwatch();
        long frametime;
        double fps;

        string fpsDraw;


        // List of all GameObjects

        List<GameObject> GObjects = new List<GameObject>();

        // Instanzen
        Player player = new Player();
        Key key = new Key();
        KeyHole keyHole = new KeyHole();

        Enemy enemy1;

        public Game1()
        {
            // open a window 800x600
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";

            this.initMemory = GC.GetTotalMemory(false);
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            background = new Texture2D(graphics.GraphicsDevice, 800,600);
            base.Initialize();
            sw.Start();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("landscape_2048x768");

            player.loadPlayer(this.graphics, this.Content);


            objTex = new Texture2D(graphics.GraphicsDevice, 64, 64);
            objTex = Content.Load<Texture2D>("object");

            // Item
            objItemKey = Content.Load<Texture2D>(@"Images/gameobjects/Schlüssel_2");
            objItemKeyHole = Content.Load<Texture2D>(@"Images/gameobjects/Schlüsselloch");

            key = new Key(objItemKey, new Rectangle(400,380,20,20));
            keyHole = new KeyHole(objItemKeyHole, new Rectangle(950, 680, 40, 40));

            enemy1 = new Enemy(10,10, objTex, new Rectangle(100, 100, 40, 40),120);

            GObjects.Add(enemy1);
          
            #region testLevel

            GObjects.Add(new GameObject(objTex,new Rectangle(100,400,20,200)));
            GObjects.Add(new GameObject(objTex, new Rectangle(180, 400, 20, 200)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 700, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(400, 600, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 500, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(300, 400, 200, 10)));

            GObjects.Add(new GameObject(objTex,new Rectangle(0,718,1024,50)));

            GObjects.Add(new GameObject(objTex, new Rectangle(950,0,40,640)));

            GObjects.Add(new MoveableObject(0, 10, objTex, new Rectangle(120,560,60,40),0));
            GObjects.Add(new MoveableObject(0, 9, objTex, new Rectangle(120, 520, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 8, objTex, new Rectangle(120, 480, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 7, objTex, new Rectangle(120, 440, 60, 40), 0));
            GObjects.Add(new MoveableObject(0, 6, objTex, new Rectangle(120, 400, 60, 40), 0));

            GObjects.Add(new MoveableObject(10, 0, objTex, new Rectangle(100, 600, 100, 64), Keys.W, Keys.S, Keys.A, Keys.D, 0));
            #endregion


            GObjects.Add(player);
            GObjects.Add(key);
            GObjects.Add(keyHole);


            Arial = Content.Load<SpriteFont>("Arial");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            sw.Stop();
            frametime = sw.ElapsedMilliseconds;
            fps = frametime / 1000.0 * 3600.0;

            if (fps >= 60.0)
            {
                fpsDraw = "FPS: " + Convert.ToString(fps) + " - CAP";
            }
            else
            {
                fpsDraw = "FPS: " + Convert.ToString(fps);
            }
            sw.Reset();
            sw.Start();


            updateCounter++;
            KeyboardState kbstate = Keyboard.GetState();

            // Allows the game to exit
            if (kbstate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            List<GameObject> toDelete = new List<GameObject>();

            GObjects.Remove(null);

           

            foreach (GameObject go in GObjects)
            {
                go.Move(gameTime, kbstate, mainFrame, GObjects);
            }

            enemy1.KI_Movement(mainFrame, GObjects, gameTime);


            Scrolling.Scroll(player,   GObjects,ref bgFrame, mainFrame);

         

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            KeyboardState kbstate = Keyboard.GetState();
            spriteBatch.Begin();

            spriteBatch.Draw(background,bgFrame,Color.White);


            foreach (GameObject gobject in GObjects)
            {
                gobject.Draw(ref spriteBatch);
            }

            spriteBatch.DrawString(Arial, Convert.ToString(((GC.GetTotalMemory(false))/1024)) + "KB", new Vector2(1, 1), Color.LimeGreen);
            spriteBatch.DrawString(Arial, fpsDraw, new Vector2(1, 20), Color.LimeGreen);

                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
