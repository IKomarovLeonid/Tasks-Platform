using System;
namespace Environment.Src.State
{
    public abstract class BaseCommand : IStateCommand
    {
        public BaseCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
