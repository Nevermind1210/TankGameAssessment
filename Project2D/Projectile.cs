using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Raylib;
using System.Threading.Tasks;
using static Raylib.Raylib;
using MathClasses;

namespace Project2D
{
    class Projectile : SceneObject
    {
        //bullet go fast
        float speed = 700;
        //varible to store direction to
        MathClasses.Vector3 direction = new MathClasses.Vector3(0, 0, 0);


        public Projectile(float xDirection, float yDirection)
        {
            direction.x = xDirection;
            direction.y = yDirection;
        }

        // when update is  called move bullet in the forward facing position

        public override void OnUpdate(float deltaTime)
        {
            MathClasses.Vector3 facing = new MathClasses.Vector3(
                direction.x,
                direction.y, 1) * deltaTime * speed;
            Translate(facing.x, facing.y);
        }

        //draws a raylib circle and gives it m7 and m8 position

        public override void OnDraw()
        {
            Raylib.Raylib.DrawCircle((int)globalTransform.m7, (int)globalTransform.m8, 10, Color.RED);
        }
    }
}
