using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

namespace Game.Graphics
{

    class LipSync
    {
        static Dictionary<char, Texture2D> mouth;
        TimeSpan sinceLast;
        int curIndex;
        string phonemes, message;
        GraphicsDeviceManager graphics;
        public LipSync(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            if (mouth == null)
            {
                mouth = new Dictionary<char, Texture2D>();
                mouth.Add('a', content.Load<Texture2D>("Art/Mouths/AI"));
                mouth.Add('i', mouth['a']);
                mouth.Add('c', content.Load<Texture2D>("Art/Mouths/CDGJKNRSYZ"));
                mouth.Add('d', mouth['c']);
                mouth.Add('g', mouth['c']);
                mouth.Add('j', mouth['c']);
                mouth.Add('k', mouth['c']);
                mouth.Add('n', mouth['c']);
                mouth.Add('r', mouth['c']);
                mouth.Add('s', mouth['c']);
                mouth.Add('y', mouth['c']);
                mouth.Add('z', mouth['c']);
                mouth.Add('e', content.Load<Texture2D>("Art/Mouths/E"));
                mouth.Add('f', content.Load<Texture2D>("Art/Mouths/FV"));
                mouth.Add('v', mouth['f']);
                mouth.Add('l', content.Load<Texture2D>("Art/Mouths/L"));
                mouth.Add('m', content.Load<Texture2D>("Art/Mouths/MBP"));
                mouth.Add('b', mouth['m']);
                mouth.Add('p', mouth['m']);
                mouth.Add('o', content.Load<Texture2D>("Art/Mouths/O"));
                mouth.Add(' ', content.Load<Texture2D>("Art/Mouths/rest"));
                mouth.Add('#', content.Load<Texture2D>("Art/Mouths/Th"));
                mouth.Add('u', content.Load<Texture2D>("Art/Mouths/U"));
                mouth.Add('w', content.Load<Texture2D>("Art/Mouths/WQ"));
                mouth.Add('q', mouth['w']);
                for (char c = (char)0; c < (char)255; c++)
                    if (!mouth.ContainsKey(c)) mouth.Add(c, mouth[' ']);
            }
        }
        
        public string Text
        {
            set
            {
                message = value;
                phonemes = Convert(value);
            }
        }

        string Convert(string oldText)
        {
            StringBuilder text = new StringBuilder(oldText.ToLower());
            text = text.Replace("0", "zero");
            text = text.Replace("1", "won");
            text = text.Replace("2", "too");
            text = text.Replace("3", "#ree");
            text = text.Replace("4", "for");
            text = text.Replace("5", "fiv");
            text = text.Replace("6", "six");
            text = text.Replace("7", "seven");
            text = text.Replace("8", "aet");
            text = text.Replace("9", "nien");
            text = text.Replace("two", "too");
            text = text.Replace("th", "#");
            text = text.Replace("h", " ");
            text = text.Replace("t", "d");
            text = text.Replace("p", "pu");
            text = text.Replace("x", "ks");
            return text.ToString();
        }

        public void Update(GameTime gameTime)
        {
            sinceLast += gameTime.ElapsedGameTime;
            if (phonemes != null)
            {
                if (sinceLast.Milliseconds > 75)
                {
                    curIndex = (curIndex + 1) % phonemes.Length;
                    sinceLast = TimeSpan.Zero;
                }
            }
        }

        public void Draw(SpriteBatch batch, Vector2 offset, SpriteFont textFont)
        {
            batch.DrawString(textFont, this.message.Substring(0, curIndex * message.Length / phonemes.Length), new Vector2(0, 0), Color.White);
            batch.Draw(mouth[phonemes[curIndex]], offset, XnaColor.White);
        }
    }
}
