using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public enum BonusType
    {
        None = 0,
        Tall = 1,
        Adhesive = 2,
        Small = 3,
        Fast = 4,
        Bullets = 5,
        PowerBall = 6,
        Life = 7
    }

    public class Bonus : Sprite
    {
        private int _screenHeight;

        private BonusType _BonusType = BonusType.None;

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public bool isObsolete = false;

        public Bonus()
        {
            //Bonus une Chance sur 5
            int rdmNumber1 = MyPopCorn.GlobalRnd.Next(1, 6);

            //Bonus une Chance sur 10
            int rdmNumber2 = MyPopCorn.GlobalRnd.Next(1, 11);

            //Bonus une Chance sur 100
            int rdmNumber3 = MyPopCorn.GlobalRnd.Next(1, 101);

            if (rdmNumber3 == 100)
            {
                List<BonusType> BonusTresRare = new List<BonusType>();
                BonusTresRare.Add(BonusType.Bullets);
                BonusTresRare.Add(BonusType.PowerBall);
                BonusTresRare.Add(BonusType.Life);

                int RandomIndex = MyPopCorn.GlobalRnd.Next(0, BonusTresRare.Count);

                _BonusType = BonusTresRare[RandomIndex];
            }
            else if (rdmNumber2 == 10)
            {
                List<BonusType> BonusRare = new List<BonusType>();
                BonusRare.Add(BonusType.Small);
                BonusRare.Add(BonusType.Fast);

                int RandomIndex = MyPopCorn.GlobalRnd.Next(0, BonusRare.Count);

                _BonusType = BonusRare[RandomIndex];
                //_BonusType = BonusType.Small;
            }
            else if (rdmNumber1 == 5)
            {
                List<BonusType> BonusNormaux = new List<BonusType>();
                BonusNormaux.Add(BonusType.Tall);
                BonusNormaux.Add(BonusType.Adhesive);

                int RandomIndex = MyPopCorn.GlobalRnd.Next(0, BonusNormaux.Count);

                _BonusType = BonusNormaux[RandomIndex];
            }
            else
            {
                isObsolete = true;
            }
        }

        public void Initialize(int screenHeight, Brique BriqueConteneur)
        {
            _screenHeight = screenHeight;

            Speed = 0.2f;

            Direction = new Vector2(0, 1);

            Position = new Vector2((BriqueConteneur.Position.X + (BriqueConteneur.Texture.Width / 2)) - (Texture.Width / 2), (BriqueConteneur.Position.Y + (Texture.Height / 2)) - (Texture.Height / 2));
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            string AssetName = string.Empty;

            switch (_BonusType)
            {
                case BonusType.Tall:
                    AssetName = "BonusTall";
                    break;
                case BonusType.Adhesive:
                    AssetName = "BonusAdhesive";
                    break;
                case BonusType.Small:
                    AssetName = "BonusSmall";
                    break;
                case BonusType.Fast:
                    AssetName = "BonusFast";
                    break;
                case BonusType.Bullets:
                    AssetName = "BonusBullets";
                    break;
                case BonusType.PowerBall:
                    AssetName = "BonusPowerBall";
                    break;
                case BonusType.Life:
                    AssetName = "BonusLife";
                    break;
            }

            base.LoadContent(content, AssetName);
        }

        public void Update(GameTime gameTime, Raquette _Raquette)
        {
            base.Update(gameTime);

            if (Position.Y > _screenHeight)
            {
                isObsolete = true;
            }

            if (_Raquette.CollisionRectangle.Intersects(CollisionRectangle))
            {
                _Raquette.AddBonus(_BonusType);
                isObsolete = true;
            }
        }
    }
}
