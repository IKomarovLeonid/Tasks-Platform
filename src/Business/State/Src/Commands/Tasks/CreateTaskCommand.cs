using System;
using Environment.Src;
using MediatR;

namespace State.Commands.Tasks
{
    public class CreateTaskCommand : IStateCommand
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationUtc { get; set; }
    }
}
