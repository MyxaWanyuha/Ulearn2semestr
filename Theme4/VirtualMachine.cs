using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		private Dictionary<char, Action<IVirtualMachine>> registeredCommands;
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public VirtualMachine(string program, int memorySize)
		{
			registeredCommands = new Dictionary<char, Action<IVirtualMachine>>();
			Memory = new byte[memorySize];
			Instructions = program;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			if (!registeredCommands.ContainsKey(symbol))
				registeredCommands.Add(symbol, execute);
		}

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				var instruction = Instructions[InstructionPointer];
				if(registeredCommands.TryGetValue(instruction, out var b))
					registeredCommands[instruction](this);
				//registeredCommands[instruction](this);
				//if (registeredCommands.ContainsKey(instruction))
				//	registeredCommands[instruction](this);

				//try
				//{
				//	registeredCommands[Instructions[InstructionPointer]](this);
				//}
				//catch (KeyNotFoundException)
				//{
				//	Console.WriteLine("Key is not found.");
				//}
			}
			
		}
	}
}