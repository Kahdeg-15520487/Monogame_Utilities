using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
	public class Range : IEnumerable<int> {
		public readonly int Max;
		public readonly int Min;
		public readonly int Step;
		private readonly IEnumerable<int> range;
		public Range(int min, int max, int step = 1) {
			Max = max;
			Min = min;
			Step = step;
			if (Max % Step != 0 || Min % Step != 0) {
				throw new InvalidOperationException("number range is not divisible into step");
			}
			Max /= Step;
			Min /= Step;
			range = Enumerable.Range(Min, Max - Min);
		}

		public bool InRange(int num, bool isInclusive = true) {
			var n = num * Step;
			if (isInclusive) {
				return n <= Max && n >= Min;
			}
			else {
				return n < Max && n > Min;
			}
		}

		public int Clamp(int num) {
			var n = num * Step;
			return (n < Min) ? Min : (n > Max) ? Max : n;
		}

		public int Random(Random rand) {
			return rand.Next(Max) * Step;
		}

		public int Next(int num) {
			return Clamp((int)Math.Floor((float)num / Step) + 1) * Step;
		}

		public int Previous(int num) {
			return Clamp((int)Math.Floor((float)num / Step) - 1) * Step;
		}

		public IEnumerator<int> GetEnumerator() {
			foreach (var num in range) {
				yield return num * Step;
			}
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator() {
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}