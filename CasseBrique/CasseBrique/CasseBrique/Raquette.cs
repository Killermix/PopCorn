using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    public class Raquette : Sprite
    {
        private float _Size;
        private float SecondCout;
        private const int C_DEFAULT_RAQUETTE_WIDTH = 100;
        private Dictionary<BonusType, int> _ListeBonus;
        private Viewport _Viewport;
        private int _Score = 0;

        public int Vie = 3;
        public int Score
        {
            get
            {
                return _Score;
            }
        }
        public Dictionary<BonusType, int> ListeBonus
        {
            get
            {
                return _ListeBonus;
            }
        }
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Convert.ToInt32(Texture.Width * _Size), Texture.Height);
            }
        }

        public Raquette(Viewport GameViewport)
        {
            _Viewport = GameViewport;
        }

        public override void Initialize()
        {
            Speed = 0.7f;

            _Size = 1.0f;

            _ListeBonus = new Dictionary<BonusType, int>();

            Position = new Vector2((_Viewport.X + (_Viewport.Width / 2)) - (C_DEFAULT_RAQUETTE_WIDTH / 2), _Viewport.Height - 30);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        public void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left) && Position.X > _Viewport.X)
            {
                PositionX -= Speed * ElapsedTime;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && (Position.X + (Texture.Width * _Size)) < _Viewport.Width + _Viewport.X)
            {
                PositionX += Speed * ElapsedTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.X < _Viewport.X)
            {
                PositionX = _Viewport.X;
            }

            if (Position.X + (Texture.Width * _Size) > _Viewport.Width + _Viewport.X)
            {
                PositionX = (_Viewport.Width + _Viewport.X) - (Texture.Width * _Size);
            }

            if (_ListeBonus.ContainsKey(BonusType.Tall))
            {
                _Size = 1.5f;
            }

            if (_ListeBonus.ContainsKey(BonusType.Small))
            {
                _Size = 0.5f;
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
            if (_ListeBonus.ContainsKey(BonusType.Tall) || _ListeBonus.ContainsKey(BonusType.Small))
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, new Vector2(_Size, 1f), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
            }
            else
            {
                _Size = 1.0f;
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        public void AddBonus(BonusType NewBonus)
        {
            if (NewBonus == BonusType.Life)
            {
                Vie++;
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
