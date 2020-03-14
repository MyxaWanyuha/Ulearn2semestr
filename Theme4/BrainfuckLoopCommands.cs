using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		private static Stack<int> stackBrackets = new Stack<int>();
		private static Dictionary<int, int> dictBrackets = new Dictionary<int, int>();

        public static void SearchBrackets(IVirtualMachine vm)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[')
                    stackBrackets.Push(i);

                if (vm.Instructions[i] == ']')
                {
                    var index = stackBrackets.Pop();
                    dictBrackets[i] = index;
                    dictBrackets[index] = i;
                }
            }
        }

		public static void RegisterTo(IVirtualMachine vm)
		{
            SearchBrackets(vm);
            vm.RegisterCommand('[', b =>
			{
			    if (b.Memory[b.MemoryPointer] == 0)
			    	b.InstructionPointer = dictBrackets[b.InstructionPointer];
			});

			vm.RegisterCommand(']', b =>
			{
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = dictBrackets[b.InstructionPointer];
			});
		}
	}
}