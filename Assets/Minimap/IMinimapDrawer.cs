using System.Collections.Generic;

namespace Minimap
{
    public interface IMinimapDrawer
    {
        void Draw(IEnumerable<IMinimapObject> objects);
    }
}
