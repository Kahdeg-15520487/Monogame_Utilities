//using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utility.ScreenManager;
using Utility.UI;
using System;

namespace Utility.Screens {
	public class FileBrowsingScreen : Screen {
		protected Canvas canvas;
		protected MessageBox msgbox;
		Label label_selectedFile;
		List<Button> filelist;

		public string SelectedFile { get; private set; } = string.Empty;

		public string StartingDirectory { get; set; }
		public string SearchPattern { get; set; }
		public Action<string> CallBack { get; set; }

		public FileBrowsingScreen(GraphicsDevice device) : base(device, "FileBrowsingScreen") { }

		public override bool Init() {
			InitUI();

			return base.Init();
		}

		private void InitUI() {
			canvas = new Canvas();

			InitFileList(StartingDirectory, SearchPattern);

			Button button_open = new Button("Open", new Point(600, 10), new Vector2(60, 30), CONTENT_MANAGER.Fonts["default"]) {
				BorderColor = Color.Black
			};
			button_open.MouseClick += (o, e) => {
				if (string.IsNullOrEmpty(SelectedFile)) {
					msgbox.Show("Warning!\nNo File is selected.", "OK");
					return;
				}
				CallBack?.Invoke(SelectedFile);
			};
			label_selectedFile = new Label("", new Point(600, 50), new Vector2(120, 40), CONTENT_MANAGER.Fonts["hack"]) {
				Origin =Vector2.Zero
			};

			msgbox = new MessageBox(new Point(280, 200), "Warning!", "OK");
			msgbox.Hide();
			msgbox.MiddleButtonPressed += (o, e) => {
				msgbox.Hide();
			};
			canvas.AddElement("button_open", button_open);
			canvas.AddElement("msgbox", msgbox);
			canvas.AddElement("label_selectedFile", label_selectedFile);
		}

		protected virtual void InitFileList(string startingdir, string searchpattern) {
			var files = Directory.GetFiles(startingdir, searchpattern);
			var y = 10;
			filelist = new List<Button>();
			foreach (var m in files) {
				Button bt = new Button(Path.GetFileNameWithoutExtension(m), new Point(10, y), new Vector2(120, 30), CONTENT_MANAGER.Fonts["default"]) {
					Origin = new Vector2(10, 0),
					ForegroundColor = Color.Black,
					BorderColor = Color.Black,
					MetaData = Path.GetDirectoryName(m)
				};

				bt.MouseClick += (o, e) => {
					SelectedFile = bt.Text;
					label_selectedFile.Text = SelectedFile;
				};

				y += 35;
				filelist.Add(bt);
			}

			foreach (var m in filelist) {
				canvas.AddElement(m.Text, m);
			}
		}

		public override void Shutdown() {
			base.Shutdown();
		}

		public override void Update(GameTime gameTime) {
			canvas.Update(gameTime, CONTENT_MANAGER.CurrentInputState, CONTENT_MANAGER.LastInputState);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.BeginSpriteBatch();
			canvas.Draw(spriteBatch, gameTime);
			spriteBatch.EndSpriteBatch();
		}
	}
}
