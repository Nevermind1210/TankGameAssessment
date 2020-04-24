using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathClasses;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        SceneObject tankObject = new SceneObject();
        SceneObject turretObject = new SceneObject();
        SceneObject bulletObject = new SceneObject();

        SpriteObject tankSprite = new SpriteObject();
        SpriteObject turretSprite = new SpriteObject();
        SpriteObject bulletSprite = new SpriteObject();

        //colliders
        AABB tankcollider = new AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));
        AABB BulletCollider = new AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 0, 0));

        //walls
        AABB TopWall = new AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(640, 0, 0));

        AABB BottomWall = new AABB(new MathClasses.Vector3(0, 480, 0), new MathClasses.Vector3(640, 480, 0));

        AABB RSideWall = new AABB(new MathClasses.Vector3(640, 0, 0), new MathClasses.Vector3(640, 480, 0));

        AABB LSideWall = new AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(0, 480, 0));

        //TestBoxDummy
        AABB DummyBox = new AABB(new MathClasses.Vector3(0, 0, 0), new MathClasses.Vector3(200, 200, 0));

        private List<Projectile> bulletsList = new List<Projectile>();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            //if (Stopwatch.IsHighResolution)
            //{
            //    Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            //}
            //----------------------------------------------------------------------------GameOBJ's----------------------------------------------------------------------------------------}
            tankSprite.Load("../Images/tankBlue_outline.png");
            //sprite is facing the wrong way... fix here
            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            // sets an offset for the base, it rotates around the centre
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);

            turretSprite.Load("../Images/barrelBlue.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            //set the turret offset from the tank base
            turretSprite.SetPosition(0, turretSprite.Width / 2.0f);

            bulletSprite.Load("../Images/bulletBlue_outline.png");
            bulletSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            bulletSprite.SetPosition(0, bulletSprite.Width / 2.0f);
            //scene object hierarchy - parent the turrent to the base,
            //then the base to the tank sceneObject

            turretObject.AddChild(turretSprite);
            tankObject.AddChild(tankSprite);
            tankObject.AddChild(turretObject);

            //having an empty object for the tank parent means we can set the
            //position/rotation of the tank without
            //affecting the offset of the base sprite
            tankObject.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);

            //Colliders

        }

      

        public void Shutdown()
        {
        }

        public void Update()
        {
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

            lastTime = currentTime;
            // insert game logic here            

            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                tankObject.Rotate(-deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                tankObject.Rotate(deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                MathClasses.Vector3 facing = new MathClasses.Vector3(
                    tankObject.LocalTransform.m1,
                    tankObject.LocalTransform.m2, 1) * deltaTime * 100;
                tankObject.Translate(facing.x, facing.y);
                
            }
            if (IsKeyDown(KeyboardKey.KEY_S))
            {
                MathClasses.Vector3 facing = new MathClasses.Vector3(
                    tankObject.LocalTransform.m1,
                    tankObject.LocalTransform.m2, 1) * deltaTime * -100;
                tankObject.Translate(facing.x, facing.y);
               
            }

            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                turretObject.Rotate(-deltaTime);
            }
            if (IsKeyDown(KeyboardKey.KEY_E))
            {
                turretObject.Rotate(deltaTime);
            }
            tankObject.Update(deltaTime);
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                //Shoots the bullets
                Projectile bullet = new Projectile(turretObject.GlobalTransform.m5, -turretObject.GlobalTransform.m4);
                bullet.SetPosition(turretObject.GlobalTransform.m7, turretObject.GlobalTransform.m8);
                bulletsList.Add(bullet);

                Console.WriteLine(bulletsList);
            }
            tankcollider.Resize(new MathClasses.Vector3(tankObject.GlobalTransform.m7 - tankSprite.Width / 2, tankObject.GlobalTransform.m8 - tankSprite.Height / 2, 0),
                new MathClasses.Vector3((tankObject.GlobalTransform.m7 + tankSprite.Width / 2), tankObject.GlobalTransform.m8 + tankSprite.Height / 2, 0));

            PlayerColls(TopWall);
            PlayerColls(RSideWall);
            PlayerColls(LSideWall);
            PlayerColls(BottomWall);
            PlayerColls(DummyBox);
            BulletCollisionDetect();

        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);
            DrawText(fps.ToString(), 10, 10, 12, Color.RED);

            DrawRectangleLines(Convert.ToInt32(tankObject.GlobalTransform.m7 - tankSprite.Width / 2), Convert.ToInt32(tankObject.GlobalTransform.m8 - tankSprite.Height / 2), 83, 78,
                Color.MAGENTA);

            DrawRectangleLines(0,0,200,200,Color.BROWN);

            DrawTexture(texture,
                GetScreenWidth() / 2 - texture.width / 2, GetScreenHeight() / 2 - texture.height / 2, Color.WHITE);

            if (bulletsList.Count >= 1)
            {
                foreach (SceneObject bullet in bulletsList)
                {
                    bullet.Draw();
                }
            }
            tankObject.Draw();
            EndDrawing();
        }

        public void PlayerColls(AABB CollidingWith)
        {
            if (tankcollider.Overlaps(CollidingWith) && IsKeyDown(KeyboardKey.KEY_W))
            {
                MathClasses.Vector3 facing = new MathClasses.Vector3(
                    tankObject.LocalTransform.m1,
                    tankObject.LocalTransform.m2, 1) * deltaTime * -100;
                tankObject.Translate(facing.x, facing.y);
            }
            else if (tankcollider.Overlaps(CollidingWith) && IsKeyDown(KeyboardKey.KEY_S))
                {
                MathClasses.Vector3 facing = new MathClasses.Vector3(
                    tankObject.LocalTransform.m1,
                    tankObject.LocalTransform.m2, 1) * deltaTime * 100;
                tankObject.Translate(facing.x, facing.y);
            }
        }
        public void BulletCollisionDetect()
        {
            //if this bullet is above or equal to 1.
            if (bulletsList.Count >= 1)
            {
                foreach (Projectile bullet in bulletsList)
                {
                    //Bullets updated per detlatime.
                    bullet.OnUpdate(deltaTime);
                    //changes the sizing of the AABB col by subtracting the radius by the position of the bullets transform.
                    BulletCollider.Resize(new MathClasses.Vector3(bullet.GlobalTransform.m7 - 10, bullet.GlobalTransform.m8 - 10, 0),
                        new MathClasses.Vector3(bullet.GlobalTransform.m7 + 10, bullet.GlobalTransform.m8 + 10, 0));

                    DrawRectangleLines((int)bullet.GlobalTransform.m7 - 10, (int)bullet.GlobalTransform.m8 - 10, 10, 10, Color.DARKBROWN);

                    //If the bulletoverlaps, the bullet is removed.
                    if (BulletCollider.Overlaps(LSideWall))
                    {
                        bulletsList.Remove(bullet);
                        Console.WriteLine("HIT");
                        break;
                    }
                    if (BulletCollider.Overlaps(RSideWall))
                    {
                        bulletsList.Remove(bullet);
                        Console.WriteLine("HIT");
                        break;
                    }
                    if (BulletCollider.Overlaps(BottomWall))
                    {
                        bulletsList.Remove(bullet);
                        Console.WriteLine("HIT");
                        break;
                    }
                    if (BulletCollider.Overlaps(TopWall))
                    {
                        bulletsList.Remove(bullet);
                        Console.WriteLine("HIT");
                        break;
                    }
                    if (BulletCollider.Overlaps(DummyBox))
                    {
                        bulletsList.Remove(bullet);
                        Console.WriteLine("HIT");
                        break;
                    }

                }
            }
        }

        Image GameObject;
        Texture2D texture;
    }
}
