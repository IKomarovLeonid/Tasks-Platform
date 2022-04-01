namespace Environment.State
{
    public abstract class BaseCommand : IStateCommand
    {
        protected BaseCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
