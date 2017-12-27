﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using EduHubLibrary.Domain.Exceptions;
using Serilog;

namespace EduHub.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, "Error");
            switch (context.Exception) {
                case ArgumentOutOfRangeException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case AlreadyMemberException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case GroupIsFullException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case GroupNotFoundException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case MemberNotFoundException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case NotEnoughPermissionsException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case UserNotFoundException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case ArgumentNullException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                default:
                    context.Result = new ObjectResult("Unknown error occured")
                    {
                        StatusCode = 500
                    };
                    return;
            }

        }
    }
}