﻿using MediatR;
using WheelAuction.Domain.Primitives;

namespace WheelAuction.Application.Abstractions.Messaging;

public interface ICommand : ICommandBase, IRequest<Result> { }