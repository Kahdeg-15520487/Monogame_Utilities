using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Utility.Drawing {
	public class Camera {
		private float zoom = 1f;
		private float rotation = 0f;
		private Vector2 centre = Vector2.Zero;
		private Viewport viewPort;

		/// <summary>
		/// Location of the camera. default value is Vector2.Zero
		/// </summary>
		public Vector2 Centre {
			get {
				return centre;
			}
			set {
				centre = value;
			}
		}

		/// <summary>
		/// Zoom ratio. default value is 1f
		/// </summary>
		public float Zoom {
			get {
				return zoom;
			}
			set {
				zoom = value;
			}
		}

		/// <summary>
		/// Rotation of the camera in radiant. default value is 0f
		/// </summary>
		public float Rotation {
			get {
				return rotation;
			}
			set {
				rotation = value;
			}
		}

		public Matrix TransformMatrix {
			get {
				return
					Matrix.CreateTranslation(new Vector3(-centre.X + (viewPort.Width / 2), -centre.Y + (viewPort.Height / 2), 0)) *
					Matrix.CreateRotationZ(rotation) *
					Matrix.CreateScale(zoom);
			}
		}

		public Camera(Viewport viewport) {
			viewPort = viewport;
			centre.X = viewport.Width / 2;
			centre.Y = viewport.Height / 2;
		}

		public Vector2 TranslateFromScreenToWorld(Vector2 vt) {
			return Vector2.Transform(vt, Matrix.Invert(TransformMatrix));
		}

		public Vector2 TranslateFromWorldToScreen(Vector2 vt) {
			return Vector2.Transform(vt, TransformMatrix);
		}
	}
}