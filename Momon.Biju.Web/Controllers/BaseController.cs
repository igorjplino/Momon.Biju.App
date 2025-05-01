using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Momon.Biju.Web.Controllers;

public abstract class BaseController : Controller
{
    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; }
}