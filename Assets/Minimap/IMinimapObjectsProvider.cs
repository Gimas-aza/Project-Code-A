using System.Collections.Generic;

namespace Minimap
{
    public interface IMinimapObjectsProvider
    {
        IEnumerable<IMinimapObject> GetObjects();
    }
}
