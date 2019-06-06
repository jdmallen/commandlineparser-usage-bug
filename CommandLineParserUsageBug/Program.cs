using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace CommandLineParserUsageBug
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Parser.Default
				.ParseArguments<DefaultOptions>(args)
				.WithParsed(
					o => { Console.WriteLine("run me with verb \"default\""); });

			Console.ReadLine();
		}
	}

	public enum EnumOptions
	{
		OptionOne,
		OptionTwo
	}

	public interface IOptions
	{
		[Option(
			shortName: 'r',
			longName: "req",
			HelpText = "fake required option to force usage to show",
			Required = true)]
		string RequiredOption { get; set; }

		[Option(
			shortName: 'o',
			longName: "option",
			HelpText = "OptionTwo is set as default, but OptionOne is never specified (as it would need to be) in the usage examples.",
			Default = EnumOptions.OptionTwo)]
		EnumOptions FakeOption { get; set; }
	}

	[Verb("default")]
	public class DefaultOptions : IOptions
	{
		public string RequiredOption { get; set; }

		public EnumOptions FakeOption { get; set; }

		[Usage(ApplicationAlias = "CommandLineParserUsageBug.exe")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				yield return new Example(
					"Run with enum option One selected",
					new DefaultOptions
					{
						FakeOption = EnumOptions.OptionOne
					});
				yield return new Example(
					"Run with enum option Two selected",
					new DefaultOptions
					{
						FakeOption = EnumOptions.OptionTwo
					});
			}
		}
	}
}
