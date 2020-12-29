using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utility {
	namespace ScreenManager {
		/// <summary>
		/// Screen Manager
		/// Keeps a list of available screens
		/// so you can switch between them, 
		/// ie. jumping from the start screen to the game screen 
		/// </summary>
		public static class SCREEN_MANAGER {
			// Protected Members
			static private List<Screen> screens = new List<Screen>();
			static private bool started = false;
			static private Screen previous = null;
			// Public Members
			static public Screen ActiveScreen = null;

			/// <summary>
			/// Add new Screen
			/// </summary>
			/// <param name="screen">New screen, name must be unique</param>
			static public void AddScreen(Screen screen) {
				foreach (Screen scr in screens) {
					if (scr.Name == screen.Name) {
						return;
					}
				}
				screens.Add(screen);
			}

			static public int GetScreensCount() {
				return screens.Count;
			}

			static public Screen GetScreen(int index) {
				return screens[index];
			}

			static public Screen GetScreen(string screenname) {
				for (int i = 0; i < screens.Count; i++) {
					if (string.Compare(screens[i].Name, screenname) == 0) {
						return screens[i];
					}
				}
				return null;
			}

			/// <summary>
			/// Go to screen
			/// </summary>
			/// <param name="name">Screen name</param>
			static public void GotoScreen(string name) {
				foreach (Screen screen in screens) {
					if (screen.Name == name) {
						// Shutsdown Previous Screen           
						previous = ActiveScreen;
						if (ActiveScreen != null) {
							ActiveScreen.Shutdown();
						}
						// Inits New Screen
						ActiveScreen = screen;
						if (started) ActiveScreen.Init();
						return;
					}
				}
			}

			/// <summary>
			/// Init Screen manager
			/// Only at this point is screen manager going to init the selected screen
			/// </summary>
			static public void Init() {
				started = true;
				if (ActiveScreen != null) {
					ActiveScreen.Init();
				}
			}
			/// <summary>
			/// Falls back to previous selected screen if any
			/// </summary>
			static public void GoBack() {
				if (previous != null) {
					GotoScreen(previous.Name);
					return;
				}
			}


			/// <summary>
			/// Updates Active Screen
			/// </summary>
			/// <param name="elapsed">GameTime</param>
			static public void Update(GameTime gameTime) {
				if (started == false) return;
				if (ActiveScreen != null) {
					ActiveScreen.Update(gameTime);
				}
			}

			static public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
				if (started == false) return;
				if (ActiveScreen != null) {
					ActiveScreen.Draw(spriteBatch, gameTime);
				}
			}
		}
	}
}