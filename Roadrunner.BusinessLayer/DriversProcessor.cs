using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Roadrunner.BusinessInterfaces;
using Roadrunner.DataInterfaces;
using Roadrunner.Types;
using Roadrunner.Utils.Identity;

namespace Roadrunner.BusinessLayer
{
    public class DriversProcessor : IDriversProcessor
    {
        private readonly IRoadrunnerIdentity _roadrunnerIdentity;
        private readonly IDriversRepository _driversRepository;
        private readonly IHistoryRepository _historyRepository;

        public DriversProcessor(IRoadrunnerIdentity roadrunnerIdentity, IDriversRepository driversRepository, IHistoryRepository historyRepository)
        {
            _roadrunnerIdentity = roadrunnerIdentity;
            _driversRepository = driversRepository;
            _historyRepository = historyRepository;
        }

        public async Task DriverReadyAtPositionAsync(Position position)
        {
            _roadrunnerIdentity.ThrowUnautorizedIfNotAuthenticated();

            await _driversRepository.DriverReadyAtPositionAsync(_roadrunnerIdentity.GetUserId(), position);
            _historyRepository.DriverReadyAtPositionAsync(_roadrunnerIdentity.GetUserId(), position);
        }

        public async Task DriverPositionUpdateAsync(Position position)
        {
            _roadrunnerIdentity.ThrowUnautorizedIfNotAuthenticated();

            await _driversRepository.DriverPositionUpdateAsync(_roadrunnerIdentity.GetUserId(), position);
            _historyRepository.DriverPositionUpdateAsync(_roadrunnerIdentity.GetUserId(), position);
        }
    }
}
