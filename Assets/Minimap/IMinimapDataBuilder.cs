using System.Collections.Generic;

namespace Minimap
{
    public interface IMinimapDataBuilder
    {
        IEnumerable<MinimapData> BuildData();
    }
}
