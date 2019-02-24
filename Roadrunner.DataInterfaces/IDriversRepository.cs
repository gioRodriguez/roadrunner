﻿using System.Threading.Tasks;
using Roadrunner.Types;

namespace Roadrunner.DataInterfaces
{
    public interface IDriversRepository
    {
        Task DriverReadyAtPositionAsync(string driverId, Position position);
    }
}