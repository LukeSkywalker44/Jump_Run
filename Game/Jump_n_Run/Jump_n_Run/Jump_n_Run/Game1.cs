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
using X2DPE;
using X2DPE.Helpers;
using System.Threading.Tasks;

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

        List<Enemy> enemies = new List<Enemy>();


        // List of all GameObjects

        List<GameObject> GObjects = new List<GameObject>();

        // Instanzen
        Player player = new Player();
        Key key = new Key();
        KeyHole keyHole = new KeyHole();
        GunItem gun = new GunItem();

        GameObject Crosshair;
        

        Emitter emitter = new Emitter();
        ParticleComponent pcomponent;



        public Game1()
        {
            // open a window 800x600
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";

            this.initMemory = GC.GetTotalMemory(false);



            pcomponent = new ParticleComponent(this);

            this.Components.Add(pcomponent);

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
            background = new Texture2D(graphics.GraphicsDevice, 800, 600);
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
            objItemKey = Content.Load<Texture2D>(@"Images/gameobjects/Schl�ssel_2");
            objItemKeyHole = Content.Load<Texture2D>(@"Images/gameobjects/Schl�sselloch");
            Texture2D objItemGun = Content.Load<Texture2D>(@"Images/gameobjects/Waffe2");
           

            key = new Key(objItemKey, new Rectangle(400, 380, 20, 20));
            keyHole = new KeyHole(objItemKeyHole, new Rectangle(950, 640, 40, 80));
            gun = new GunItem(objItemGun, new Rectangle(0, 0, 760, 290));


            MouseState ms = Mouse.GetState();

            Crosshair = new GameObject(objTex,new Rectangle(ms.X,ms.Y,5,5));

            #region testLevel

            GObjects.Add(new GameObject(objTex, new Rectangle(100, 400, 20, 200)));
            GObjects.Add(new GameObject(objTex, new Rectangle(180, 400, 20, 200)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 700, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(400, 600, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(600, 500, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(300, 400, 200, 10)));

            GObjects.Add(new GameObject(objTex, new Rectangle(0, 718, 1024, 50)));

            GObjects.Add(new GameObject(objTex, new Rectangle(950, 0, 40, 640)));


            #endregion


            GObjects.Add(player);
            GObjects.Add(key);
            GObjects.Add(keyHole);
            GObjects.Add(gun);
            GObjects.Add(Crosshair);



            Arial = Content.Load<SpriteFont>("Arial");




            Emitter emitter = new Emitter()
     {
         Active = true,
         TextureList = new List<Texture2D>()
		{
			Content.Load<Texture2D>(@"Kopie von object")
			
		 },
         RandomEmissionInterval = new RandomMinMax(1.0d),
         ParticleLifeTime = 2000,
         ParticleDirection = new RandomMinMax(0,359),
         ParticleSpeed = new RandomMinMax(6.0f, 12.0f),
         ParticleRotation = new RandomMinMax(0, 100),
         RotationSpeed = new RandomMinMax(0.015f),
         ParticleFader = new ParticleFader(false, true, 1350),
         ParticleScaler = new ParticleScaler(true, 0.04f),
         colliders = GObjects
     };

           

           
            pcomponent.particleEmitterList.Add(emitter);
            pcomponent.particleEmitterList.Add(new Emitter()  {
         Active = true,
         TextureList = new List<Texture2D>()
		{
			Content.Load<Texture2D>(@"Kopie von object")
			
		 },
         RandomEmissionInterval = new RandomMinMax(1.0d),
         ParticleLifeTime = 2000,
         ParticleDirection = new RandomMinMax(89,91),
         ParticleSpeed = new RandomMinMax(6.0f, 12.0f),
         ParticleRotation = new RandomMinMax(0, 100),
         RotationSpeed = new RandomMinMax(0.015f),
         ParticleFader = new ParticleFader(false, true, 1350),
         ParticleScaler = new ParticleScaler(true, 0.04f),
         colliders = GObjects
     });



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



            pcomponent.particleEmitterList[0].Position = new Vector2(player.rectangle.X + 30, player.rectangle.Y + 35);



            pcomponent.particleEmitterList[0].Position = new Vector2(new Random().Next(-100, 2000), -100);
            pcomponent.particleEmitterList[0].Active = true;

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


            emitter.Active = true;
            updateCounter++;
            KeyboardState kbstate = Keyboard.GetState();


            if (kbstate.IsKeyDown(Keys.E))
            {

                pcomponent.particleEmitterList[0].Active = true;
              
            }
            else
            {
                pcomponent.particleEmitterList[0].Active = false;
            }

            // Allows the game to exit
            if (kbstate.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            pcomponent.particleEmitterList[0].Active = true;
            List<GameObject> toDelete = new List<GameObject>();

            GObjects.Remove(null);

            

            MouseState ms = Mouse.GetState();

            Crosshair.rectangle.X = ms.X;
            Crosshair.rectangle.Y = ms.Y;

            Vector2 mouse = new Vector2(ms.X, ms.Y);
            Vector2 gunVec = new Vector2(gun.rectangle.X, gun.rectangle.Y);

            Vector2 AngleVec = mouse - gunVec;

            float rotation = (float)Math.Atan2(AngleVec.Y, AngleVec.X);

            gun.rotation = rotation;
            gun.rectangle.X = player.rectangle.X + 20;
            gun.rectangle.Y = player.rectangle.Y + 20;

            pcomponent.particleEmitterList[1].Position = new Vector2(gun.rectangle.X + 10, gun.rectangle.Y);

            if (ms.LeftButton.Equals(ButtonState.Pressed))
            {
                pcomponent.particleEmitterList[1].Active = true;
            }
            else
            {
                pcomponent.particleEmitterList[1].Active = false;
            }

              
            if (rotation > MathHelper.Pi)
            {
                player.playerDirection = SpriteEffects.FlipHorizontally;
            }

            gun.se = player.playerDirection;

            foreach (GameObject go in GObjects)
            {
                 go.Move(gameTime, kbstate, mainFrame, GObjects);
               
            }




          

            foreach (Enemy gegner in enemies)
            {
                gegner.KI_Movement(mainFrame, GObjects, gameTime);
            }
        

            

            Scrolling.Scroll(player, GObjects, ref bgFrame, mainFrame, pcomponent.particleEmitterList[0]);


            Panda locpanda = new Panda();
            locpanda.loadPanda(this.graphics, this.Content);

            locpanda.rectangle.X = new Random().Next(0, 1000);
            locpanda.rectangle.Y = new Random().Next(0, 500);


            if (kbstate.IsKeyDown(Keys.Space))
            {
               
                enemies.Add(locpanda);
                GObjects.Add(locpanda);
            }


            sw.Reset();
            sw.Start();

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

            spriteBatch.Draw(background, bgFrame, Color.White);



            foreach (GameObject gobject in GObjects)
            {
                gobject.Draw(ref spriteBatch);
            }


            spriteBatch.DrawString(Arial, Convert.ToString(((GC.GetTotalMemory(false)) / 1024)) + "KB", new Vector2(1, 1), Color.LimeGreen);
            spriteBatch.DrawString(Arial, fpsDraw, new Vector2(1, 20), Color.LimeGreen);
            spriteBatch.DrawString(Arial, "Pandas: " + enemies.Count, new Vector2(1, 40), Color.LimeGreen);
            spriteBatch.DrawString(Arial, "Partikel: " + pcomponent.particleEmitterList[0].ParticleList.Count, new Vector2(1, 60), Color.LimeGreen);

          

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
