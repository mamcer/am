using AM.Core;
using Ninject.Modules;

namespace AM
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IAudioPlayer>().To<NAudioPlayer>();
            Bind<IPlaylist>().To<SimplePlaylist>();
        }
    }
}