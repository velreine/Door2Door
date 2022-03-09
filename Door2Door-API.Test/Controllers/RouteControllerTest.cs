using System;
using System.Net;
using Door2Door_API.Controllers;
using Door2Door_API.ExceptionTypes;
using Door2Door_API.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Door2Door_API.Test.Controllers;

public class RouteControllerTest
{

    [Fact]
    public async void GetRoute_ShouldReturn404WhenNoRouteCanBeGenerated()
    {
        // Arrange
        var mockRepository = new Mock<IRouteRepository>();
        mockRepository.Setup(m => m.GetRouteTo(1)).Throws(new RouteBuildingException("..."));
        var controller = new RouteController(mockRepository.Object);
        
        // Act
        var task = await controller.GetRoute(1);
        
        // Assert
        Assert.IsType<NotFoundObjectResult>(task.Result);
    }
    
    [Fact]
    public async void GetRoute_ShouldReturn500WhenAnExceptionIsThrown()
    {
        // Arrange
        var mockRepository = new Mock<IRouteRepository>();
        mockRepository.Setup(m => m.GetRouteTo(1)).Throws(new Exception("..."));
        var controller = new RouteController(mockRepository.Object);
        
        // Act
        var task = await controller.GetRoute(1);
        
        // Assert
        var actualStatusCode = ((task.Result as ObjectResult)!).StatusCode;
        Assert.Equal((int)HttpStatusCode.InternalServerError, actualStatusCode);
        Assert.IsType<NotFoundObjectResult>(task.Result);
    }
    
}