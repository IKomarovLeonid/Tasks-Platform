﻿using System;
using Environment.State;
using Objects.Dto;

namespace State.Commands.Tasks
{
    public class UpdateTaskCommand : BaseCommand
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus? Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public Priority? Priority { get; set; }

        public string Category { get; set; }

        public UpdateTaskCommand() : base(nameof(UpdateTaskCommand)) { }
    }
}
