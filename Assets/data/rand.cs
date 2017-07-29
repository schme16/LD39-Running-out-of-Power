using System;

public class Rand {

	private System.Random rnd;

	public Rand () {
		rnd = new Random(Guid.NewGuid().GetHashCode());
	}

	public Rand (string seed) {
		rnd = new Random(string.IsNullOrEmpty(seed) ? Guid.NewGuid().GetHashCode() : seed.GetHashCode());
	}


	public int In (int a, int b) {
		return rnd.Next(a, b + 1);
	}

}
