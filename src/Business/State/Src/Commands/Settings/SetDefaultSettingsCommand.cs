using System;
using Environment.Src;
using Environment.Src.State;

namespace State.Src.Commands.Settings
{
    public class SetDefaultSettingsCommand : BaseCommand
    {
        public SetDefaultSettingsCommand() : base(nameof(SetDefaultSettingsCommand))
        {

        }
    }
}
