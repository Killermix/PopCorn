using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    public enum BulletsSide
    {
        Left = 0,
        Right = 1
    }

    public class Bullets : Sprite
    {
        private Viewport _Viewport;
        private BulletsSide _BulletSide;

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public bool isObsolete = false;

        public Bullets(BulletsSide CoteDuBullets)
        {
            _BulletSide = CoteDuBullets;
        }

        public void Initialize(Viewport Viewport, Raquette LaRaquette)
        {
            _Viewport = Viewport;

            Speed = 0.4f;

            Direction = new Vector2(0, -1f);

            float xPosition = LaRaquette.Position.X + (6 * LaRaquette.SizeScale);

            if (_BulletSide == BulletsSide.Right)
            {
                xPosition = LaRaquette.Position.X + (LaRaquette.Texture.Width * LaRaquette.SizeScale) - Texture.Width - (6 * LaRaquette.SizeScale);
            }

            Position = new Vector2(xPosition, LaRaquette.Position.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.Y < _Viewport.X)
            {
                isObsolete = true;
            }

            base.Update(gameTime);
        }
    }
}
