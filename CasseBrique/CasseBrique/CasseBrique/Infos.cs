using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CasseBrique
{
    public class Infos
    {
        private SpriteFont _PopCornFont;
        private SpriteFont _PopCornFontSmall;
        private SpriteFont _PopCornFontBig;
        private Viewport _Viewport;
        private int _Frames = 0;
        private float _ElapsedTime = 0.0f;
        private int _Fps = 0;
        private int _Vies = 1000;
        private Dictionary<BonusType, int> _Bonus = new Dictionary<BonusType,int>();
        private Texture2D _Vie;
        private int _Score = 0;

        public Infos(Viewport InfosViewport)
        {
            _Viewport = InfosViewport;
        }

        public void LoadContent(ContentManager content)
        {
            _PopCornFont = content.Load<SpriteFont>("PopCornFont");
            _PopCornFontSmall = content.Load<SpriteFont>("PopCornFontSmall");
            _PopCornFontBig = content.Load<SpriteFont>("PopCornFontBig");
            _Vie = content.Load<Texture2D>("raquette");
        }

        public void Update(GameTime gameTime, Raquette _Raquette)
        {
            _ElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_ElapsedTime >= 1000.0f)
            {
                _Fps = _Frames;
                _Frames = 0;
                _ElapsedTime = 0;
            }

            _Vies = _Raquette.Vie;
            _Bonus = _Raquette.ListeBonus;
            _Score = _Raquette.Score;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _Frames++;

            spriteBatch.DrawString(_PopCornFont, string.Format("FPS = {0}", _Fps), new Vector2(_Viewport.X + 20, _Viewport.Y + 8), Color.White);
            spriteBatch.DrawString(_PopCornFont, "P = Pause", new Vector2(_Viewport.X + 20, _Viewport.Y + 25), Color.White);
            spriteBatch.DrawString(_PopCornFont, "ESC = Exit", new Vector2(_Viewport.X + 20, _Viewport.Y + 42), Color.White);

            spriteBatch.DrawString(_PopCornFontBig, "VIES", new Vector2(_Viewport.X + 55, _Viewport.Y + 115), Color.Green);
            //spriteBatch.DrawString(_PopCornFont, _Vies.ToString(), new Vector2(_Viewport.X + 20, _Viewport.Y + 150), Color.White);

            int EspacementVertical = 20;

            for (int i = 0; i < _Vies; i++)
            {
                spriteBatch.Draw(_Vie, new Vector2(_Viewport.X + 10, _Viewport.Y + 152 + i * EspacementVertical), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f); 
            }


            spriteBatch.DrawString(_PopCornFontBig, "SCORE", new Vector2(_Viewport.X + 50, _Viewport.Y + 264), Color.Green);

            spriteBatch.DrawString(_PopCornFont, _Score.ToString(), new Vector2(_Viewport.X + 10, _Viewport.Y + 330), Color.White);

            spriteBatch.DrawString(_PopCornFontBig, "BONUS / MALUS", new Vector2(_Viewport.X + 7, _Viewport.Y + 413), Color.Green);

            int Lignes = 0;
            int Colonne = 0;
            int EspacementHorizontal = 20;

            foreach (KeyValuePair<BonusType, int> b in _Bonus)
            {
                spriteBatch.DrawString(_PopCornFontSmall, string.Format("{0} : {1}", b.Key.ToString(), b.Value), new Vector2(_Viewport.X + 5 + (Colonne * EspacementHorizontal), _Viewport.Y + 450 + (Lignes * EspacementVertical)), Color.White);

                if (Lignes > 3)
                {
                    Colonne++;
                    Lignes = 0;
                }

                Lignes++;
            }
        }
    }
}
