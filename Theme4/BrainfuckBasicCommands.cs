using System;
using System.Text;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        private static void Act(IVirtualMachine b, byte border, byte newValue, bool isPlus)
        {
            if (b.Memory[b.MemoryPointer] == border)
                b.Memory[b.MemoryPointer] = newValue;
            else if (isPlus)
                b.Memory[b.MemoryPointer]++;
            else
                b.Memory[b.MemoryPointer]--;
        }
        //private static void Act2(ref int obj, int border, int newValue, bool isPlus)
        //{
        //    if (obj == border)
        //        obj = newValue;
        //    else if (isPlus)
        //        obj++;
        //    else
        //        obj--;
        //}

        private static void Act2(ref byte obj, byte border, byte newValue, bool isPlus)
        {
            if (obj == border)
                obj = newValue;
            else if (isPlus)
                obj++;
            else
                obj--;
        }

        private static void ChangePointer(IVirtualMachine b, int border, int newValue, bool isPlus)
        {
            if (b.MemoryPointer == border)
                b.MemoryPointer = newValue;
            else if (isPlus)
                b.MemoryPointer++;
            else
                b.MemoryPointer--;
        }

        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });

            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });

            vm.RegisterCommand('+', b => Act2(ref b.Memory[b.MemoryPointer], byte.MaxValue, byte.MinValue, true));//Act(b, byte.MaxValue, byte.MinValue, true));

            vm.RegisterCommand('-', b => Act(b, byte.MinValue, byte.MaxValue, false));

            vm.RegisterCommand('>', b => ChangePointer(b, b.Memory.Length - 1, 0, true));

            vm.RegisterCommand('<', b => ChangePointer(b, 0, b.Memory.Length - 1, false));

            RegChars(vm);
        }

        private static void RegChars(IVirtualMachine vm)
        {
            var sb = new StringBuilder();
            AddChars(sb, 'a', 'z');
            AddChars(sb, 'A', 'Z');
            AddChars(sb, '0', '9');

            var chars = sb.ToString().ToCharArray();
            foreach (var e in chars)
                vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = (byte)e; });
        }

        private static void AddChars(StringBuilder sb, char first, char last)
        {
            for (char a = first; a <= last; a++)
                sb.Append(a);
        }
    }
}