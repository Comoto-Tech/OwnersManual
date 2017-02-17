using System;

namespace OwnersManual.Features.Configuration
{
    public static class Manual
    {
        public static ManualInstance Build(Action<IManualConfiguration> action)
        {
            var cfg = new ManualConfiguration();

            action(cfg);

            return new ManualInstance(cfg.Updaters);
        }
    }
}
