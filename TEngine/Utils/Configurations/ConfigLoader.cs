using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEngine.Utils.Configurations
{
    public interface IConfig<T>
    {
        T Load(string path);
    }

    public abstract class ConfigLoader<T> : IConfig<T>
    {
        public abstract T Load(string path);
    }
}
