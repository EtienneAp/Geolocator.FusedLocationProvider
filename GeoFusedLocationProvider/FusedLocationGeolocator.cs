using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace GeoFusedLocationProvider
{
    public class FusedLocationGeolocator : IGeolocator
    {
        private IGeolocator bestGeolocator;

        public FusedLocationGeolocator()
        {
            if (Util.IsPlayServicesAvailable())
            {
                bestGeolocator = new FusedLocationGeolocatorInternal();
            }
            else
            {
                bestGeolocator = new GeolocatorImplementation();
            }
        }

        public Task<bool> StopListeningAsync()
        {
            return bestGeolocator.StopListeningAsync();
        }

        public Task<Position> GetLastKnownLocationAsync()
        {
            return bestGeolocator.GetLastKnownLocationAsync();
        }

        public Task<Position> GetPositionAsync(TimeSpan? timeout = default(TimeSpan?), CancellationToken? token = default(CancellationToken?), bool includeHeading = false)
        {
            return bestGeolocator.GetPositionAsync(timeout, token, includeHeading);
        }

        public Task<IEnumerable<Address>> GetAddressesForPositionAsync(Position position, string mapKey = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StartListeningAsync(TimeSpan minimumTime, double minimumDistance, bool includeHeading = false, ListenerSettings listenerSettings = null)
        {
            return bestGeolocator.StartListeningAsync(minimumTime, minimumDistance, includeHeading);
        }

        public double DesiredAccuracy
        {
            get { return bestGeolocator.DesiredAccuracy; }
            set { bestGeolocator.DesiredAccuracy = value; }
        }

        public bool IsListening
        {
            get { return bestGeolocator.IsListening; }
        }

        public bool SupportsHeading
        {
            get { return bestGeolocator.SupportsHeading; }
        }

        public bool IsGeolocationAvailable
        {
            get { return bestGeolocator.IsGeolocationAvailable; }
        }

        public bool IsGeolocationEnabled
        {
            get { return bestGeolocator.IsGeolocationEnabled; }
        }

        public event EventHandler<PositionErrorEventArgs> PositionError
        {
            add { bestGeolocator.PositionError += value; }
            remove { bestGeolocator.PositionError -= value; }
        }
        public event EventHandler<PositionEventArgs> PositionChanged
        {
            add { bestGeolocator.PositionChanged += value; }
            remove { bestGeolocator.PositionChanged -= value; }
        }
    }
}