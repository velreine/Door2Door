using System;
using System.ComponentModel;
using Xunit;
using Door2Door_API.Controllers;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;
using Door2Door_API.Models;
using NetTopologySuite.IO;


namespace Door2Door_API.Test;

public class Door2DoorControllerTest
{

    // Arrange (for multiple)
    private static readonly Room KnownExistingRoom = new Room()
    {
        //WKT output from pgAdmin4 ?
        //0106000020110F000001000000010300000001000000090000001AEBD504D503344122863B79BD665C4149C16EFEDA0334417BD6E444C3665C411ABE06DEED033441BAE362F3C1665C41862AA68DF3033441DD6F9462C7665C414A564535EC03344133F535E8C7665C418F656647F1033441E3D597ABCC665C412761B8F64C0434410CE261B4C6665C41555BFE663B0434413DB6361FB6665C411AEBD504D503344122863B79BD665C41
        //Geometry = new WKTReader(Geometry.DefaultFactory).Read("0106000020110F000001000000010300000001000000090000001AEBD504D503344122863B79BD665C4149C16EFEDA0334417BD6E444C3665C411ABE06DEED033441BAE362F3C1665C41862AA68DF3033441DD6F9462C7665C414A564535EC03344133F535E8C7665C418F656647F1033441E3D597ABCC665C412761B8F64C0434410CE261B4C6665C41555BFE663B0434413DB6361FB6665C411AEBD504D503344122863B79BD665C41"),
        Id = 1,
        Name = "B.26",
        Type = 1, // classroom?
    };
    
    
    public Door2DoorControllerTest() { }
    
    
    [Fact]
    public void ItShouldReturnARoomWhenIdIsValid()
    {

        // Arrange
        var controller = new Door2DoorController();
        
        // Act
        var room = controller.GetRoomById(KnownExistingRoom.Id);

        int x = 2;
        
        // Assert
        Assert.NotNull(room);
    }

    [Fact]
    public void ItShouldReturnA404WhenIdIsInvalid()
    {
        
        // Arrange
        var controller = new Door2DoorController();
        
        // Act
        var room = controller.GetRoomById(-13839819389); // bogus id.
        
        // Assert
        Assert.Null(room);
    }
    
}