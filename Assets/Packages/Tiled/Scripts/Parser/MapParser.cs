using Tiled;

namespace Tiled.Parser {

    public interface MapParser {

        Map Parse(string text);
    }
}
