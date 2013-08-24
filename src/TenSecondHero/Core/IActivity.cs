using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace TenSecondHero.Core
{
    public interface IActivity
    {
        Task Run();
        void Started();
        void Activated();
        void Deactivated();
        void Completed();

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }

    public interface IActivity<T> : IActivity
    {
        new Task<T> Run();
    }
}
