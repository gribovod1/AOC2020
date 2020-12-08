using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public class Command
    {
        public string command;
        public int arg;
        public int count;

        public Command(string description)
        {
            var sc = description.Split(' ');
            command = sc[0];
            arg = int.Parse(sc[1]);
            count = 0;
        }
    }

    public class GameConsole
    {
        public delegate bool CheckTerminate();

        public int CommandAddress { get; private set; }
        public int Accumulator { get; private set; }
        public List<Command> CurrentProgram { get; private set; }

        public void LoadProgramFromFile(string path)
        {
            var commands = File.ReadAllLines(path);
            CurrentProgram = new List<Command>();
            foreach (var commandText in commands)
                CurrentProgram.Add(new Command(commandText));
        }

        public void Reset()
        {
            CommandAddress = 0;
            Accumulator = 0;
            foreach (var command in CurrentProgram)
                command.count = 0;
        }

        public bool Run(CheckTerminate terminate)
        {
            Reset();
            while (CommandAddress < CurrentProgram.Count && !terminate())
            {
                ++CurrentProgram[CommandAddress].count;
                switch (CurrentProgram[CommandAddress].command)
                {
                    case "nop":
                        {
                            ++CommandAddress;
                            break;
                        }
                    case "jmp":
                        {
                            CommandAddress += CurrentProgram[CommandAddress].arg;
                            break;
                        }
                    case "acc":
                        {
                            Accumulator += CurrentProgram[CommandAddress].arg;
                            ++CommandAddress;
                            break;
                        }
                }
            }
            return CommandAddress >= CurrentProgram.Count;
        }
    }
}
