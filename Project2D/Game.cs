using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        public Game()
        {
            stopwatch.Start();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public float Seconds
        {
            get { return stopwatch.ElapsedMilliseconds / 1000.0f; }
        }

        public float GetDeltaTime()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            return deltaTime;
        }

        Image GameObject;
        Texture2D texture;


        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }
            //----------------------------------------------------------------------------GameOBJ's----------------------------------------------------------------------------------------}
            tankSprite.Load("tankBlue_outline.png");
            //sprite is facing the wrong way... fix here
            //tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            //// sets an offset for the base, it rotates around the centre
            //tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            //turretSprite.Load("barrelBlue.png");
            //turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            ////set the turret offset from the tank base
            //turretSprite.SetPosition(0, turretSprite.Width / 2.0fl)
        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            // insert game logic here            
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            DrawTexture(texture, 
                GetScreenWidth() / 2 - texture.width / 2, GetScreenHeight() / 2 - texture.height / 2, Color.WHITE);

            EndDrawing();
        }

    }
}
