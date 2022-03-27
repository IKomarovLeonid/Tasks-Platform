using System;
using MediatR;

namespace State.Commands
{
    public class CreateTaskCommand : IRequest<StateResult>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationUtc { get; set; }
    }
}
