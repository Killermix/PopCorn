using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CasseBrique
{
    public enum MenuAction
    {
        PlayClassic = 0,
        PlayInfinite = 1,
        Score = 2,
        Exit = 3
    }

    public class Menu
    {
        private Texture2D _BackgroundMenu;
        private List<MenuBouton> _Boutons;
        private MenuBouton _btnPlayClassic;
        private MenuBouton _btnPlayInfinite;
        private MenuBouton _btnScores;
        private MenuBouton _btnExit;

        private int _screenWidth;
        private int _screenHeight;
        private bool IsAnyKeyDownBefore = false;

        public event EventHandler OnBoutonSelected; 

        public Menu(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            _Boutons = new List<MenuBouton>();

            _btnPlayClassic = new MenuBouton("Play classic mod", MenuPosition.First, MenuAction.PlayClassic, _screenWidth, _screenHeight);
            _btnPlayClassic.Selected = true;

            _btnPlayInfinite = new MenuBouton("Play infinite mod", MenuPosition.Second, MenuAction.PlayInfinite, _screenWidth, _screenHeight);

            _btnScores = new MenuBouton("Scores", MenuPosition.Third, MenuAction.Score, _screenWidth, _screenHeight);

            _btnExit = new MenuBouton("Exit", MenuPosition.Last, MenuAction.Exit, _screenWidth, _screenHeight);

            _Boutons.Add(_btnPlayClassic);
            _Boutons.Add(_btnPlayInfinite);
            _Boutons.Add(_btnScores);
            _Boutons.Add(_btnExit);
        }

        public void LoadContent(ContentManager content)
        {
            _BackgroundMenu = content.Load<Texture2D>("BackgroundMenu");

            _btnPlayClassic.LoadContent(content);
            _btnPlayInfinite.LoadContent(content);
            _btnScores.LoadContent(content);
            _btnExit.LoadContent(content);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            bool AnyKeyDownNow;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Enter))
            {
                AnyKeyDownNow = true;

                if (AnyKeyDownNow && !IsAnyKeyDownBefore)
                {
                    MenuBouton SelectedBouton = (from b in _Boutons
                                                 where b.Selected
                                                 select b).SingleOrDefault();

                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        SelectedBouton.Selected = false;

                        if (SelectedBouton.BoutonPosition != MenuPosition.Last)
                        {
                            MenuBouton BoutonToSelect = (from b in _Boutons
                                                         where b.BoutonPosition == (SelectedBouton.BoutonPosition + 1)
                                                         select b).SingleOrDefault();

                            BoutonToSelect.Selected = true;
                        }
                        else
                        {
                            MenuBouton BoutonToSelect = (from b in _Boutons
                                                         where b.BoutonPosition == MenuPosition.First
                                                         select b).SingleOrDefault();

                            BoutonToSelect.Selected = true;
                        }
                    }
                    else if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        SelectedBouton.Selected = false;

                        if (SelectedBouton.BoutonPosition != MenuPosition.First)
                        {
                            MenuBouton BoutonToSelect = (from b in _Boutons
                                                         where b.BoutonPosition == (SelectedBouton.BoutonPosition - 1)
                                                         select b).SingleOrDefault();

                            BoutonToSelect.Selected = true;
                        }
                        else
                        {
                            MenuBouton BoutonToSelect = (from b in _Boutons
                                                         where b.BoutonPosition == MenuPosition.Last
                                                         select b).SingleOrDefault();

                            BoutonToSelect.Selected = true;
                        }
                    }
                    else
                    {
                        OnBoutonSelected(SelectedBouton, new EventArgs());
                    }
                }
            }
            else
            {
                AnyKeyDownNow = false;
            }

            IsAnyKeyDownBefore = AnyKeyDownNow;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_BackgroundMenu, Vector2.Zero, Color.White);

            foreach (MenuBouton Bouton in _Boutons)
            {
                Bouton.Draw(spriteBatch);
            }
        }
    }
}
