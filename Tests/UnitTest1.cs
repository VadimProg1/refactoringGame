using Microsoft.VisualBasic.ApplicationServices;
using System;
using static System.Windows.Forms.AxHost;
using static Tests.UnitTest1;

namespace Tests;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(DataForWolfMoveLeft))]
    public void TestWolfMoveLeft(
        object[,] map,
        int wolfPosX,
        int wolfPosY,
        int expectedWolfPosX,
        int expectedWolfPosY,
        bool checkFoodMock
        )
    {
        if (checkFoodMock)
        {
            var mockFood = new Mock<Apple>(2, 0, new Random(), new List<Cell>(), new object[3, 3]);
            map[wolfPosX - 1, wolfPosY] = mockFood.Object; // -1 because food should be on left side from Wolf

            var actualWolf = new Wolf<IBigAnimal, ISmallAnimal>(wolfPosX, wolfPosY, false, new Random(), new List<Cell>(), map);

            actualWolf.MoveLeft();

            mockFood.Verify(m => m.Death(), Times.Once());
            Assert.Equal(expectedWolfPosX, actualWolf.x);
            Assert.Equal(expectedWolfPosY, actualWolf.y);
        }
        else
        {
            var actualWolf = new Wolf<IBigAnimal, ISmallAnimal>(wolfPosX, wolfPosY, false, new Random(), new List<Cell>(), map);

            actualWolf.MoveLeft();

            Assert.Equal(expectedWolfPosX, actualWolf.x);
            Assert.Equal(expectedWolfPosY, actualWolf.y);
        }
    }
    // Покрыть тестами условие (map[x - 1, y] is Cell)

    public static IEnumerable<object[]> DataForWolfMoveLeft =>
      new List<object[]>
      {
          //Wolf from left. Position not changed
           new object[] {
               CreateMap(3, 3, new Wolf<IBigAnimal, ISmallAnimal>(1, 0, false, new Random(), new List<Cell>(), new object[3, 3]), 1, 0),
               2, //initial position
               0,
               2, //expect same position
               0,
               false
           },
           //Food from left. Position not changed
           new object[] {
               CreateMap(3, 3, new Apple(2, 0, new Random(), new List<Cell>(), new object[3, 3]), 1, 0),
               2, //initial position
               0,
               2, //expect same position
               0,
               true //Food from left. Need for check Death() method invocation
           },
           //Cell from left. Wolf moved to left
           new object[] {
               CreateMap(3, 3, new Cell(1, 0), 1, 0),
               2, //initial position
               0,
               1, //moved left
               0,
               false
           },
           //Cell from left. Position not changed. Boundary check
           new object[] {
               CreateMap(3, 3, new Cell(0, 0), 0, 0),
               1, //initial position
               0,
               1, //expect same position
               0,
               false
           },
      };

    public static object[,] CreateMap(int sizeX, int sizeY, Cell cell, int cellPosX, int cellPosY)
    {
        object[,] map = new object[sizeX, sizeY];
        map[cellPosX, cellPosY] = cell;
        return map;
    }

    [Theory]
    [MemberData(nameof(DataForNukeActivation))]
    public void TestNukeActivate(
        bool explosionEventStarted,
        bool nukeIsLaunched,
        bool nukeInExplosionState,
        bool expectLaunchNukeMethodInvocation,
        bool expectedIsLaunchedValue,
        bool expectedExplosionTimerMethodInvocation
        )
    {
        var mockNuke = new Mock<Nuke>(2, 2, new Random(), new object[3, 3], new List<Cell>());
        mockNuke.Setup(n => n.ActivationEventIsStarted()).Returns(explosionEventStarted);

        mockNuke.Object.isLaunched = nukeIsLaunched;
        mockNuke.Object.state = nukeInExplosionState ? "explosion" : "none";

        mockNuke.Object.Activate();

        mockNuke.Verify(m => m.LaunchNuke(), expectLaunchNukeMethodInvocation ? Times.Once() : Times.Never());

        mockNuke.Verify(m => m.ExplosionTimer(), expectedExplosionTimerMethodInvocation ? Times.Once() : Times.Never());

        if (explosionEventStarted)
        {
            Assert.Equal(expectedIsLaunchedValue, mockNuke.Object.isLaunched);
        }

         
    }

    public static IEnumerable<object[]> DataForNukeActivation =>
      new List<object[]>
      {
          //Event not started. Nuke not launched. Not in explosion state.
           new object[] { 0, 0, 0, 0, 0, 0 },

           //Event not started. Nuke not launched. In explosion state.
           new object[] { 0, 0, 1, 0, 0, 1 },

           //Event not started. Nuke is launched. Not in explosion state.
           new object[] { 0, 1, 0, 1, 1, 0 },

           //Event not started. Nuke is launched. In explosion state.
           new object[] { 0, 1, 1, 1, 1, 1 },

           //Event is started. Nuke not launched. Not in explosion state.
           new object[] { 1, 0, 0, 1, 1, 0 },

           //Event is started. Nuke not launched. In explosion state.
           new object[] { 1, 0, 1, 1, 1, 1 },

           //Event is started. Nuke is launched. Not in explosion state.
           new object[] { 1, 1, 0, 1, 1, 0 },

           //Event is started. Nuke is launched. In explosion state.
           new object[] { 1, 1, 1, 1, 1, 1 },
      };

}


/*
 * public void Activate()
        {
            int eventProb = random.Next(eventPropability);
            if (eventProb == 1 || isLaunched)
            {
                isLaunched = true;
                LaunchNuke();
            }
            if(state == "explosion")
            {
                ExplosionTimer();
            }
        }
 */