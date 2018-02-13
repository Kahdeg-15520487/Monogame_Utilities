using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility {
	public class Timer {
		private float elapsedtime;

		/// <summary>
		/// Total second of this timer
		/// </summary>
		public float ElapsedTime { get => elapsedtime; }

		/// <summary>
		/// Has this timer been started
		/// </summary>
		public bool IsStarted { get; private set; }

		/// <summary>
		/// Is this timer running
		/// </summary>
		public bool IsRunning { get; private set; }

		public Timer() {
			elapsedtime = 0f;
		}

		/// <summary>
		/// Start this timer
		/// </summary>
		public void Start() {
			IsStarted = true;
			IsRunning = true;
		}

		/// <summary>
		/// Stop this timer but do not reset it to 0
		/// </summary>
		public void Stop() {
			IsRunning = false;
		}

		/// <summary>
		/// Stop this timer and reset it to 0
		/// </summary>
		public void Reset() {
			elapsedtime = 0;
			IsStarted = false;
			IsRunning = false;
		}
		public void Update(GameTime gameTime) {
			if (IsRunning) {
				elapsedtime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			}
		}
	}
}
