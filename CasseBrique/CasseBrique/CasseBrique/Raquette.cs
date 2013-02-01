using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CasseBrique
{
    public class Raquette : Sprite
    {
        private float _SizeScale;
        private float SecondCout;
        private const int C_DEFAULT_RAQUETTE_WIDTH = 100;
        private Dictionary<BonusType, int> _ListeBonus;
        private List<Bullets> _ListeBullets;
        private Viewport _Viewport;
        private int _Score = 0;
        private float BulletsTimer;

        public int Vie = 3;
        public int Score
        {
            get
            {
                return _Score;
            }
        }
        public float SizeScale
        {
            get
            {
                return _SizeScale;
            }
        }
        public Dictionary<BonusType, int> ListeBonus
        {
            get
            {
                return _ListeBonus;
            }
        }
        public List<Bullets> ListeBullets
        {
            get
            {
                return _ListeBullets;
            }
            set
            {
                _ListeBullets = value;
            }
        }
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Convert.ToInt32(Texture.Width * _SizeScale), Texture.Height);
            }
        }

        public Raquette(Viewport GameViewport)
        {
            _Viewport = GameViewport;
        }

        public override void Initialize()
        {
            Speed = 0.7f;

            _SizeScale = 1.0f;

            _ListeBonus = new Dictionary<BonusType, int>();

            _ListeBullets = new List<Bullets>();

            Position = new Vector2((_Viewport.X + (_Viewport.Width / 2)) - (C_DEFAULT_RAQUETTE_WIDTH / 2), _Viewport.Height - 30);
        }

        public void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState keyboardState, ContentManager content)
        {
            if (keyboardState.IsKeyDown(Keys.Left) && Position.X > _Viewport.X)
            {
                PositionX -= Speed * ElapsedTime;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && (Position.X + (Texture.Width * _SizeScale)) < _Viewport.Width + _Viewport.X)
            {
                PositionX += Speed * ElapsedTime;
            }

            
            BulletsTimer += ElapsedTime;

            if (_ListeBonus.ContainsKey(BonusType.Bullets) && keyboardState.IsKeyDown(Keys.A) && BulletsTimer >= 200.0f)
            {
                BulletsTimer = 0;

                Bullets BulletGauche = new Bullets(BulletsSide.Left);
                Bullets BulletDroite = new Bullets(BulletsSide.Right);

                _ListeBullets.Add(BulletGauche);
                _ListeBullets.Add(BulletDroite);

                BulletGauche.LoadContent(content, "Bullets");
                BulletDroite.LoadContent(content, "Bullets");

                BulletGauche.Initialize(_Viewport, this);
                BulletDroite.Initialize(_Viewport, this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.X < _Viewport.X)
            {
                PositionX = _Viewport.X;
            }

            if (Position.X + (Texture.Width * _SizeScale) > _Viewport.Width + _Viewport.X)
            {
                PositionX = (_Viewport.Width + _Viewport.X) - (Texture.Width * _SizeScale);
            }

            if (_ListeBonus.ContainsKey(BonusType.Tall))
            {
                if (_SizeScale < 1.5f)
                {
                    _SizeScale += 0.01f;
                    PositionX -= 0.5f;
                }
            }
            else if (_ListeBonus.ContainsKey(BonusType.Small))
            {
                if (_SizeScale > 0.5f)
                {
                    _SizeScale -= 0.01f;
                    PositionX += 0.5f;
                }
            }
            else // Pour gérer la décroissance et la recroissance
            {
                if (_SizeScale > 1.0f)
                {
                    _SizeScale -= 0.01f;
                    PositionX += 0.5f;
                }
                else if(_SizeScale < 1.0f)
                {
                    _SizeScale += 0.01f;
                    PositionX -= 0.5f;
                }
            }

            SecondCout += ElapsedTime;

            if (SecondCout >= 1000.0f)
            {
                SecondCout = 0;

                List<BonusType> Keys = new List<BonusType>(_ListeBonus.Keys);

                foreach (BonusType TypeBonus in Keys)
                {
                    _ListeBonus[TypeBonus]--;

                    if (_ListeBonus[TypeBonus] < 1)
                    {
                        _ListeBonus.Remove(TypeBonus);
                    }
                }
            }

            base.Update(gameTime); //On fait le update de base pour avoir l'elapsed time cela ne changera pas la position car direction est egale a (0, 0)
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            //if (_ListeBonus.ContainsKey(BonusType.Tall) || _ListeBonus.ContainsKey(BonusType.Small))
            //{
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(_SizeScale, 1f), SpriteEffects.None, 0f);
            //}
            //else
            //{
            //    spriteBatch.Draw(Texture, Position, Color.White);
            //}

            foreach (Bullets Bullet in _ListeBullets)
            {
                Bullet.Draw(spriteBatch, gameTime);
            }
        }

        public void AddBonus(BonusType NewBonus)
        {
            if (NewBonus == BonusType.Life)
            {
                Vie++;

                //Supprimer le bonus ou montrer qu'on a eu une vie...
            }
            else
            {
                if (!_ListeBonus.ContainsKey(NewBonus))
                {
                    if (NewBonus == BonusType.Small)
                    {
                        if (_ListeBonus.ContainsKey(BonusType.Tall))
                        {
                            _ListeBonus.Remove(BonusType.Tall);
                        }
                    }

                    if (NewBonus == BonusType.Tall)
                    {
                        if (_ListeBonus.ContainsKey(BonusType.Small))
                        {
                            _ListeBonus.Remove(BonusType.Small);
                        }
                    }

                    _ListeBonus.Add(NewBonus, 10);
                }
                else
                {
                    _ListeBonus[NewBonus] = 10;
                }
            }
        }

        public void UpdateScore(int Points)
        {
            _Score += Points;
        }
    }
}
