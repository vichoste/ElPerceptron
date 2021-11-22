internal class Program {
	private static int CalculateOutput(double firstInput, double secondInput, double[] weights) => firstInput * weights[0] + secondInput * weights[1] + 1 * weights[2] >= 0 ? 1 : 0;

	private static async Task Main(string[] args) {
		try {
			var inputFile = await File.ReadAllLinesAsync(args[0]);
			var outputFile = await File.ReadAllLinesAsync(args[1]);
			var input = inputFile.Select(x => x.Trim().Split(' ').Select(y => int.Parse(y)).ToArray()).ToArray();
			var output = outputFile.Select(y => int.Parse(y)).ToArray();
			if (input.Length != output.Length) {
				Console.WriteLine("Input and output count does not match");
				return;
			}
			var r = new Random();
			double[] weights = { r.NextDouble(), r.NextDouble(), r.NextDouble() };
			var learningRate = 1.0;
			var totalError = 1.0;
			do {
				totalError = 0;
				for (var i = 0; i < output.Length; i++) {
					var @out = CalculateOutput(input[i][0], input[i][1], weights);
					var error = output[i] - @out;
					weights[0] += learningRate * error * input[i][0];
					weights[1] += learningRate * error * input[i][1];
					weights[2] += learningRate * error * 1;
					totalError += Math.Abs(error);
				}
			} while (totalError > .2);
			Console.WriteLine("Result:");
			for (var i = 0; i < output.Length; i++) {
				Console.WriteLine(CalculateOutput(input[i][0], input[i][1], weights));
			}
			Console.WriteLine($"Error: {totalError}");
		} catch (Exception) {
			Console.WriteLine("No input/output files provided");
		}
	}
}