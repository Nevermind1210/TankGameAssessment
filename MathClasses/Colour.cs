using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MathClasses
{
    public class Colour
    {
        public UInt32 colour;

        public Colour()
        {
            colour = 0;
        }

        //Creating the 4 byte or 24 bit colour integer

        public Colour(byte _red, byte _green, byte _blue, byte _alpha)
        {
            this.colour = ((UInt32)_red << 24) |
                          ((UInt32)_green << 16) |
                          ((UInt32)_blue << 8) |
                           (UInt32)_alpha;
        }

        public byte GetRed()
        {
            return (byte)((colour & 0xff000000) >> 24);
        }

        public void SetRed(byte red)
        {
            colour = colour & 0x00fffffff;
            colour |= (UInt32)red << 24;
        }

        public byte GetGreen()
        {
            return (byte)((colour & 0x0ff0000) >> 16);
        }

        public void SetGreen(byte green)
        {
            colour = colour & 0xff00ffff;
            colour |= (UInt32)green << 16;
        }

        public byte GetBlue()
        {
            return (byte)((colour & 0x0000ff00) >> 8);
        }

        public void SetBlue(byte blue)
        {
            colour = colour & 0xffff00ff;
            colour |= (UInt32)blue << 8;
        }

        public byte GetAlpha()
        {
            return (byte)((colour & 0x000000ff));
        }

        public byte RedToGreen()
        {
            return (byte)((colour & 0xff000000) >> 8);
        }
        public void SetAlpha(byte alpha)
        {
            colour = colour & 0xffffff00;
            colour |= (UInt32)alpha << 0;
        }

        public void ChangeColor()
        {
            SetGreen(GetRed());

            SetRed(0);
        }
    }
}
