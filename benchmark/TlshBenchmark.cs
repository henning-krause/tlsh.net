using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TrendMicro.Tlsh;

[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[MemoryDiagnoser]
public class TlshBenchmark
{
	private byte[]? data;

	[Params(1000, 10_000, 1_000_000)]
	public int N;

	[GlobalSetup]
	public void Setup()
	{
		data = new byte[N];
		new Random(42).NextBytes(data);
	}

	[Benchmark]
	public TlshValue Tlsh()
	{
		var tlsh = new Tlsh();
		tlsh.Update(data);
		return tlsh.GetHash();
	}
}